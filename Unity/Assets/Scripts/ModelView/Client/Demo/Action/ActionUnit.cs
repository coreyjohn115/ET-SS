namespace ET.Client
{
    public enum ActionState
    {
        Ready,

        /// <summary>
        /// 行为运行中
        /// </summary>
        Run,

        /// <summary>
        /// 行为已完成
        /// </summary>
        Complete,

        /// <summary>
        /// 行为已结束
        /// </summary>
        Finish
    }

    [ChildOf(typeof (ActionComponent))]
    public class ActionUnit: Entity, IAwake<string>, IDestroy
    {
        public string ActionName { get; set; }

        /// <summary>
        /// 行为是否在执行中
        /// </summary>
        public bool IsRunning => this.state == ActionState.Run;

        public ActionConfig Config => ActionConfigCategory.Instance.GetActionCfg(this.ActionName);

        public AAction action;

        public ActionState state;

        /// <summary>
        /// 行为执行时间
        /// </summary>
        public float duration;

        /// <summary>
        /// 行为开始时间
        /// </summary>
        public float startTime;
    }
}