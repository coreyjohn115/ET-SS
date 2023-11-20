﻿using System.Collections.Generic;

namespace ET.Server
{
    [TaskProcess("Default")]
    public class TaskProcessDefault: ATaskProcess
    {
        public override KeyValuePair<long, long> Run(TaskComponent self, TaskData task, long[] cfgArgs)
        {
            return new KeyValuePair<long, long>(task.Min, task.Max);
        }
    }

    /// <summary>
    /// 默认成功
    /// </summary>
    [TaskProcess("SubTask")]
    public class TaskProcessSubTask: ATaskProcess
    {
        public override KeyValuePair<long, long> Run(TaskComponent self, TaskData task, long[] cfgArgs)
        {
            return new KeyValuePair<long, long>(task.Args[0], cfgArgs.Length - 1);
        }
    }
}