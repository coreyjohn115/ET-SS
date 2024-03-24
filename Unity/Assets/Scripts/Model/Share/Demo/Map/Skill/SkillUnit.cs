using System.Collections.Generic;

namespace ET
{
    [ChildOf]
    public class SkillUnit: Entity, IAwake
    {
        public SkillConfig Config
        {
            get
            {
                return SkillConfigCategory.Instance.Get((int)this.Id);
            }
        }

        public SkillMasterConfig MasterConfig
        {
            get
            {
                return SkillMasterConfigCategory.Instance.Get(this.Config.MasterId);
            }
        }

        public List<long> cdList = new List<long>();
    }
}