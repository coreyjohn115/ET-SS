﻿namespace ET.Server
{
    public static class BuffSystem
    {
        public class BuffAwakeSystem: AwakeSystem<BuffUnit, int, long, long>
        {
            protected override void Awake(BuffUnit self, int id, long time, long addUnitId)
            {
                self.BuffId = id;
                self.AddTime = time;
                self.AddRoleId = addUnitId;
            }
        }
    }
}