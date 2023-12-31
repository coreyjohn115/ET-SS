using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.ComponentModel;

namespace ET
{
    [Config]
    public partial class TaskConfigCategory : Singleton<TaskConfigCategory>, IMerge, IConfigCategory
    {
        [BsonElement]
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        private Dictionary<int, TaskConfig> dict = new();
		
        public void Merge(object o)
        {
            TaskConfigCategory s = o as TaskConfigCategory;
            foreach (var kv in s.dict)
            {
                this.dict.Add(kv.Key, kv.Value);
            }
        }
		
        public TaskConfig Get(int id)
        {
            this.dict.TryGetValue(id, out TaskConfig item);

            if (item == null)
            {
                throw new Exception($"配置找不到，配置表名: {nameof (TaskConfig)}，配置id: {id}");
            }

            return item;
        }
        
        public object GetConfig(int id)
        {
            return this.Get(id);
        }
		
        public bool Contain(int id)
        {
            return this.dict.ContainsKey(id);
        }

        public Dictionary<int, TaskConfig> GetAll()
        {
            return this.dict;
        }
        
        public object GetAllConfig()
        {
            return this.dict.Values;
        }

        public TaskConfig GetOne()
        {
            if (this.dict == null || this.dict.Count <= 0)
            {
                return null;
            }
            return this.dict.Values.GetEnumerator().Current;
        }
    }

	public partial class TaskConfig: ProtoObject, IConfig
	{
		/// <summary>Id</summary>
		public int Id { get; set; }
		/// <summary>任务类型</summary>
		public int TaskType { get; set; }
		/// <summary>任务事件类型</summary>
		public int EventType { get; set; }
		/// <summary>任务参数</summary>
		public long[] Args { get; set; }
		/// <summary>完成是否显示</summary>
		public bool FinishShow { get; set; }
		/// <summary>是否显示进度</summary>
		public bool ShopProgress { get; set; }
		/// <summary>自动提交</summary>
		public bool AutoCommit { get; set; }
		/// <summary>奖励ID</summary>
		public int DropId { get; set; }
		/// <summary>接取条件列表</summary>
		public string GetConditionListStr { get; set; }
		/// <summary>提交条件列表</summary>
		public string CommitConditionListStr { get; set; }
		/// <summary>提交条件列表</summary>
		public string CommitCmdListStr { get; set; }
		/// <summary>品质</summary>
		public int Quality { get; set; }
		/// <summary>后续任务列表</summary>
		public int[] NextList { get; set; }
		/// <summary>是否进入完成列表</summary>
		public bool IsEnterFinish { get; set; }

	}
}
