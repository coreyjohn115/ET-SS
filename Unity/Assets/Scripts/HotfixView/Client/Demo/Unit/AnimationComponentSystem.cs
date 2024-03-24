using UnityEngine;

namespace ET.Client
{
    [EntitySystemOf(typeof (AnimationComponent))]
    [FriendOf(typeof (AnimationComponent))]
    public static partial class AnimationComponentSystem
    {
        [EntitySystem]
        private static void Awake(this AnimationComponent self)
        {
            var animation = self.GetParent<Unit>().GetComponent<UnitGoComponent>().GetAnimation();

            if (animation == null)
            {
                return;
            }

            self.animation = animation;
        }

        [EntitySystem]
        private static void Destroy(this AnimationComponent self)
        {
        }

        private static bool IsClipLoop(AnimationClip clip)
        {
            return clip && (clip.wrapMode == WrapMode.Loop || clip.wrapMode == WrapMode.PingPong);
        }

        private static AnimationState PlayInner(this AnimationComponent self, string clipName, float fadeLength, bool isLoop)
        {
            AnimationState r = null;
            if (self.lastAnimName == null)
            {
                if (self.animation.Play(clipName, PlayMode.StopAll))
                {
                    self.animation.Sample();
                    r = self.animation[clipName];
                }
            }
            else
            {
                if (clipName == self.lastAnimName)
                {
                    if (isLoop)
                    {
                        return self.animation[clipName];
                    }

                    if (fadeLength <= 0)
                    {
                        if (self.animation.Play(clipName, PlayMode.StopAll))
                        {
                            self.animation.Sample();
                            r = self.animation[clipName];
                        }
                        else
                        {
                            r = null;
                        }
                    }
                    else
                    {
                        r = self.animation.CrossFadeQueued(clipName, fadeLength, QueueMode.PlayNow, PlayMode.StopAll);
                    }
                }
                else
                {
                    if (fadeLength <= 0)
                    {
                        if (self.animation.Play(clipName, PlayMode.StopAll))
                        {
                            self.animation.Sample();
                            r = self.animation[clipName];
                        }
                        else
                        {
                            r = null;
                        }
                    }
                    else
                    {
                        self.animation.CrossFade(clipName, fadeLength, PlayMode.StopAll);
                        r = self.animation[clipName];
                    }

                    if (r != null && self.lastAnimState != null && IsClipLoop(self.lastAnimState.clip) && isLoop)
                    {
                        r.normalizedTime = self.lastAnimState.normalizedTime - Mathf.Floor(self.lastAnimState.normalizedTime);
                    }
                }
            }

            self.lastAnimName = clipName;
            self.lastAnimState = r;
            return r;
        }

        public static AnimationState Play(this AnimationComponent self, AnimationAActionConfig cfg)
        {
            var ac = self.animation.GetClip(cfg.ClipName);
            if (!ac)
            {
                Log.Error($"can not find animation clip: {cfg.ClipName}");
                return default;
            }

            if (self.lastAnimName == cfg.ClipName && (Time.frameCount - self.lastAnimStateFrame) <= 3)
            {
                return self.lastAnimState;
            }

            bool isLoop = IsClipLoop(ac);
            self.lastAnimStateFrame = Time.frameCount;
            if (cfg.Strict)
            {
                return self.PlayInner(cfg.ClipName, cfg.FadeTime, isLoop);
            }
            else
            {
                bool focusNewAnim = cfg.AnimLayer > self.lastAnimLayer;
                float targetWeight = focusNewAnim? 10 : 1;
                self.lastAnimLayer = cfg.AnimLayer;
                self.animation.Blend(cfg.ClipName, targetWeight, 0.3f);
                return self.animation[cfg.ClipName];
            }
        }
    }
}