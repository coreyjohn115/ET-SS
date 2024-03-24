using System;
using System.Collections.Generic;
using Unity.Mathematics;

namespace ET.Server
{
    [FriendOf(typeof (MoveComponent))]
    [FriendOf(typeof (NumericComponent))]
    public static partial class UnitHelper
    {
        public static async ETTask<(bool, Unit)> LoadUnit(Player player)
        {
            Unit unit = await CacheHelper.GetCache(player.Scene(), player.Id);
            bool isNewPlayer = unit == null;
            if (isNewPlayer)
            {
                unit = UnitFactory.Create(player.Scene(), player.Id, UnitType.Player);
            }

            return (isNewPlayer, unit);
        }

        public static async ETTask InitUnit(Unit unit, bool isNewPlayer)
        {
            string[] list = { "UnitExtra", "UnitLucky", "ItemComponent", "TaskComponent", "BuffComponent", "AbilityComponent", "ShieldComponent", };
            foreach (string s in list)
            {
                if (unit.GetComponentByName(s) == null)
                {
                    var t = CodeTypes.Instance.GetType($"ET.Server.{s}");
                    unit.AddComponent(t);
                    isNewPlayer = true;
                }
            }

            if (isNewPlayer)
            {
                CacheHelper.UpdateAllCache(unit.Scene(), unit);
            }

            await ETTask.CompletedTask;
        }

        public static UnitInfo CreateUnitInfo(Unit unit)
        {
            UnitInfo unitInfo = new();
            NumericComponent nc = unit.GetComponent<NumericComponent>();
            unitInfo.UnitId = unit.Id;
            unitInfo.ConfigId = unit.ConfigId;
            unitInfo.Type = (int)unit.Type();
            unitInfo.Position = unit.Position;
            unitInfo.Forward = unit.Forward;

            MoveComponent moveComponent = unit.GetComponent<MoveComponent>();
            if (moveComponent)
            {
                if (!moveComponent.IsArrived())
                {
                    unitInfo.MoveInfo = new MoveInfo();
                    unitInfo.MoveInfo.Points.Add(unit.Position);
                    for (int i = moveComponent.N; i < moveComponent.Targets.Count; ++i)
                    {
                        float3 pos = moveComponent.Targets[i];
                        unitInfo.MoveInfo.Points.Add(pos);
                    }
                }
            }
            
            AbilityComponent abilityComponent = unit.GetComponent<AbilityComponent>();
            if (abilityComponent)
            {
                unitInfo.FightData = new FightDataInfo();
                unitInfo.FightData.Ability = abilityComponent.Ability;
            }

            foreach ((int key, long value) in nc.NumericDic)
            {
                unitInfo.KV.Add(key, value);
            }

            return unitInfo;
        }

        public static bool IsAlive(this Unit self)
        {
            if (self == null || self.IsDisposed)
            {
                return false;
            }

            var numeric = self.GetComponent<NumericComponent>();
            return numeric.GetAsLong(NumericType.Hp) > 0;
        }

        public static void AddHp(this Unit self, long hp, long srcId = 0, int id = 0)
        {
            if (!self.IsAlive() || hp == 0)
            {
                return;
            }

            srcId = srcId == 0? self.Id : srcId;
            long oldHp = self.GetAttrValue(NumericType.Hp);
            long newHp = Math.Max(Math.Min(oldHp + hp, self.GetAttrValue(NumericType.MaxHp)), 0);
            self.GetComponent<NumericComponent>().Set(NumericType.Hp, newHp);
            if (oldHp != newHp)
            {
                EventSystem.Instance.Publish(self.Scene(), new UnitHpChange() { Unit = self });
            }

            if (hp < 0 && id > 0)
            {
                Unit attacker = self.Scene().GetComponent<UnitComponent>().Get(srcId);
                EventSystem.Instance.Publish(self.Scene(), new UnitBeHurt() { Unit = self, Attacker = attacker, Id = id, Hurt = oldHp - newHp });
            }

            if (hp < 0)
            {
                if (!self.IsAlive())
                {
                    EventSystem.Instance.Publish(self.Scene(), new UnitDead() { Unit = self, Killer = srcId, Id = id });
                }
            }
            else
            {
                Unit attacker = self.Scene().GetComponent<UnitComponent>().Get(srcId);
                EventSystem.Instance.Publish(self.Scene(), new UnitAddHp() { Unit = self, Attacker = attacker, Hp = hp, RealHp = newHp - oldHp });
            }
        }

