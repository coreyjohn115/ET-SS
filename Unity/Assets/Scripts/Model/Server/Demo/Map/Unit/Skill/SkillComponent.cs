using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using Unity.Mathematics;

namespace ET.Server
{
    public class HurtPkg
    {
        public List<HurtInfo> HurtInfos = new();
        public string ViewCmd;
    }

    public struct SkillDyna
    {
        public List<Unit> LastHurtList;
        public int Direct;
        public List<long> DstList;
        public List<float3> DstPosition;
    }

    [ComponentOf(typeof (Unit))]
    public class SkillComponent: Entity, IAwake, IDestroy
    {
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int, SkillUnit> skillDict = new();

        public int oft;
        public int lastSkillId;
        public int usingSkillId;
        public long singTimer;
        public long effectTimer;
        public ASkillEffect skillEffect;
        public SkillDyna dyna;

        /// <summary>
        /// 减CD比例(万比)
        /// </summary>
        public int reduceCdRate = 0;

        /// <summary>
        /// 技能ID减Cd比例
        /// </summary>
        public Dictionary<int, int> cdRateDict = new();

        /// <summary>
        /// 技能ID减Cd时间
        /// </summary>
        public Dictionary<int, int> cdSecDict = new();
    }
}