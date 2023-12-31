﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ET
{
	/// <summary>
    /// ConfigLoader会扫描所有的有ConfigAttribute标签的配置,加载进来
    /// </summary>
    public class ConfigLoader: Singleton<ConfigLoader>, ISingletonAwake
    {
        public struct GetAllConfigBytes
        {
        }
        
        public struct GetOneConfigBytes
        {
            public string ConfigName;
        }
		
        private readonly ConcurrentDictionary<Type, ASingleton> allConfig = new();
        
        public ASingleton GetConfigSingleton(Type type)
		{
			lock (this)
			{
				return this.allConfig[type];
			}
		}
        
        public void Awake()
        {
        }

		public async ETTask Reload(Type configType)
		{
			byte[] oneConfigBytes =
					await EventSystem.Instance.Invoke<GetOneConfigBytes, ETTask<byte[]>>(new GetOneConfigBytes() { ConfigName = configType.Name });

			object category = MongoHelper.Deserialize(configType, oneConfigBytes, 0, oneConfigBytes.Length);
			ASingleton singleton = category as ASingleton;
			this.allConfig[configType] = singleton;
			
			World.Instance.AddSingleton(singleton);
		}
		
		public async ETTask LoadAsync()
		{
			this.allConfig.Clear();
			Dictionary<Type, byte[]> configBytes = await EventSystem.Instance.Invoke<GetAllConfigBytes, ETTask<Dictionary<Type, byte[]>>>(new GetAllConfigBytes());

			using ListComponent<Task> listTasks = ListComponent<Task>.Create();
			
			// 首先加载语言配置
			Type t = configBytes.Keys.FirstOrDefault(t => t.Name == "LanguageCategory");
			if (t != null)
			{
				await Task.Run(() => LoadOneInThread(t, configBytes[t]));
			}
			
			foreach (Type type in configBytes.Keys)
			{
				if (type.Name == "LanguageCategory")
				{
					continue;
				}
				
				byte[] oneConfigBytes = configBytes[type];
				Task task = Task.Run(() => LoadOneInThread(type, oneConfigBytes));
				listTasks.Add(task);
			}

			await Task.WhenAll(listTasks.ToArray());
		}
		
		private void LoadOneInThread(Type configType, byte[] oneConfigBytes)
		{
			object category = MongoHelper.Deserialize(configType, oneConfigBytes, 0, oneConfigBytes.Length);
			
			lock (this)
			{
				ASingleton singleton = category as ASingleton;
				this.allConfig[configType] = singleton;
				
				World.Instance.AddSingleton(singleton);
			}
		}
    }
}