﻿namespace ET.Server
{
    /// <summary>
    /// 免疫
    /// </summary>
    [Buff("ImmUnity")]
    public class ImmUnityEffect: ABuffEffect
    {
        protected override void OnCreate(BuffComponent self, BuffUnit buff, EffectArg effectArg)
        {
            self.GetParent<Unit>().GetComponent<AbilityComponent>().AddAbility((int) RoleAbility.ImmUnity);
        }

        protected override void OnRemove(BuffComponent self, BuffUnit buff, EffectArg effectArg)
        {
            self.GetParent<Unit>().GetComponent<AbilityComponent>().RemoveAbility((int) RoleAbility.ImmUnity);
        }
    }
}