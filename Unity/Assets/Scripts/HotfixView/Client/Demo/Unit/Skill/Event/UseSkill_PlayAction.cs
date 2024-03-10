namespace ET.Client
{
    [Event(SceneType.Current)]
    public class UseSkill_PlayAction: AEvent<Scene, UseSKill>
    {
        protected override async ETTask Run(Scene scene, UseSKill e)
        {
            await ETTask.CompletedTask;
        }
    }
}