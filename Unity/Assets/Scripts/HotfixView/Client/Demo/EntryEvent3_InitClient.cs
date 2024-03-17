namespace ET.Client
{
    [Event(SceneType.Main)]
    public class EntryEvent3_InitClient: AEvent<Scene, EntryEvent3>
    {
        protected override async ETTask Run(Scene root, EntryEvent3 args)
        {
            root.AddComponent<UIComponent>();
            root.AddComponent<ResourcesLoaderComponent>();
            root.AddComponent<ClientPlayerComponent>();
            root.AddComponent<CurrentScenesComponent>();
            
            root.AddComponent<ServerInfoComponent>();
            root.AddComponent<DataSaveComponent>();
            root.AddComponent<ClientTaskComponent>();
            root.AddComponent<ClientItemComponent>();
            root.AddComponent<ClientChatComponent>();
            
            await IconHelper.LoadAtlas(root, AtlasType.Widget);
            
            // 根据配置修改掉Main Fiber的SceneType
            SceneType sceneType = EnumHelper.FromString<SceneType>(Global.Instance.GlobalConfig.AppType.ToString());
            root.SceneType = sceneType;

            await EventSystem.Instance.PublishAsync(root, new AppStartInitFinish());
        }
    }
}