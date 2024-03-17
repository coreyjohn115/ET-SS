﻿using System;

namespace ET.Client
{
    [EntitySystemOf(typeof (ActionComponent))]
    [FriendOf(typeof (ActionComponent))]
    [FriendOf(typeof (ActionUnit))]
    public static partial class ActionComponentSystem
    {
        [EntitySystem]
        private static void Awake(this ActionComponent self)
        {
        }

        [EntitySystem]
        private static void Update(this ActionComponent self)
        {
            if (self.curAction != null)
            {
                try
                {
                    ActionUnit action = self.curAction;
                    action.Update();

                    if (action.state == ActionState.Finish)
                    {
                        self.curAction = null;
                        self.pushActions.Remove(action.Id);
                        if (self.pushActions.Count > 0)
                        {
                            self.curAction = self.GetChild<ActionUnit>(self.pushActions[0]);
                        }
                    }
                }
                catch (Exception e)
                {
                    Log.Error($"push action 执行报错 {e}");
                }
            }

            if (self.playActions.Count <= 0)
            {
                return;
            }

            using var list = ListComponent<long>.Create();
            foreach (long id in self.playActions)
            {
                ActionUnit action = self.GetChild<ActionUnit>(id);
                if (action == null)
                {
                    continue;
                }

                try
                {
                    action.Update();
                    if (action.state == ActionState.Finish)
                    {
                        list.Add(action.Id);
                    }
                }
                catch (Exception e)
                {
                    Log.Error($"play action 执行报错 {action.ActionName} {e}");
                }
            }

            foreach (long l in list)
            {
                self.playActions.Remove(l);
                self.RemoveChild(l);
            }
        }

        [EntitySystem]
        private static void Destroy(this ActionComponent self)
        {
            self.StopAllAction();
        }

        public static long PlayAction(this ActionComponent self, string name)
        {
            ActionUnit action = self.AddChild<ActionUnit, string>(name);
            self.playActions.Add(action.Id);

            return action.Id;
        }

        public static long PushAction(this ActionComponent self, string name)
        {
            ActionUnit action = self.AddChild<ActionUnit, string>(name);
            self.pushActions.Add(action.Id);

            self.pushActions.Sort(Comparison);
            if (self.curAction == null)
            {
                self.curAction = self.GetChild<ActionUnit>(self.pushActions[0]);
            }

            return action.Id;

            int Comparison(long a, long b)
            {
                ActionUnit unitA = self.GetChild<ActionUnit>(a);
                ActionUnit unitB = self.GetChild<ActionUnit>(b);
                return unitA.Config.Priority.CompareTo(unitB.Config.Priority);
            }
        }

        public static void RemoveAction(this ActionComponent self, long id)
        {
            if (!self.GetChild<ActionUnit>(id))
            {
                return;
            }

            self.RemoveChild(id);
            self.playActions.Remove(id);
        }

        public static void RemoveAction(this ActionComponent self, string name)
        {
            foreach (long l in self.playActions)
            {
                ActionUnit action = self.GetChild<ActionUnit>(l);
                if (!action || action.ActionName != name)
                {
                    continue;
                }

                action.Finish();
            }
        }

        public static void StopAllAction(this ActionComponent self)
        {
            if (self.curAction != null)
            {
                ActionUnit action = self.curAction;
                action.Finish();
            }

            self.curAction = null;
            self.pushActions.Clear();

            foreach (long l in self.playActions)
            {
                ActionUnit action = self.GetChild<ActionUnit>(l);
                if (action == null)
                {
                    continue;
                }

                action.Finish();
            }
        }
    }
}