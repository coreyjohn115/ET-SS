﻿using System;
using UnityEngine;

namespace ET
{
    /// <summary>
    /// 可挂载的Unity动画实现
    /// </summary>
    public abstract class UComTweener : MonoBehaviour
    {
        #region Properties
        /// <summary>
        /// 动画管理器
        /// </summary>
        public Tweener Tweener => tweener;
        #endregion

        #region Methods
        public Tweener TweenStart(Action<Tweener> start)
        {
            Thrower.IsNotNull(start);
            tweener.OnStart += start;

            return tweener;
        }

        public Tweener TweenPause(Action<Tweener> pause)
        {
            Thrower.IsNotNull(pause);
            tweener.OnPause += pause;

            return tweener;
        }

        public Tweener TweenUpdate(Action<Tweener> update)
        {
            Thrower.IsNotNull(update);
            tweener.OnUpdated += update;

            return tweener;
        }

        public Tweener TweenKill(Action<Tweener> kill)
        {
            Thrower.IsNotNull(kill);
            tweener.OnKill += kill;

            return tweener;
        }

        public Tweener TweenComplete(Action<Tweener> complete)
        {
            Thrower.IsNotNull(complete);
            tweener.OnComplete += complete;

            return tweener;
        }

        public Tweener Pause(bool isPause)
        {
            tweener.IsPaused = isPause;

            return tweener;
        }

        public Tweener Stop()
        {
            tweener.Stop();

            return tweener;
        }

        public void Kill()
        {
            tweener.Kill();
        }
        #endregion

        #region Internal Methods
        protected virtual void Awake()
        {
            tweener = TweenManager.CreateTweener<Tweener>();
            tweener.UpdateFactor = OnUpdate;
        }

        protected virtual void Start()
        {
            tweener.Delay = delay;
            tweener.Duration = duration;
            tweener.LoopType = loopType;
            tweener.EaseType = easeType;
            tweener.UseFixedUpdate = fixedUpdate;

            if (autoPlay)
            {
                tweener.PlayForward();
            }
        }

        private void OnDestroy()
        {
            tweener.Kill();
        }

        /// <summary>
        /// 当更新时
        /// </summary>
        /// <param name="factor">采样因子 大小在0 - 1之间</param>
        /// <param name="currentTime"></param>
        protected abstract void OnUpdate(float factor, float currentTime);

        protected virtual void OnValidate()
        {
            if (tweener != null)
            {
                tweener.Delay = delay;
                tweener.Duration = duration;
                tweener.LoopType = loopType;
                tweener.EaseType = easeType;
                tweener.UseFixedUpdate = fixedUpdate;
            }
        }

        [ContextMenu("PlayForward")]
        private void PlayForward()
        {
            tweener?.PlayForward();
        }

        [ContextMenu("PlayReverse")]
        private void PlayReverse()
        {
            tweener?.PlayReverse();
        }
        #endregion

        #region Internal Fields
        [SerializeField]
        private float delay = 0;
        [SerializeField]
        private float duration = 1;
        [SerializeField]
        private LoopType loopType = LoopType.PingPong;
        [SerializeField]
        private Ease easeType = Ease.Linear;
        [SerializeField]
        private bool fixedUpdate = false;
        [SerializeField]
        private bool autoPlay = true;

        private Tweener tweener;
        #endregion
    }
}