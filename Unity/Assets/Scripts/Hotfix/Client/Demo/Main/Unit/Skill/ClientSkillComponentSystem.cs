namespace ET.Client
{
    [EntitySystemOf(typeof (ClientSkillComponent))]
    public static partial class ClientSkillComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ClientSkillComponent self)
        {
        }

        /// <summary>
        /// 主动使用技能
        /// </summary>
        /// <param name="self"></param>
        public static void UseSkill(this ClientSkillComponent self)
        {
            var cfg = SkillConfigCategory.Instance.Get(10001001);
            EventSystem.Instance.Publish(self.Scene(), new ClientUseSkill() { Unit = self.GetParent<Unit>(), MasterId = cfg.MasterId, });
        }

        public static void SyncUseSkill(this ClientSkillComponent self, M2C_UseSkill message)
        {
            self.GetParent<Unit>().Position = message.Position;
            SkillUnit skill = self.GetChild<SkillUnit>(message.Id);
            if (!skill)
            {
                Log.Error($"找不到技能: {message.Id}");
                return;
            }

            EventSystem.Instance.Publish(self.Scene(), new UseSKill()
            {
                Unit = self.GetParent<Unit>(),
                SkillId = message.Id,
                Direct = message.Direct,
                DstList = message.DstList,
                DstPosition = message.DstPosition,
            });
        }
    }
}