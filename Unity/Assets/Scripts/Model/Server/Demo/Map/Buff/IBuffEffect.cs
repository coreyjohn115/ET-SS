﻿namespace ET.Server
{
    public interface IBuffEffect
    {
        /// <summary>
        /// 天赋是否生效过
        /// </summary>
        bool IsBsEffect {get; set;}

        void OnCheckIn();
        
        void OnCheckOut();
        
        /// <summary>
        /// Buff 创建时
        /// </summary>
        void Create(BuffComponent self, Buff buff, EffectArg effectArg);

        /// <summary>
        /// Buff 时间间隔到
        /// </summary>
        void Update(BuffComponent self, Buff buff, EffectArg effectArg);

        /// <summary>
        /// 触发Buff事件时
        /// </summary>
        /// <param name="self"></param>
        /// <param name="buffEvent">Buff事件类型</param>
        void Event(BuffComponent self, BuffEvent buffEvent, Buff buff, EffectArg effectArg);
        
        /// <summary>
        /// 计时时间到
        /// </summary>
        void TimeOut(BuffComponent self, Buff buff, EffectArg effectArg);

        /// <summary>
        /// buff 移除时
        /// </summary>
        void Remove(BuffComponent self, Buff buff, EffectArg effectArg);
    }
}