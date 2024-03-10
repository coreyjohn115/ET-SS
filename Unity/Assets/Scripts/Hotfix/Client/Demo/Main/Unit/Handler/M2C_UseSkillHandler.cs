namespace ET.Client
{
    [MessageHandler(SceneType.Client)]
    public class M2C_UseSkillHandler: MessageHandler<Scene, M2C_UseSkill>
    {
        protected override async ETTask Run(Scene root, M2C_UseSkill message)
        {
            Scene currentScene = root.CurrentScene();
            UnitComponent unitComponent = currentScene.GetComponent<UnitComponent>();

            Unit unit = unitComponent.Get(message.RoleId);
            if (unit)
            {
                unit.GetComponent<ClientSkillComponent>().UseSkill(message);
            }

            await ETTask.CompletedTask;
        }
    }
}