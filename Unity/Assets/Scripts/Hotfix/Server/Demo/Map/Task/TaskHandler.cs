﻿using System.Collections.Generic;

namespace ET.Server
{
    /// <summary>
    /// 默认成功
    /// </summary>
    [TaskArgs("Default")]
    [FriendOf(typeof (TaskComponent))]
    public class TaskHandlerDefault: ATaskHandler
    {
        public override bool Run(TaskComponent self, TaskData task, List<long> args, long[] cfgArgs)
        {
            if (!self.TaskFuncDict.TryGetValue((TaskEventType) task.Config.EventType, out var tf))
            {
                return true;
            }

            int i = tf.Process.Key;
            int j = tf.Process.Value;
            if (j > 0)
            {
                return args[i] >= cfgArgs[j];
            }

            return true;
        }
    }
}