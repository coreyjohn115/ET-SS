using System;
using System.Collections.Generic;
using System.Linq;

namespace ET.Server;

[EntitySystemOf(typeof (SkillComponent))]
[FriendOf(typeof (SkillComponent))]
[FriendOf(typeof (SkillUnit))]
public static partial class SkillComponentSystem
{
    [EntitySystem]
    private static void Awake(this SkillComponent self)
    {
    }

    [EntitySystem]
    private static void Destroy(this SkillComponent self)
    {
        self.Scene().GetComponent<TimerComponent>().Remove(ref self.singTimer);
        self.Scene().GetComponent<TimerComponent>().Remove(ref self.effectTimer);
    }

    [Invoke(TimerInvokeType.SkillSing)]
    private class SkillSingTimer: ATimer<SkillComponent>
    {
        protected override void Run(SkillComponent self)
        {
            self.ProcessSkill();
        }
    }

    [Invoke(TimerInvokeType.SKillEffect)]
    private class SkillEffectTimer: ATimer<SkillComponent>
    {
        protected override void Run(SkillComponent self)
        {
            self.ProcessSKillEffect();
        }
    }

    private static List<Unit> GetHurtList(this SkillComponent self, SkillUnit skill)
    {
        var effectCfg = skill.Config.EffectList.Get(self.oft);
        switch (effectCfg.RangeType)
        {
            case 0:
                return new List<Unit>();
            case (int)RangeType.UseLast:
                return self.dyna.LastHurtList;
            default:
                return self.GetParent<Unit>().GetAttackList((FocusType)effectCfg.Dst,
                    (RangeType)effectCfg.RangeType,
                    self.dyna.Direct,
                    self.GetParent<Unit>().GetUnitsById(self.dyna.DstList),
                    self.dyna.DstPosition,
                    0,
                    effectCfg[0], effectCfg[1], effectCfg[2], effectCfg[3],
                    skill.MasterConfig.MaxDistance);
        }
    }

    private static void ProcessSKillEffect(this SkillComponent self)
    {
        if (self.skillEffect == default)
        {
            return;
        }

        var skill = self.skillDict.Get(self.usingSkillId);
        var unitList = self.GetHurtList(skill);
        var pkg = self.skillEffect.Run(self, skill, unitList, self.dyna);
        self.dyna.LastHurtList = unitList;
        if (pkg != null)
        {
            self.GetParent<Unit>().BroadCastHurt(self.usingSkillId, pkg);
        }

        self.oft++;
        self.ProcessSkill();
    }

    private static void ProcessSkill(this SkillComponent self)
    {
        var skill = self.skillDict.Get(self.usingSkillId);
        if (skill == default)
        {
            return;
        }

        //天赋修改技能
        var effectCfg = skill.Config.EffectList.Get(self.oft);
        if (effectCfg == default)
        {
            self.UseSuccess();
            return;
        }

        if (!SkillEffectSingleton.Instance.SkillEffectDict.TryGetValue(effectCfg.Cmd, out var t))
        {
            Log.Error($"获取技能效果失败: {effectCfg.Cmd}");
            return;
        }

        //技能效果是顺序执行的, 当前效果执行完才会切换下一个
        self.skillEffect = Activator.CreateInstance(t) as ASkillEffect;
        self.skillEffect.SetEffectArg(effectCfg);
        if (effectCfg.Ms > 0)
        {
            self.effectTimer = self.Scene().GetComponent<TimerComponent>().NewOnceTimer(effectCfg.Ms, TimerInvokeType.SKillEffect, self);
            return;
        }

        self.ProcessSKillEffect();
    }

    private static MessageReturn CheckCondition(this SkillComponent self, SkillUnit skill)
    {
        return MessageReturn.Success();
    }

    public static MessageReturn UseSKill(this SkillComponent self, int id, SkillDyna dyna)
    {
        var skill = self.skillDict.Get(id);
        if (skill == default)
        {
            return MessageReturn.Create(ErrorCode.ERR_CantFindCfg);
        }

        var unit = self.GetParent<Unit>();
        if (!unit.IsAlive())
        {
            return MessageReturn.Create(ErrorCode.ERR_UnitDead);
        }

        switch (skill.MasterConfig.RangeType)
        {
            case (int)RangeType.Single:
                if (dyna.DstList.IsNullOrEmpty())
                {
                    return MessageReturn.Create(ErrorCode.ERR_InputInvaid);
                }

                break;
            case (int)RangeType.SelfLine:
            case (int)RangeType.DstLine:
                if (dyna.DstPosition.Count == 0)
                {
                    return MessageReturn.Create(ErrorCode.ERR_InputInvaid);
                }

                break;
        }

        var ret = self.CheckCondition(skill);
        if (ret.Errno != ErrorCode.ERR_Success)
        {
            return ret;
        }

        int cdTime = 0;
        if (skill.Config.ColdTime > 0 && self.reduceCdRate < 10000)
        {
            cdTime = Math.Max(skill.Config.ColdTime * (10000 - self.reduceCdRate) / 10000, 0);
        }

        skill.cdList.Sort((l, r) => l.CompareTo(r));
        skill.cdList[0] = Math.Max(TimeInfo.Instance.FrameTime, skill.cdList.Last()) + cdTime;

        if (cdTime > 0)
        {
            M2C_UpdateSkill pkg = M2C_UpdateSkill.Create();
            pkg.CdList.AddRange(skill.cdList);
            unit.SendToClient(pkg);
        }

        M2C_UseSkill useSkill = M2C_UseSkill.Create();
        useSkill.Id = id;
        useSkill.RoleId = unit.Id;
        useSkill.Position = unit.Position;
        if (dyna.DstList != null)
        {
            useSkill.DstList.AddRange(dyna.DstList);
        }

        if (dyna.DstPosition != null)
        {
            useSkill.DstPosition.AddRange(dyna.DstPosition);
        }

        useSkill.Direct = dyna.Direct;
        MapMessageHelper.Broadcast(unit, useSkill);

        self.dyna = dyna;
        self.oft = 0;
        self.usingSkillId = id;
        self.singTimer = self.Scene().GetComponent<TimerComponent>().NewOnceTimer(skill.Config.SingTime, TimerInvokeType.SkillSing, self);

        return MessageReturn.Success();
    }

    public static bool BreakSkill(this SkillComponent self, bool isForce, params int[] classify)
    {
        if (self.singTimer > 0 || self.effectTimer > 0)
        {
            if (!isForce)
            {
                SkillUnit skill = self.skillDict.Get(self.usingSkillId);
                foreach (int c in classify)
                {
                    if (skill.MasterConfig.Interrupt.Exists(c))
                    {
                        isForce = true;
                        break;
                    }
                }
            }

            if (!isForce)
            {
                return false;
            }

            self.Scene().GetComponent<TimerComponent>().Remove(ref self.singTimer);
            self.Scene().GetComponent<TimerComponent>().Remove(ref self.effectTimer);
            var unit = self.GetParent<Unit>();
            MapMessageHelper.Broadcast(unit, new M2C_BreakSkill() { Id = self.usingSkillId, RoleId = unit.Id });
            self.UseSuccess();
        }

        return false;
    }

    private static void UseSuccess(this SkillComponent self)
    {
        self.lastSkillId = self.usingSkillId;
        self.usingSkillId = 0;
        self.skillEffect = default;
        self.dyna = default;
    }
}