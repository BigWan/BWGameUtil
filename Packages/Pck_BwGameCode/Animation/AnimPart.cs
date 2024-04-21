namespace BW.GameCode.Animation
{
    using System.Collections;

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
        }

        /// <summary>
        /// 处理动画进度
        /// </summary>
        /// <param name="process"></param>
        protected abstract void SetAnimationState(float process);

        public static IEnumerator Play(AnimPart m_anim, float speed) {
            var process = m_anim.Process;

            float target = speed > 0 ? 1f : 0f; // 速度大于0,目标是1,速度小于0,目标是0

            float duration = m_anim.Duration;

            while (!IsAnimReachedTarget(process, speed)) {
                //Debug.Log("A" + process + "/" + speed + "+" + IsAnimReachedTarget(process, speed));
                process += Time.deltaTime / duration * speed;
                m_anim.Process = process;
                //Debug.Log("B" + process + "/" + speed);
                yield return null;
            }
            m_anim.Process = target;
        }

        static bool IsAnimReachedTarget(float process, float speed) {
            if (speed > 0) {
                return process >= 1f;
            } else {
                return process <= 0f;
            }
        }
    }
}