﻿namespace ET.Server;

[EntitySystemOf(typeof(FightFormula))]
public static partial class FightFormulaSystem
{
    [EntitySystem]
    private static void Awake(this FightFormula self)
    {
        self.CirtDamage = 25000;
        self.K = 0.5f;
    }
    
    [EntitySystem]
    private static void Load(this FightFormula self)
    {
        self.Awake();
    }

    /// <summary>
    /// 计算伤害
    /// </summary>
    /// <returns>返回伤害结果</returns>
    public static long CalcHurt(this FightFormula self,
    NumericComponent attack,
    NumericComponent dst,
    int extraAttack,
    int skillAdjust = 10000,
    int element = 0,
    int judgment = 0,
    bool isCrit = false,
    bool isSymptom = false)
    {
        int dmgRate = isCrit ? self.CirtDamage : 10000;
        var att = attack.GetAsLong(NumericType.Attack) + extraAttack;
        var defense = dst.GetAsLong(NumericType.Defense);
        var hp = attack.GetAsLong(NumericType.MaxHp);
        if (defense == 0)
        {
            return 1;
        }

        float d2;
        float skillAdj = skillAdjust / 10000f;
        switch (judgment)
        {
            case 1:
                d2 = att * att * skillAdj / (self.K * defense);
                break;
            case 2:
                d2 = defense * defense * skillAdj * skillAdj / (self.K * defense);
                break;
            case 3:
                d2 = hp * hp * skillAdj * skillAdj / (self.K * defense);
                break;
            default:
                return 0;
        }

        if (isSymptom)
        {
            d2 *= (1 + attack.GetAsLong(NumericType.Symptom) / 200f);
        }

        var d4 = d2 * dmgRate / 10000;
        var hurtRate = (1 + attack.GetAsLong(NumericType.HurtAddRate) / 10000f) * (1 - dst.GetAsLong(NumericType.HurtReduceRate) / 10000f);

        return (d4 * hurtRate * self.GetElementRate(attack, dst, element) + 0.0001f).Ceil();
    }

    //元素伤害比例
    private static float GetElementRate(this FightFormula self, NumericComponent attack,
    NumericComponent dst, int element)
    {
        var el = (ElementType)element;
        switch (el)
        {
            case ElementType.None:
                return 1;
            case ElementType.Fire:
                return 1 + (attack.GetAsLong(NumericType.FireAdd) - dst.GetAsLong(NumericType.FireAvoid)) / 200f;
            case ElementType.Thunder:
                return 1 + (attack.GetAsLong(NumericType.ThunderAdd) - dst.GetAsLong(NumericType.ThunderAvoid)) / 200f;
            case ElementType.Ice:
                return 1 + (attack.GetAsLong(NumericType.IceAdd) - dst.GetAsLong(NumericType.IceAvoid)) / 200f;
            default:
                return 1;
        }
    }

    /// <summary>
    /// 是否暴击
    /// </summary>
    /// <returns></returns>
    public static bool IsCrit(this FightFormula self, Unit target)
    {
        return target.GetComponent<NumericComponent>().GetAsLong(NumericType.CirtRate).IsHit();
    }

    /// <summary>
    /// 是否暴击
    /// </summary>
    /// <returns></returns>
    public static bool IsCrit(this FightFormula self, NumericComponent target)
    {
        return target.GetAsLong(NumericType.CirtRate).IsHit();
    }

    /// <summary>
    /// 是否命中
    /// </summary>
    /// <returns></returns>
    public static bool IsHit(this FightFormula self, Unit attack, Unit dst)
    {
        var hitRate = attack.GetComponent<NumericComponent>().GetAsLong(NumericType.HitRate);
        var avoidRate = dst.GetComponent<NumericComponent>().GetAsLong(NumericType.AvoidRate);

        return (hitRate - avoidRate).IsHit();
    }

    /// <summary>
    /// 是否命中
    /// </summary>
    /// <returns></returns>
    public static bool IsHit(this FightFormula self, NumericComponent attack, NumericComponent dst)
    {
        var hitRate = attack.GetAsLong(NumericType.HitRate);
        var avoidRate = dst.GetAsLong(NumericType.AvoidRate);

        return (hitRate - avoidRate).IsHit();
    }
}