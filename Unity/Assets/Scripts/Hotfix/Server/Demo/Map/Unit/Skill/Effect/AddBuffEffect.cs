using System.Collections.Generic;

namespace ET.Server;

/// <summary>
/// 添加Buff
/// buffId,概率,buff有效时间,最大数量,对象子类型
/// </summary>
[Skill("AddBuff")]
public class AddBuffEffect: ASkillEffect
{
    public override HurtPkg Run(SkillComponent self, SkillUnit skill, List<Unit> RoleList, SkillDyna dyna)
    {
        return default;
    }
}