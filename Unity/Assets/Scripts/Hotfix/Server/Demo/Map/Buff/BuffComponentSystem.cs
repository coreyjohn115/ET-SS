using System;
using System.Collections.Generic;

namespace ET.Server
{
    public struct BuffCreate
    {
        public Unit Unit { get; set; }

        public BuffUnit Buff { get; set; }
    }

    public struct BuffRemove
    {
        public Unit Unit { get; set; }

        public BuffUnit Buff { get; set; }
    }

    [FriendOf(typeof (BuffComponent))]
    public static class BuffComponentSystem
    {
        public class BuffComponentUpdateSystem: UpdateSystem<BuffComponent>
        {
            protected override void Update(BuffComponent self)
            {
                self.BuffHook();
            }
        }

        public static BuffUnit GetBuff(this BuffComponent self, long id)
        {
            self.buffDict.TryGetValue(id, out BuffUnit buff);
            return buff;
        }

        public static BuffUnit GetBuff(this BuffComponent self, int idOrMasterId)
        {
            foreach (var buff in self.buffDict.Values)
            {
                if (buff.BuffId == idOrMasterId || buff.MasterId == idOrMasterId)
                {
                    return buff;
                }
            }

            return default;
        }

        public static void ProcessBuff(this BuffComponent self, BuffEvent buffEvent)
        {
            switch (buffEvent)
            {
                case BuffEvent.PerHurt:
                    self.ProcessPerHurt();
                    break;
                case BuffEvent.Hurt:
                    self.BuffHook(buffEvent);
                    break;
                case BuffEvent.HurtMagic:
                case BuffEvent.HurtPhysics:
                    self.BuffHook(buffEvent);
                    break;
                default:
                    if (self.scribeEventMap.ContainsKey((int)buffEvent))
                    {
                        self.BuffHook(buffEvent);
                    }

                    break;
            }

            self.RemoveComponent<HurtArgs>();
        }

        public static void BuffHook(this BuffComponent self, BuffEvent? buffEvent = null)
        {
            if (self.buffDict.Count == 0)
            {
                return;
            }

            var list = ObjectPool.Instance.Fetch<List<long>>();
            list.Clear();
            list.AddRange(self.buffDict.Keys);
            foreach (long id in list)
            {
                if (self.buffDict.TryGetValue(id, out BuffUnit buff))
                {
                    if (IsValid(buff))
                    {
                        if (buffEvent.HasValue)
                        {
                            self.DoBuff(buff, BuffLife.OnEvent, buffEvent);
                        }
                        else
                        {
                            if (buff.Interval <= 0 || buff.Interval + buff.LastUseTime >= TimeInfo.Instance.ServerFrameTime())
                            {
                                continue;
                            }

                            self.DoBuff(buff, BuffLife.OnUpdate);
                            buff.LastUseTime = buff.LastUseTime <= 0? TimeInfo.Instance.ServerFrameTime() : buff.LastUseTime + buff.Interval;
                        }
                    }
                    else if (buff.ValidTime > 0)
                    {
                        self.DoBuff(buff, BuffLife.OnTimeOut);
                        self.RemoveBuff(id);
                    }
                    else
                    {
                        self.RemoveBuff(id);
                    }
                }
            }

            ObjectPool.Instance.Recycle(list);
        }

