using UnityEngine;
using System;
using System.Collections.Generic;

namespace ET.Client
{
    public class UIComponentAwakeSystem: AwakeSystem<UIComponent>
    {
        protected override void Awake(UIComponent self)
        {
            self.Awake();
        }
    }

    public class UIComponentDestroySystem: DestroySystem<UIComponent>
    {
        protected override void Destroy(UIComponent self)
        {
            self.Destroy();
        }
    }

    public enum AtlasType
    {
        Icon,
        Widget,
        ArtText,
        Emotion,
    }

    [FriendOf(typeof (ShowWindowData))]
    [FriendOf(typeof (WindowCoreData))]
    [FriendOf(typeof (UIBaseWindow))]
    [FriendOf(typeof (UIComponent))]
    public static class UIComponentSystem
    {
        public static void Awake(this UIComponent self)
        {
            self.AddComponent<UIMask, bool>(true);
            self.IsPopStackWndStatus = false;
            self.AllWindowsDic?.Clear();
            self.VisibleWindowsDic?.Clear();
            self.StackWindowsQueue?.Clear();
            self.UIBaseWindowlistCached?.Clear();
            foreach (var name in Enum.GetNames(typeof (AtlasType)))
            {
                self.AtlasPath.Add(name, name.ToUISpriteAtlasPath());
            }
        }

        public static string GetAtlasPath(this UIComponent self, AtlasType t)
        {
            return self.AtlasPath[t.ToString()];
        }

        /// <summary>
        /// 窗口是否是正在显示的 
        /// </summary>
        /// <OtherParam name="id"></OtherParam>
        /// <returns></returns>
        public static bool IsWindowVisible(this UIComponent self, WindowID id)
        {
            return self.VisibleWindowsDic.ContainsKey((int)id);
        }

        /// <summary>
        /// 根据泛型获得UI窗口逻辑组件
        /// </summary>
        /// <param name="self"></param>
        /// <param name="isNeedShowState">界面是否处于打开状态</param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetDlgLogic<T>(this UIComponent self, bool isNeedShowState = false) where T : Entity, IUILogic
        {
            WindowID windowsId = self.GetWindowIdByGeneric<T>();
            UIBaseWindow baseWindow = self.GetUIBaseWindow(windowsId);
            if (baseWindow == null)
            {
                return null;
            }

            if (!baseWindow.IsPreLoad)
            {
                Log.Warning($"{windowsId} is not loaded!");
                return null;
            }

            if (isNeedShowState)
            {
                if (!self.IsWindowVisible(windowsId))
                {
                    return null;
                }
            }

            return baseWindow.GetComponent<T>();
        }

        /// <summary>
        /// 根据泛型类型获取窗口Id
        /// </summary>
        /// <param name="self"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static WindowID GetWindowIdByGeneric<T>(this UIComponent self) where T : Entity
        {
            if (UIPath.Instance.WindowTypeIdDict.TryGetValue(typeof (T).Name, out int windowsId))
            {
                return (WindowID)windowsId;
            }

            Log.Error($"{typeof (T).FullName} is not have any windowId!");
            return WindowID.Win_Invaild;
        }

        /// <summary>
        /// 根据泛型类型获取窗口Id
        /// </summary>
        /// <param name="self"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static WindowID GetWindowIdByGeneric(this UIComponent self, Type type)
        {
            if (UIPath.Instance.WindowTypeIdDict.TryGetValue(type.Name, out int windowsId))
            {
                return (WindowID)windowsId;
            }

            Log.Error($"{type.FullName} is not have any windowId!");
            return WindowID.Win_Invaild;
        }

        /// <summary>
        /// 压入一个进栈队列界面
        /// </summary>
        /// <param name="self"></param>
        /// <typeparam name="T"></typeparam>
        public static void ShowStackWindow<T>(this UIComponent self) where T : Entity, IUILogic
        {
            WindowID id = self.GetWindowIdByGeneric<T>();
            self.ShowStackWindow(id);
        }

