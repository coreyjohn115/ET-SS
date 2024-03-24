namespace ET.Server
{
    [EntitySystemOf(typeof (UnitExtra))]
    public static partial class UnitExtraSystem
    {
        [EntitySystem]
        private static void Awake(this UnitExtra self)
        {
        }

        public static PlayerInfoProto ToPlayerInfo(this UnitExtra self)
        {
            return new PlayerInfoProto()
            {
                Id = self.Id,
                Name = self.PlayerName,
                HeadIcon = self.HeadIcon,
                Level = self.Level,
                Fight = self.TotalFight,
                Sex = self.Sex,
            };
        }
    }
}