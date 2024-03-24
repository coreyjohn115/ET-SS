using MongoDB.Bson;

namespace ET.Client
{
    [Action("Animator")]
    public class AnimatorAction: AAction
    {
        public override async ETTask OnExecute(Unit unit, ActionUnit actionUnit)
        {
            var animation = unit.GetComponent<UnitGoComponent>().GetAnimator();
            if (!animation)
            {
                return;
            }

            var cfg = actionUnit.Config.GetSubConfig<AnimatorAActionConfig>();
            Log.Info(cfg.ToJson());
            foreach (var view in cfg.ViewList)
            {
                unit.GetComponent<ActionComponent>().PlayAction(view);
            }

            if (cfg.LockMove)
            {
                unit.GetComponent<ClientAbilityComponent>().RemoveAbility(RoleAbility.Move);
            }

            unit.GetComponent<AnimatorComponent>().SetTrigger("Attack");
            await ETTask.CompletedTask;
        }

        public override void OnUnExecute(Unit unit, ActionUnit actionUnit)
        {
            var cfg = actionUnit.Config.GetSubConfig<AnimatorAActionConfig>();
            if (cfg.LockMove)
            {
                unit.GetComponent<ClientAbilityComponent>().AddAbility(RoleAbility.Move);
            }
        }
    }
}