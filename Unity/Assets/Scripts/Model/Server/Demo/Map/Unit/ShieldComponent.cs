using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;

namespace ET.Server
{
    /// <summary>
    /// 护盾修改事件
    /// </summary>
    public struct UnitShieldChange
    {
        public Unit Unit { get; set; }
    }

    [ComponentOf(typeof (Unit))]
    public class ShieldComponent: Entity, IAwake, ITransfer
    {
        /// <summary>
        /// 护盾字典
        /// </summary>
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int, long> ShieldIdDict { get; } = new();
    }
}