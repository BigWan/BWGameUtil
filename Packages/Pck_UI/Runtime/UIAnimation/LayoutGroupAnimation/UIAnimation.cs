namespace BW.GameCode.UI
{
    using System.Collections;
    using System;
    using UnityEngine;

    [RequireComponent(typeof(RectTransform))]
    public abstract class UIAnimation : MonoBehaviour
    {
        protected RectTransform Rect { get; private set; }

        public abstract float Duration { get; }

         float m_process;

        public virtual void Init() {
            Rect = this.transform as RectTransform;
            Debug.Assert(Rect != null);
        }

        public void UpdateState(float process) {
            m_process = process;
            SetAnimationState(m_process);
        }

        /// <summary>
        /// 处理动画进度
        /// </summary>
        /// <param name="process"></param>
        protected abstract void SetAnimationState(float process);


    }
}