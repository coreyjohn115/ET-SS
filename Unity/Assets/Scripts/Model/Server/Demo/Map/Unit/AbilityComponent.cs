﻿namespace ET.Server;

[ComponentOf(typeof (Unit))]
public class AbilityComponent: Entity, IAwake
{
    /// <summary>
    /// 能力值
    /// </summary>
    public int Value;

    /// <summary>
    /// 能力列表
    /// </summary>
    public int[] AbilityList { get; } = new int[(int) RoleAbility.End];
}