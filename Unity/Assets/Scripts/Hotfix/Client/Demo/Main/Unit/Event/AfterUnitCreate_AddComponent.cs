namespace ET.Client
{
    [Event(SceneType.Current)]
    public class AfterUnitCreate_AddComponent: AEvent<Scene, AfterUnitCreate>
    {
        protected override async ETTask Run(Scene scene, AfterUnitCreate e)
        {
            await ETTask.CompletedTask;
            UnitType t = (UnitType)e.Unit.Config().Type;
            switch (t)
            {
                case UnitType.Player:
                    e.Unit.AddComponent<ClientSkillComponent>();
                    e.Unit.AddComponent<ClientBuffComponent>();
                    e.Unit.AddComponent<ClientAbilityComponent>().UpdateAbility(e.UnitInfo.FightData.Ability);
                    break;
            }
        }
    }
}