        /// <summary>
        /// 添加Buff
        /// </summary>
        /// <param name="self"></param>
        /// <param name="id">BuffId</param>
        /// <param name="ms">Buff持续时间 单位毫秒</param>
        /// <param name="addRoleId"></param>
        /// <param name="skillId"></param>
        /// <param name="addLayer">添加层级</param>
        public static BuffUnit AddBuff(this BuffComponent self, int id, int ms = 0, long addRoleId = 0, int skillId = 0, int addLayer = 1)
        {
            var buffConfig = BuffConfigCategory.Instance.Get(id);
            if (buffConfig == null)
            {
                Log.Error($"buff 配置不存在 {id}");
                return default;
            }

            ms = ms == 0? buffConfig.Ms : ms;
            if (ms == 0)
            {
                Log.Error($"buff 配置时间为0 {id}");
                return default;
            }

            var playerBuff = self.GetBuff(id);
            if (playerBuff == null)
            {
                var mutexList = new HashSet<long>();
                foreach (var buff in self.buffDict.Values)
                {
                    if (buff.Id != id && IsValid(buff))
                    {
                        var cfg = BuffConfigCategory.Instance.Get(buff.BuffId);

                        // 1 为免疫所有BUFF
                        if (cfg.MutexMap.Contains(1))
                        {
                            return default;
                        }

                        foreach (var classify in buffConfig.ClassifyMap)
                        {
                            if (cfg.MutexMap.Contains(classify))
                            {
                                if (cfg.MutexLevel >= buffConfig.MutexLevel)
                                {
                                    return default;
                                }

                                mutexList.Add(buff.Id);
                            }
                        }
                    }
                }

                foreach (var dd in mutexList)
                {
                    self.RemoveBuff(dd);
                }
            }

            if (buffConfig.AddType == (int)BuffAddType.Replace && playerBuff != null)
            {
                self.RemoveBuff(playerBuff.Id);
                playerBuff = null;
            }
            else if (buffConfig.AddType == (int)BuffAddType.New)
            {
                playerBuff = null;
            }
            else if (playerBuff != null && buffConfig.AddType == (int)BuffAddType.SelfMutex)
            {
                return default;
            }
            else if (buffConfig.AddType == (int)BuffAddType.ClassifyMutex) // 类型互斥
            {
                foreach (var classify in buffConfig.ClassifyMap)
                {
                    self.RemoveBuffByClassify(classify);
                }
            }

            var isNew = playerBuff == null;
            if (playerBuff == null)
            {
                playerBuff = self.CreateBuff(id, addRoleId, ms);
            }

            if (!isNew)
            {
                if (buffConfig.AddType == (int)BuffAddType.AddTime)
                {
                    playerBuff.ValidTime = Math.Max(playerBuff.ValidTime, TimeInfo.Instance.ServerFrameTime()) + ms;
                }
                else if (buffConfig.AddType == (int)BuffAddType.ResetTime)
                {
                    playerBuff.ValidTime = TimeInfo.Instance.ServerFrameTime() + ms;
                }
                else if (buffConfig.AddType == (int)BuffAddType.Role)
                {
                    if (addRoleId != 0 && addRoleId != playerBuff.AddRoleId)
                    {
                        isNew = true;
                        playerBuff = self.CreateBuff(id, addRoleId, ms);
                    }
                }
            }

            if (isNew)
            {
                playerBuff.Layer = 0;
                playerBuff.ValidTime = TimeInfo.Instance.ServerFrameTime() + ms;
                playerBuff.AddTime = TimeInfo.Instance.ServerFrameTime();
                playerBuff.LastUseTime = TimeInfo.Instance.ServerFrameTime();
                playerBuff.Interval = buffConfig.Interval;
                playerBuff.MaxLayer = buffConfig.MaxLayer;
                playerBuff.ViewCmd = buffConfig.ViewCmd;
                playerBuff.MasterId = buffConfig.MasterId;
            }

            if (playerBuff.Layer < playerBuff.MaxLayer)
            {
                playerBuff.Layer = Math.Min(playerBuff.MaxLayer, playerBuff.Layer + addLayer);
            }

            playerBuff.SkillId = skillId;
            self.buffDict[playerBuff.Id] = playerBuff;
            if (isNew || playerBuff.Layer > 1)
            {
                self.CalcBuffClassify();
                playerBuff.AddRoleId = addRoleId == 0? playerBuff.AddRoleId : addRoleId;
                if (playerBuff.AddRoleId == 0)
                {
                    playerBuff.AddRoleId = self.GetParent<Unit>().Id;
                }

                self.DoBuff(playerBuff, BuffLife.OnCreate);
                EventSystem.Instance.Publish(self.Root(), new BuffCreate() { Unit = self.GetParent<Unit>(), Buff = playerBuff });
            }
            else
            {
                self.DoBuff(playerBuff, BuffLife.OnUpdate);
            }

            var msg = M2C_UpdateBuff.Create();
            msg.Id = playerBuff.Id;
            msg.Layer = playerBuff.Layer;
            msg.ValidTime = playerBuff.ValidTime;
            msg.RoleId = self.Id;
            msg.CfgId = id;
            self.GetParent<Unit>().SendToClient(msg);
            
            return playerBuff;
        }

        /// <summary>
        /// 通过Buff唯一Id 移除Buff
        /// </summary>
        /// <param name="self"></param>
        /// <param name="id"></param>
        public static void RemoveBuff(this BuffComponent self, long id)
        {
            if (!self.buffDict.TryGetValue(id, out var buff))
            {
                return;
            }

            if (buff.IsRemove)
            {
                return;
            }

            buff.IsRemove = true;
            self.DoBuff(buff, BuffLife.OnRemove);
            self.buffDict.Remove(id);
            self.CalcBuffClassify();

            buff.EffectDict.Clear();
            EventSystem.Instance.Publish(self.Root(), new BuffRemove() { Unit = self.GetParent<Unit>(), Buff = buff });
            self.RemoveChild(id);
            
            var msg = M2C_DelBuff.Create();
            msg.RoleId = self.Id;
            msg.Id = id;
            self.GetParent<Unit>().SendToClient(msg);
        }

