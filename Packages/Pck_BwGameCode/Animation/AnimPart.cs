namespace BW.GameCode.Animation
{
    using UnityEngine;

    public abstract class AnimPart : MonoBehaviour
    {
        public abstract float Duration { get; }

        float m_process = 0;

        /// <summary>
        /// value from 0 to 1
        /// </summary>
        public float Process {
            get => m_process;
            set {
                m_process = Mathf.Clamp01(value);
                SetAnimationState(m_process);
            }
        }

        public virtual void Init() {
            Process = 0;
        }

        /// <summary>
        /// 处理动画进度
        /// </summary>
        /// <param name="process"></param>
        protected abstract void SetAnimationState(float process);
    }
}