        /// <summary>
        /// 获取看见unit的玩家，主要用于广播
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static Dictionary<long, AOIEntity> GetBeSeePlayers(this Unit self)
        {
            return self.GetComponent<AOIEntity>().GetBeSeePlayers();
        }

        /// <summary>
        /// 获取unit看见的玩家
        /// </summary>
        /// <param name="self"></param>
        /// <returns></returns>
        public static List<Unit> GetSeePlayers(this Unit self)
        {
            var dict = self.GetComponent<AOIEntity>().GetSeePlayers();
            var list = new List<Unit>();
            foreach (var (_, aoiEntity) in dict)
            {
                list.Add(aoiEntity.Unit);
            }

            return list;
        }

        public static List<Unit> GetFoucslayers(this Unit self, FocusType fT, bool isCanAttack = true)
        {
            var list = self.GetSeePlayers();
            return list;
        }

        /// <summary>
        /// 获取修正位置
        /// </summary>
        /// <param name="srcX">x坐标</param>
        /// <param name="srcZ">y坐标</param>
        /// <param name="direct">方向</param>
        /// <param name="repairPos">修改距离</param>
        /// <returns></returns>
        public static Pair<float, float> GetRepairPos(float srcX, float srcZ, float direct, float repairPos)
        {
            if (repairPos != 0)
            {
                srcX = (float)Math.Floor(srcX + Math.Cos(Math.Atan(direct)) * repairPos);
                srcZ = (float)Math.Floor(srcZ + Math.Cos(Math.Atan(direct)) * repairPos);
            }

            return new Pair<float, float>(srcX, srcZ);
        }

        public static Unit GetOwner(this Unit self)
        {
            return default;
        }

        /// <summary>
        /// 重置当前血量为最大血量
        /// </summary>
        /// <param name="self"></param>
        public static void ResetHp(this Unit self)
        {
            var numeric = self.GetComponent<NumericComponent>();
            numeric.Set(NumericType.Hp, numeric.GetAsLong(NumericType.MaxHp));
        }

        /// <summary>
        /// 获取当前角色的的属性值
        /// </summary>
        /// <param name="self"></param>
        /// <param name="attrType">属性类型</param>
        /// <returns></returns>
        public static long GetAttrValue(this Unit self, int attrType)
        {
            var numeric = self.GetComponent<NumericComponent>();
            return numeric.GetAsLong(attrType);
        }

        public static List<Unit> GetUnitsById(this Unit self, List<long> roleIds)
        {
            List<Unit> list = new List<Unit>();
            var unitComponent = self.Scene().GetComponent<UnitComponent>();
            foreach (var roleId in roleIds)
            {
                Unit unit = unitComponent.Get(roleId);
                if (unit != null)
                {
                    list.Add(unit);
                }
            }

            return list;
        }

        public static void AddHate(this Unit self, long roleId, long hate)
        {
        }

        /// <summary>
        /// 创建受伤参数
        /// </summary>
        /// <param name="self"></param>
        /// <param name="list"></param>
        /// <param name="isPhysics"></param>
        /// <returns></returns>
        public static HurtArgs CreateHurtArgs(BuffComponent self, List<HurtInfo> list, bool isPhysics = false)
        {
            var args = self.GetComponent<HurtArgs>();
            if (args != null)
            {
                return args;
            }

            args = self.AddComponent<HurtArgs>();
            args.IsPhysics = false;
            if (list is { Count: > 0 })
            {
                foreach (var hurtInfo in list)
                {
                    args.HurtList.Add(hurtInfo);
                }
            }

            return args;
        }
    }
}