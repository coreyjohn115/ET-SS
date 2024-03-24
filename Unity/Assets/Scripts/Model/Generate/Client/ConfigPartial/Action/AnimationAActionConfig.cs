namespace ET
{
    /// <summary>
    /// 动画行为配置
    /// </summary>
    public class AnimationAActionConfig: ActionSubConfig
    {
        public string ClipName;
        public float AnimFastBlendRate;
        public float AnimSmoothBlendRate;
        public bool Strict;
        public int AnimLayer;
        public float FadeTime;
    }
}