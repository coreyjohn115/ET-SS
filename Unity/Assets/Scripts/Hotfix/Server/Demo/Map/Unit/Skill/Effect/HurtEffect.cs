using System.Collections.Generic;

namespace ET.Server
{
    /// <summary>
    /// 伤害
    /// 技能发挥比例,最大数量,视图ID
    /// </summary>
    [Skill("Hurt")]
    public class HurtEffect: ASkillEffect
    {
        public override HurtPkg Run(SkillComponent self, SkillUnit skill, List<Unit> RoleList, SkillDyna dyna)
        {
            var pkg = new HurtPkg() { ViewCmd = this.EffectArg.Args[2].ToString() };
            pkg.HurtInfos = HurtHelper.StandHurt(self.GetParent<Unit>(),
                RoleList,
                (int)skill.Id,
                this.EffectArg.Args[0],
                0,
                this.EffectArg.Args[1],
                skill.MasterConfig.HateBase,
                skill.MasterConfig.HateRate,
                this.EffectArg.SubList,
                dyna,
                skill.MasterConfig.Element);
            return pkg;
        }
    }
}