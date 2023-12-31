﻿using System;

namespace ET
{
    [Flags]
    public enum SceneType: long
    {
        None = 0,
        Main = 1, // 主纤程,一个进程一个, 初始化从这里开始
        NetInner = 1 << 2, // 负责进程间消息通信

        /// <summary>
        /// 登录服
        /// </summary>
        Realm = 1 << 3,

        /// <summary>
        /// 网关服
        /// </summary>
        Gate = 1 << 4,
        Http = 1 << 5,
        Location = 1 << 6,

        /// <summary>
        /// 地图服
        /// </summary>
        Map = 1 << 7,
        Router = 1 << 8,
        RouterManager = 1 << 9,
        Robot = 1 << 10,
        BenchmarkClient = 1 << 11,
        BenchmarkServer = 1 << 12,
        Match = 1 << 14,
        Room = 1 << 15,
        LockStepClient = 1 << 16,
        LockStepServer = 1 << 17,
        RoomRoot = 1 << 18,
        Watcher = 1 << 19,

        /// <summary>
        /// 缓存服
        /// </summary>
        Cache = 1 << 20,

        /// <summary>
        /// 聊天服
        /// </summary>
        Chat = 1 << 21,

        /// <summary>
        /// 排行榜服
        /// </summary>
        Rank = 1 << 22,

        /// <summary>
        /// 账号服
        /// </summary>
        Account = 1 << 24,
        
        /// <summary>
        /// 帮会服
        /// </summary>
        League = 1 << 25,
        
        /// <summary>
        /// 活动服
        /// </summary>
        Activity = 1 << 26,
        

        // 客户端
        Client = 1L << 50,
        Current = 1L << 51,
        LockStep = 1L << 52,
        LockStepView = 1L << 53,
        DemoView = 1L << 54,
        NetClient = 1L << 55,

        All = long.MaxValue,
    }

    public static class SceneTypeHelper
    {
        public static bool HasSameFlag(this SceneType a, SceneType b)
        {
            if (((ulong) a & (ulong) b) == 0)
            {
                return false;
            }

            return true;
        }
    }
}