        /// <summary>
        /// 压入一个进栈队列界面
        /// </summary>
        /// <param name="self"></param>
        /// <param name="id"></param>
        public static void ShowStackWindow(this UIComponent self, WindowID id)
        {
            self.StackWindowsQueue.Enqueue(id);

            if (self.IsPopStackWndStatus)
            {
                return;
            }

            self.IsPopStackWndStatus = true;
            self.PopStackUIBaseWindow();
        }

        /// <summary>
        /// 弹出并显示一个栈队列中的界面
        /// </summary>
        /// <param name="self"></param>
        private static void PopStackUIBaseWindow(this UIComponent self)
        {
            if (self.StackWindowsQueue.Count > 0)
            {
                WindowID windowID = self.StackWindowsQueue.Dequeue();
                self.ShowWindowAsync(windowID).Coroutine();
                UIBaseWindow uiBaseWindow = self.GetUIBaseWindow(windowID);
                uiBaseWindow.IsInStackQueue = true;
            }
            else
            {
                self.IsPopStackWndStatus = false;
            }
        }

        /// <summary>
        /// 弹出并显示下一个栈队列中的界面
        /// </summary>
        /// <param name="self"></param>
        /// <param name="id"></param>
        private static void PopNextStackUIBaseWindow(this UIComponent self, WindowID id)
        {
            UIBaseWindow uiBaseWindow = self.GetUIBaseWindow(id);
            if (uiBaseWindow is { IsDisposed: false } && self.IsPopStackWndStatus && uiBaseWindow.IsInStackQueue)
            {
                uiBaseWindow.IsInStackQueue = false;
                self.PopStackUIBaseWindow();
            }
        }

        /// <summary>
        /// 根据泛型类型显示UI窗口
        /// </summary>
        /// <param name="self">UI组件</param>
        /// <param name="showData">窗口透传参数</param>
        /// <typeparam name="T"></typeparam>
        public static async ETTask ShowWindow<T>(this UIComponent self, ShowWindowData showData = null) where T : Entity, IUILogic
        {
            WindowID windowsId = self.GetWindowIdByGeneric<T>();
            await self.ShowWindowAsync(windowsId, showData);
        }