        /// <summary>
        /// 通过BuffId 移除buff
        /// </summary>
        /// <param name="self"></param>
        /// <param name="id"></param>
        public static void RemoveBuff(this BuffComponent self, int id)
        {
            var list = ObjectPool.Instance.Fetch<List<long>>();
            list.Clear();
            foreach (var buff in self.buffDict.Values)
            {
                if (buff.Id == id)
                {
                    list.Add(buff.Id);
                }
            }

            foreach (var cfgId in list)
            {
                self.RemoveBuff(cfgId);
            }

            ObjectPool.Instance.Recycle(list);
        }

        /// <summary>
        /// 删除指定类型的buff
        /// </summary>
        /// <param name="self"></param>
        /// <param name="classify"></param>
        public static void RemoveBuffByClassify(this BuffComponent self, int classify)
        {
            if (self.buffDict.Count == 0)
            {
                return;
            }

            var list = ObjectPool.Instance.Fetch<List<long>>();
            list.Clear();
            list.AddRange(self.buffDict.Keys);
            foreach (var id in list)
            {
                if (self.buffDict.TryGetValue(id, out var buff))
                {
                    var buffCfg = BuffConfigCategory.Instance.Get(buff.BuffId);
                    if (buffCfg.ClassifyMap.Contains(classify))
                    {
                        self.RemoveBuff(id);
                    }
                }
            }

            ObjectPool.Instance.Recycle(list);
        }

        /// <summary>
        /// 降低Buff层级
        /// </summary>
        /// <param name="self"></param>
        /// <param name="id"></param>
        /// <param name="layer"></param>
        public static void RemoveBuffLayer(this BuffComponent self, long id, int layer = 1)
        {
            if (self.buffDict.TryGetValue(id, out var buff))
            {
                buff.Layer -= layer;
                if (buff.Layer <= 0)
                {
                    self.RemoveBuff(id);
                }
            }
        }

        public static void AddBuffTime(this BuffComponent self, long id, int ms)
        {
            if (self.buffDict.TryGetValue(id, out var buff))
            {
                buff.ValidTime += ms;
            }
        }

        /// <summary>
        /// 清除玩家身上所有Buff
        /// </summary>
        public static void ClearBuff(this BuffComponent self)
        {
            var list = ObjectPool.Instance.Fetch<List<long>>();
            list.Clear();
            list.AddRange(self.buffDict.Keys);
            foreach (var id in list)
            {
                self.RemoveBuff(id);
            }

            ObjectPool.Instance.Recycle(list);
        }

        public static void SubscribeBuffEvent(this BuffComponent self, int buffEvent)
        {
            if (!self.scribeEventMap.TryGetValue(buffEvent, out var v))
            {
                self.scribeEventMap.Add(buffEvent, 0);
            }

            self.scribeEventMap[buffEvent] = v + 1;
        }

        public static void UnSubscribeBuffEvent(this BuffComponent self, int buffEvent)
        {
            if (self.scribeEventMap.TryGetValue(buffEvent, out var v))
            {
                v -= 1;
                self.scribeEventMap[buffEvent] = v;
                if (v <= 0)
                {
                    self.scribeEventMap.Remove(buffEvent);
                }
            }
        }

        /// <summary>
        /// 是否存在指定的Buff类型
        /// </summary>
        /// <param name="self"></param>
        /// <param name="classify"></param>
        /// <returns></returns>
        public static bool ContainsClassify(this BuffComponent self, int classify)
        {
            return self.eventMap.ContainsKey(classify);
        }

        /// <summary>
        /// 是否存在指定的Buff类型
        /// </summary>
        /// <param name="self"></param>
        /// <param name="classifyList"></param>
        /// <returns></returns>
        public static bool ContainsClassify(this BuffComponent self, IEnumerable<int> classifyList)
        {
            foreach (var classify in classifyList)
            {
                if (self.eventMap.ContainsKey(classify))
                {
                    return true;
                }
            }

            return false;
        }

        private static void CalcBuffClassify(this BuffComponent self)
        {
            self.eventMap.Clear();
            foreach (BuffUnit buff in self.buffDict.Values)
            {
                var buffCfg = BuffConfigCategory.Instance.Get(buff.BuffId);
                foreach (int c in buffCfg.ClassifyMap)
                {
                    self.eventMap[c] = true;
                }
            }
        }

        private static bool IsValid(BuffUnit buff)
        {
            if (buff.Id > 0 && (buff.ValidTime >= TimeInfo.Instance.ServerFrameTime() || buff.Ms < 0))
            {
                return true;
            }

            return false;
        }

