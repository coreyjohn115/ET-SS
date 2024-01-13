using UnityEngine;

namespace ET.Client;

[EntitySystemOf(typeof (ActionUnit))]
[FriendOf(typeof (ActionUnit))]
public static partial class ActionUnitSystem
{
    [EntitySystem]
    private static void Awake(this ActionUnit self, string name)
    {
        self.actionName = name;
        ActionConfig cfg = ActionConfigCategory.Instance.GetActionCfg(name);
        self.config = MongoHelper.FromJson<AActionSubConfig>(cfg.JsonStr);
        self.action = ActionSingleton.Instance.GetAction(cfg.Type);
    }

    [EntitySystem]
    private static void Destroy(this ActionUnit self)
    {
        if (self.state != ActionState.Complete)
        {
            self.state = ActionState.Complete;
            self.UnExecute();
        }

        self.state = ActionState.Ready;
        self.duration = 0;
        self.startTime = 0;
        self.config = null;
        self.action = null;
    }

    public static void Finish(this ActionUnit self)
    {
        if (self.state == ActionState.Run)
        {
            self.state = ActionState.Complete;
            self.UnExecute();
        }
    }

    public static void Update(this ActionUnit self)
    {
        if (self.action == null)
        {
            return;
        }

        Unit unit = self.Parent.GetParent<Unit>();
        switch (self.state)
        {
            case ActionState.Run:
                self.action.OnUpdate(unit, self);
                self.duration += Time.deltaTime;
                if (self.duration >= self.config.Duration + self.config.StartTime)
                {
                    self.state = ActionState.Complete;
                    self.UnExecute();
                }

                break;
            case ActionState.Ready:
                self.duration += Time.deltaTime;
                if (self.duration >= self.config.StartTime)
                {
                    self.Execute();
                }

                break;
        }
    }

    private static void Execute(this ActionUnit self)
    {
        if (self.action == null)
        {
            return;
        }

        if (self.state != ActionState.Ready)
        {
            return;
        }

        self.startTime = Time.time;
        Unit unit = self.Parent.GetParent<Unit>();
        self.action.Execute(unit, self);
        self.state = ActionState.Run;
        self.action.OnExecute(unit, self);
    }

    private static void UnExecute(this ActionUnit self)
    {
        if (self.action == null)
        {
            return;
        }

        if (self.state != ActionState.Complete)
        {
            return;
        }

        Unit unit = self.Parent.GetParent<Unit>();
        self.action.OnUnExecute(unit, self);
        self.state = ActionState.Finish;
        self.action.OnFinish(unit, self);
    }
}