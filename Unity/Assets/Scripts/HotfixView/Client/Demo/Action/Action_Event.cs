namespace ET.Client
{
    [Event(SceneType.Current)]
    public class AddBuffViewEvent: AEvent<Scene, AddBuffView>
    {
        protected override async ETTask Run(Scene scene, AddBuffView a)
        {
            await ETTask.CompletedTask;
            a.Unit.GetComponent<ActionComponent>().PlayAction(a.ViewCmd);
        }
    }

    [Event(SceneType.Current)]
    public class RemoveBuffViewEvent: AEvent<Scene, RemoveBuffView>
    {
        protected override async ETTask Run(Scene scene, RemoveBuffView a)
        {
            await ETTask.CompletedTask;
            a.Unit.GetComponent<ActionComponent>().RemoveAction(a.ViewCmd);
        }
    }
}