        private static BuffUnit CreateBuff(this BuffComponent self, int id, long addRoleId, int ms)
        {
            var playerBuff = self.AddChild<BuffUnit, int, long, long>(id, TimeInfo.Instance.ServerFrameTime(), addRoleId);
            playerBuff.EffectDict = new Dictionary<string, IBuffEffect>();
            playerBuff.Ms = ms;

            return playerBuff;
        }

        private static void DoBuff(this BuffComponent self, BuffUnit buff, BuffLife life, BuffEvent? buffEvent = null)
        {
            var buffCfg = BuffConfigCategory.Instance.Get(buff.BuffId);
            foreach (var effect in buffCfg.EffectList)
            {
                switch (life)
                {
                    case BuffLife.OnCreate:
                    {
                        var buffEffect = self.CreateBuffEffect(effect.Cmd);
                        if (buffEffect == null)
                        {
                            Log.Error($"创建Buff效果实例失败: {effect.Cmd}");
                            return;
                        }

                        buff.EffectDict.Add(effect.Cmd, buffEffect);
                        self.CheckBsEffect(buff, effect);
                        buffEffect.Create(self, buff, effect);
                        buffEffect.Update(self, buff, effect);
                        break;
                    }
                    case BuffLife.OnUpdate:
                    {
                        if (buff.EffectDict.TryGetValue(effect.Cmd, out var buffEffect))
                        {
                            buffEffect.Update(self, buff, effect);
                        }

                        break;
                    }
                    case BuffLife.OnEvent:
                    {
                        if (buffEvent.HasValue && buff.EffectDict.TryGetValue(effect.Cmd, out IBuffEffect buffEffect))
                        {
                            buffEffect.Event(self, buffEvent.Value, buff, effect);
                        }

                        break;
                    }
                    case BuffLife.OnTimeOut:
                    {
                        if (buff.EffectDict.TryGetValue(effect.Cmd, out IBuffEffect buffEffect))
                        {
                            buffEffect.TimeOut(self, buff, effect);
                            buffEffect.Update(self, buff, effect);
                        }

                        break;
                    }
                    case BuffLife.OnRemove:
                    {
                        if (buff.EffectDict.TryGetValue(effect.Cmd, out IBuffEffect buffEffect))
                        {
                            buffEffect.Remove(self, buff, effect);
                        }

                        break;
                    }
                }
            }
        }

        private static IBuffEffect CreateBuffEffect(this BuffComponent self, string cmd)
        {
            if (!SkillEffectSingleton.Instance.BuffEffectDict.TryGetValue(cmd, out Type t))
            {
                return default;
            }

            return Activator.CreateInstance(t) as IBuffEffect;
        }

        private static void CheckBsEffect(this BuffComponent self, BuffUnit buff, EffectArg effectArg)
        {
            buff.EffectMap ??= new Dictionary<string, BuffDyna>();
            if (!buff.EffectMap.TryGetValue(effectArg.Cmd, out var dyna))
            {
                dyna = new BuffDyna { Args = new List<object>() };
                buff.EffectMap.Add(effectArg.Cmd, dyna);
            }

            var effect = buff.EffectDict[effectArg.Cmd];
            if (!effect.IsBsEffect)
            {
                effect.IsBsEffect = true;

                // var actor = BattlePlugin.Battle.GetRole(buff.AddRoleUid);
                // if (actor)
                // {
                //     buffDyna.BeEffectArg = actor.Talent.Hook(buff.Config.MasterId, buff.Id, loopType, oft, effectArg);
                // }
            }
        }

        private static void ProcessPerHurt(this BuffComponent self)
        {
            var hurtArgs = self.GetComponent<HurtArgs>();
            foreach (var info in hurtArgs.HurtList)
            {
                var dst = self.Root().GetComponent<UnitComponent>().Get(info.Id);
                if (dst == null)
                {
                    continue;
                }

                var buffCom = dst.GetComponent<BuffComponent>();
                UnitHelper.CreateHurtArgs(buffCom, default);
                buffCom.ProcessBuff(BuffEvent.BeHurt);
                var eventType = hurtArgs.IsPhysics? BuffEvent.HurtPhysics : BuffEvent.HurtMagic;
                long oldHurt = info.Hurt;
                UnitHelper.CreateHurtArgs(buffCom, default);
                buffCom.ProcessBuff(eventType);
                if (oldHurt == info.Hurt) //护盾减伤
                {
                    continue;
                }

                UnitHelper.CreateHurtArgs(buffCom, default);
                self.ProcessBuff(BuffEvent.HurtShield);
            }
        }
    }
}