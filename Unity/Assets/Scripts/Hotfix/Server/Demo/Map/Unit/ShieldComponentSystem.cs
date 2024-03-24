namespace ET.Server
{
    public static class ShieldComponentSystem
    {
        public static void AddShield(this ShieldComponent self, int buffId, long value)
        {
            self.ShieldIdDict[buffId] = value;
            self.ReCaculate();
        }

        public static long RemoveShield(this ShieldComponent self, int buffId)
        {
            self.ShieldIdDict.Remove(buffId, out var value);

            self.ReCaculate();
            return value;
        }

        public static void ClearShield(this ShieldComponent self)
        {
            self.ShieldIdDict.Clear();
            self.ReCaculate();
        }

        /// <summary>
        /// 重新计算护盾值
        /// </summary>
        /// <param name="self"></param>
        private static void ReCaculate(this ShieldComponent self)
        {
            EventSystem.Instance.Publish(self.Scene(), new UnitShieldChange() { Unit = self.GetParent<Unit>() });
        }
    }
}