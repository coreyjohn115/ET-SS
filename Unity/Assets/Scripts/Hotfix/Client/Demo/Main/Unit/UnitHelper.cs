namespace ET.Client
{
    public static partial class UnitHelper
    {
        public static Unit GetMyUnitFromClientScene(Scene root)
        {
            ClientPlayerComponent playerComponent = root.GetComponent<ClientPlayerComponent>();
            Scene currentScene = root.GetComponent<CurrentScenesComponent>().Scene;
            return currentScene.GetComponent<UnitComponent>().Get(playerComponent.MyId);
        }

        public static Unit GetUnitFromClientScene(Scene root, long id)
        {
            Scene currentScene = root.GetComponent<CurrentScenesComponent>().Scene;
            return currentScene.GetComponent<UnitComponent>().Get(id);
        }

        public static Unit GetMyUnitFromCurrentScene(Scene currentScene)
        {
            ClientPlayerComponent playerComponent = currentScene.Root().GetComponent<ClientPlayerComponent>();
            return currentScene.GetComponent<UnitComponent>().Get(playerComponent.MyId);
        }

        public static Unit GetUnitFromCurrentScene(Scene currentScene, long id)
        {
            return currentScene.GetComponent<UnitComponent>().Get(id);
        }
    }
}