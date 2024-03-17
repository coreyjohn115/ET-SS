using System.Collections.Generic;
using Lean.Touch;
using UnityEngine;

namespace ET.Client
{
    [EntitySystemOf(typeof (PcInputComponent))]
    [FriendOf(typeof (PcInputComponent))]
    public static partial class PcInputComponentSystem
    {
        [EntitySystem]
        private static void Awake(this PcInputComponent self)
        {
            self.AddHotKey("Alpha1", KeyCode.Alpha1);
            self.AddHotKey("Alpha2", KeyCode.Alpha2);
            self.AddHotKey("Alpha3", KeyCode.Alpha3);
            self.AddHotKey("Alpha4", KeyCode.Alpha4);
            self.AddHotKey("Alpha5", KeyCode.Alpha5);
            self.AddHotKey("Alpha6", KeyCode.Alpha6);
        }

        [EntitySystem]
        private static void Update(this PcInputComponent self)
        {
            // Get the fingers we want to use
            var fingers = self.Use.GetFingers();

            //缩放
            var pinchScale = LeanGesture.GetPinchRatio(fingers, 0.1f);
            if (pinchScale != 1.0f)
            {
                self.Scene().GetComponent<CameraComponent>().Scale(pinchScale - 1);
            }

            //切换视角
            if (Input.GetKeyDown(KeyCode.V))
            {
                self.Scene().GetComponent<CameraComponent>().ChangeCfg();
            }

            if (Input.anyKeyDown)
            {
                // foreach (KeyCode keyCode in System.Enum.GetValues(typeof (KeyCode)))
                // {
                //     if (Input.GetKeyDown(keyCode))
                //     {
                //         Log.Info("当前按下的按键类型：" + keyCode);
                //         break;
                //     }
                // }
            }
        }

        private static void AddHotKey(this PcInputComponent self, string name, params KeyCode[] keys)
        {
            if (self.hotKeyDict.TryGetValue(name, out var hotKey))
            {
                return;
            }

            hotKey = new HotKey() { Keys = new List<KeyCode>() };
            foreach (var code in keys)
            {
                hotKey.Keys.Add(code);
            }

            self.hotKeyDict.Add(name, hotKey);
        }

        private static void CheckHotKey(this PcInputComponent self)
        {
            foreach (var pair in self.hotKeyDict)
            {
                if (pair.Value.Check())
                {
                    EventSystem.Instance.Publish(self.Scene(), new HotKeyEvent() { Name = pair.Key, Code = pair.Value.Keys[0] });
                }
            }
        }

        private static Vector3 AxisToSceneDir(float h, float v)
        {
            Vector3 moveDir = Vector3.zero;
            moveDir.Set(h, 0, v);
            Vector3 vDir = Global.Instance.MainCamera.transform.rotation.eulerAngles;
            vDir.x = 0;
            Quaternion qDir = Quaternion.Euler(vDir);
            moveDir = qDir * moveDir;
            moveDir.Normalize();
            return moveDir;
        }

        [EntitySystem]
        private static void LateUpdate(this PcInputComponent self)
        {
            self.CheckHotKey();

            if (Input.GetKey(KeyCode.Q))
            {
                self.Scene().GetComponent<CameraComponent>().Yaw(2f);
            }
            else if (Input.GetKey(KeyCode.E))
            {
                self.Scene().GetComponent<CameraComponent>().Yaw(-2f);
            }

            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            bool bMoving = (h != 0.0f) || (v != 0.0f);
            if (bMoving)
            {
                self.moving = true;
                Vector3 dir = AxisToSceneDir(h, v);
                Unit myUnit = UnitHelper.GetMyUnitFromClientScene(self.Root());
                // C2M_PathfindingResult c2MPathfindingResult = C2M_PathfindingResult.Create(true);
                // c2MPathfindingResult.Position = myUnit.Position + new float3(dir * (5 * Time.smoothDeltaTime));
                // self.Root().GetComponent<ClientSenderCompnent>().Send(c2MPathfindingResult);
                Log.Info(dir);
            }
            else if (self.moving)
            {
                self.moving = false;
                self.Root().GetComponent<ClientSenderComponent>().Send(new C2M_Stop());
            }
        }
    }
}