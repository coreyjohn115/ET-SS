﻿using System.Collections.Generic;

namespace ET.Server
{
    public struct BuffDyna
    {
        /// <summary>
        /// 天赋技能是否生效
        /// </summary>
        public bool IsEffect { get; set; }

        public EffectArg BeEffectArg {get; set;}

        public List<object> Args { get; set; }
    }

    [ChildOf(typeof (BuffComponent))]
    public class BuffUnit: Entity, IAwake<int, long, long>
    {
        /// <summary>
        /// BuffId
        /// </summary>
        public int BuffId {get; set;}
        
        /// <summary>
        /// Buff过期时间
        /// </summary>
        public long ValidTime { get; set; }

        /// <summary>
        /// 当前Buff层级
        /// </summary>
        public int Layer { get; set; }

        /// <summary>
        /// Buff添加时间
        /// </summary>
        public long AddTime { get; set; }

        /// <summary>
        /// 上次脉冲触发时间
        /// </summary>
        public long LastUseTime { get; set; }

        /// <summary>
        /// 添加者UID
        /// </summary>
        public long AddRoleId { get; set; }

        /// <summary>
        /// 移除标志
        /// </summary>
        public bool IsRemove { get; set; }

        /// <summary>
        /// 技能Id
        /// </summary>
        public int SkillId { get; set; }

        public Dictionary<string, IBuffEffect> EffectDict { get; set; }

        /// <summary>
        /// 动态参数
        /// </summary>
        public Dictionary<string, BuffDyna> EffectMap { get; set; }

        //----------------------- 以下用于天赋动态修改的参数 ---------------------------

        /// <summary>
        /// Buff间隔时间
        /// </summary>
        public int Interval { get; set; }

        /// <summary>
        /// Buff最大层级
        /// </summary>
        public int MaxLayer { get; set; }

        /// <summary>
        /// Buff视图
        /// </summary>
        public string ViewCmd { get; set; }
        
        /// <summary>
        /// buff持续时间
        /// </summary>
        public int Ms { get; set; }

        public int MasterId {get; set;}
    }
}