        /// <summary>
        /// 根据指定Id的异步加载显示UI窗口
        /// </summary>
        /// <param name="self">UI组件</param>
        /// <param name="id">窗口类型ID</param>
        /// <param name="showData">窗口透传参数</param>
        public static async ETTask ShowWindowAsync(this UIComponent self, WindowID id, ShowWindowData showData = null)
        {
            UIBaseWindow baseWindow = self.GetUIBaseWindow(id);
            try
            {
                baseWindow = await self.ShowBaseWindowAsync(id);
                if (null != baseWindow)
                {
                    self.RealShowWindow(baseWindow, id, showData);
                }
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        /// <summary>
        /// 根据泛型类型异步加载显示UI窗口
        /// </summary>
        /// <param name="self"></param>
        /// <param name="showData"></param>
        /// <typeparam name="T"></typeparam>
        public static async ETTask ShowWindowAsync<T>(this UIComponent self, ShowWindowData showData = null) where T : Entity, IUILogic
        {
            WindowID windowsId = self.GetWindowIdByGeneric<T>();
            await self.ShowWindowAsync(windowsId, showData);
        }

        /// <summary>
        /// 隐藏ID指定的UI窗口
        /// </summary>
        /// <OtherParam name="id"></OtherParam>
        /// <OtherParam name="onComplete"></OtherParam>
        public static void HideWindow(this UIComponent self, WindowID id)
        {
            if (!self.VisibleWindowsDic.ContainsKey((int)id))
            {
                Log.Warning($"检测关闭 WindowsID: {id} 失败！");
                return;
            }

            UIBaseWindow baseWindow = self.VisibleWindowsDic[(int)id];
            if (baseWindow == null || baseWindow.IsDisposed)
            {
                Log.Error($"UIBaseWindow is null  or isDisposed ,  WindowsID: {id} 失败！");
                return;
            }

            UIEvent.Instance.GetUIEventHandler(id).OnUnFocus(baseWindow);
            baseWindow.UIPrefabGameObject?.SetActive(false);
            UIEvent.Instance.GetUIEventHandler(id).OnHideWindow(baseWindow);
            if (baseWindow.WindowData.NeedMask)
            {
                self.GetComponent<UIMask>().Hide();
            }

            self.VisibleWindowsDic.Remove((int)id);
            for (int i = self.ShowWindowsList.Count - 1; i >= 0; i--)
            {
                if (self.ShowWindowsList[i].WindowID == id)
                {
                    self.ShowWindowsList.RemoveAt(i);
                    break;
                }
            }

            if (self.ShowWindowsList.Count > 0)
            {
                var win = self.ShowWindowsList[0];
                UIEvent.Instance.GetUIEventHandler(win.WindowID).OnFocus(win);
                Log.Info("<color=magenta>### window Focus </color>" + win.WindowID);
            }

            self.PopNextStackUIBaseWindow(id);
        }

        /// <summary>
        /// 根据泛型类型隐藏UI窗口
        /// </summary>
        /// <param name="self"></param>
        /// <typeparam name="T"></typeparam>
        public static void HideWindow<T>(this UIComponent self) where T : Entity
        {
            WindowID hideWindowId = self.GetWindowIdByGeneric<T>();
            self.HideWindow(hideWindowId);
        }

        /// <summary>
        /// 根据泛型类型隐藏UI窗口
        /// </summary>
        /// <param name="self"></param>
        /// <param name="type"></param>
        public static void HideWindow(this UIComponent self, Type type)
        {
            WindowID hideWindowId = self.GetWindowIdByGeneric(type);
            self.HideWindow(hideWindowId);
        }

        /// <summary>
        /// 卸载指定的UI窗口实例
        /// </summary>
        /// <OtherParam name="id"></OtherParam>
        public static void UnLoadWindow(this UIComponent self, WindowID id, bool isDispose = true)
        {
            UIBaseWindow baseWindow = self.GetUIBaseWindow(id);
            if (null == baseWindow)
            {
                Log.Error($"UIBaseWindow WindowId {id} is null!!!");
                return;
            }

            UIEvent.Instance.GetUIEventHandler(id).BeforeUnload(baseWindow);
            if (baseWindow.IsPreLoad)
            {
                UnityEngine.Object.Destroy(baseWindow.UIPrefabGameObject);
                baseWindow.UIPrefabGameObject = null;
            }

            if (isDispose)
            {
                self.AllWindowsDic.Remove((int)id);
                self.VisibleWindowsDic.Remove((int)id);
                baseWindow.Dispose();
            }
        }

        /// <summary>
        /// 根据泛型类型卸载UI窗口实例
        /// </summary>
        /// <param name="self"></param>
        /// <typeparam name="T"></typeparam>
        public static void UnLoadWindow<T>(this UIComponent self) where T : Entity, IUILogic
        {
            WindowID hideWindowId = self.GetWindowIdByGeneric<T>();
            self.UnLoadWindow(hideWindowId);
        }

        private static async ETTask<UIBaseWindow> ShowBaseWindowAsync(this UIComponent self, WindowID id)
        {
            CoroutineLock coroutineLock = null;
            try
            {
                coroutineLock = await self.Fiber().Root.GetComponent<CoroutineLockComponent>().Wait(CoroutineLockType.LoadUIBaseWindows, (int)id);
                UIBaseWindow baseWindow = self.GetUIBaseWindow(id);
                if (baseWindow == null)
                {
                    if (UIPath.Instance.WindowPrefabPath.ContainsKey((int)id))
                    {
                        baseWindow = self.AddChild<UIBaseWindow>();
                        baseWindow.WindowID = id;
                        await self.PreLoadWindowsItemAsync(baseWindow.WindowID);
                        await self.LoadBaseWindowsAsync(baseWindow);
                    }
                }

                if (!baseWindow.IsPreLoad)
                {
                    await self.PreLoadWindowsItemAsync(baseWindow.WindowID);
                    await self.LoadBaseWindowsAsync(baseWindow);
                }

                return baseWindow;
            }
            catch (Exception e)
            {
                Log.Error(e);
                return default;
            }
            finally
            {
                coroutineLock?.Dispose();
            }
        }

        private static void RealShowWindow(this UIComponent self, UIBaseWindow baseWindow, WindowID id, ShowWindowData showData = null)
        {
            var root = self.GetTargetRoot(baseWindow.WindowData.WindowType);
            if (baseWindow.WindowData.NeedMask)
            {
                self.GetComponent<UIMask>().SetActive(root);
            }

            baseWindow.UITransform.SetAsLastSibling();
            baseWindow.UIPrefabGameObject?.SetActive(true);
            UIEvent.Instance.GetUIEventHandler(id).OnShowWindow(baseWindow, showData);
            if (self.ShowWindowsList.Count > 0)
            {
                var win = self.ShowWindowsList[0];
                UIEvent.Instance.GetUIEventHandler(win.WindowID).OnUnFocus(win);
                Log.Info("<color=magenta>### window OnUnFocus </color>" + win.WindowID);
            }

            UIEvent.Instance.GetUIEventHandler(id).OnFocus(baseWindow);

            self.ShowWindowsList.Add(baseWindow);
            self.VisibleWindowsDic[(int)id] = baseWindow;
            Log.Info("<color=magenta>### current Navigation window </color>" + baseWindow.WindowID);
        }

        public static void Destroy(this UIComponent self)
        {
            self.CloseAllWindow();
        }

        /// <summary>
        /// 根据窗口Id获取UIBaseWindow
        /// </summary>
        /// <param name="self"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private static UIBaseWindow GetUIBaseWindow(this UIComponent self, WindowID id)
        {
            if (self.AllWindowsDic.ContainsKey((int)id))
            {
                return self.AllWindowsDic[(int)id];
            }

            return null;
        }

        /// <summary>
        /// 根据窗口Id隐藏并完全关闭卸载UI窗口实例
        /// </summary>
        /// <param name="self"></param>
        /// <param name="windowId"></param>
        public static void CloseWindow(this UIComponent self, WindowID windowId)
        {
            if (!self.VisibleWindowsDic.ContainsKey((int)windowId))
            {
                return;
            }

            self.HideWindow(windowId);
            self.UnLoadWindow(windowId);
            Log.Info("<color=magenta>## close window without PopNavigationWindow() ##</color>");
        }

        /// <summary>
        /// 根据窗口泛型类型隐藏并完全关闭卸载UI窗口实例
        /// </summary>
        /// <param name="self"></param>
        /// <typeparam name="T"></typeparam>
        public static void CloseWindow<T>(this UIComponent self) where T : Entity, IUILogic
        {
            WindowID hideWindowId = self.GetWindowIdByGeneric<T>();
            self.CloseWindow(hideWindowId);
        }

        /// <summary>
        /// 关闭并卸载所有的窗口实例
        /// </summary>
        /// <param name="self"></param>
        public static void CloseAllWindow(this UIComponent self)
        {
            self.IsPopStackWndStatus = false;
            if (self.AllWindowsDic == null)
            {
                return;
            }

            foreach (KeyValuePair<int, UIBaseWindow> window in self.AllWindowsDic)
            {
                UIBaseWindow baseWindow = window.Value;
                if (baseWindow == null || baseWindow.IsDisposed)
                {
                    continue;
                }

                self.HideWindow(baseWindow.WindowID);
                self.UnLoadWindow(baseWindow.WindowID, false);
                baseWindow.Dispose();
            }

            self.ShowWindowsList.Clear();
            self.AllWindowsDic.Clear();
            self.VisibleWindowsDic.Clear();
            self.StackWindowsQueue.Clear();
            self.UIBaseWindowlistCached.Clear();
        }

        /// <summary>
        /// 隐藏所有已显示的窗口
        /// </summary>
        /// <param name="self"></param>
        /// <param name="includeFixed"></param>
        public static void HideAllShownWindow(this UIComponent self, bool includeFixed = false)
        {
            self.IsPopStackWndStatus = false;
            self.UIBaseWindowlistCached.Clear();
            foreach (KeyValuePair<int, UIBaseWindow> window in self.VisibleWindowsDic)
            {
                if (window.Value.WindowData.WindowType == UIWindowType.Fixed && !includeFixed)
                    continue;
                if (window.Value.IsDisposed)
                {
                    continue;
                }

                self.UIBaseWindowlistCached.Add((WindowID)window.Key);
                window.Value.UIPrefabGameObject?.SetActive(false);
                UIEvent.Instance.GetUIEventHandler(window.Value.WindowID).OnHideWindow(window.Value);
            }

            if (self.UIBaseWindowlistCached.Count > 0)
            {
                for (int i = 0; i < self.UIBaseWindowlistCached.Count; i++)
                {
                    self.VisibleWindowsDic.Remove((int)self.UIBaseWindowlistCached[i]);
                }
            }

            self.StackWindowsQueue.Clear();
        }

        private static Transform GetTargetRoot(this UIComponent self, UIWindowType type)
        {
            switch (type)
            {
                case UIWindowType.Normal:
                    return Global.Instance.NormalRoot;
                case UIWindowType.Fixed:
                    return Global.Instance.FixedRoot;
                case UIWindowType.PopUp:
                    return Global.Instance.PopUpRoot;
                case UIWindowType.Other:
                    return Global.Instance.OtherRoot;
                default:
                    Log.Error("uiroot type is error: " + type);
                    return null;
            }
        }

        /// <summary>
        /// 异步加载UI窗口实例
        /// </summary>
        private static async ETTask LoadBaseWindowsAsync(this UIComponent self, UIBaseWindow baseWindow)
        {
            if (!UIPath.Instance.WindowPrefabPath.TryGetValue((int)baseWindow.WindowID, out string value))
            {
                Log.Error($"{baseWindow.WindowID} is not Exist!");
                return;
            }

            GameObject go = await self.Root().GetComponent<ResourcesLoaderComponent>().LoadAssetAsync<GameObject>(value.ToUIPath());
            baseWindow.UIPrefabGameObject = UnityEngine.Object.Instantiate(go);
            baseWindow.UIPrefabGameObject.name = go.name;

            UIEvent.Instance.GetUIEventHandler(baseWindow.WindowID).OnInitWindowCoreData(baseWindow);

            var root = self.GetTargetRoot(baseWindow.WindowData.WindowType);
            baseWindow.SetRoot(root);

            UIEvent.Instance.GetUIEventHandler(baseWindow.WindowID).OnInitComponent(baseWindow);
            UIEvent.Instance.GetUIEventHandler(baseWindow.WindowID).OnRegisterUIEvent(baseWindow);

            self.AllWindowsDic[(int)baseWindow.WindowID] = baseWindow;
        }
        
        private static async ETTask PreLoadWindowsItemAsync(this UIComponent self,  WindowID windowID)
        {
            if (!WindowItemRes.WindowItemResDictionary.TryGetValue(windowID, out var itemResNames))
            {
                return;
            }

            if (itemResNames.Count <= 0)
            {
                return;
            }
            
            using ListComponent<ETTask> list = ListComponent<ETTask>.Create();
            
            foreach (var poolName in itemResNames)
            {
                Log.Info($"开始预加载页面Item  {windowID} -> {poolName}");
                list.Add(GameObjectPoolHelper.InitPoolFormGamObjectAsync(poolName, 3));
            }
            
            try
            {
                await ETTaskHelper.WaitAll(list);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }
    }
}