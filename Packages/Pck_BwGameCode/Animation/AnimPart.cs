namespace BW.GameCode.Animation
{
    using System.Collections;
    using System;
    using UnityEngine;

    public abstract class AnimPart : MonoBehaviour
    {
        public abstract float Duration { get; }

        float m_process;

        public virtual void Init() {
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