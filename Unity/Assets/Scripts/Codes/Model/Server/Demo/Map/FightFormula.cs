﻿namespace ET.Server;

/// <summary>
/// 战斗公式
/// </summary>
[ComponentOf(typeof (Scene))]
public class FightFormula: Entity, IAwake, ILoad
{
    public int CirtDamage { get; set; }
    
    public float K { get; set; }
}