﻿using System.Collections.Generic;

namespace ET
{
    public partial class BuffConfig
    {
        public List<EffectArg> EffectList { get; private set; }

        public HashSet<int> MutexMap { get; private set; }

        public HashSet<int> ClassifyMap { get; private set; }

        public override void EndInit()
        {
            this.EffectList = MongoHelper.FromJson<List<EffectArg>>(this.EffectStr);
            this.MutexMap = new HashSet<int>();
            this.ClassifyMap = new HashSet<int>();
            foreach (int m in this.Mutex)
            {
                this.MutexMap.Add(m);
            }

            foreach (int m in this.Classify)
            {
                this.ClassifyMap.Add(m);
            }
        }
    }
}