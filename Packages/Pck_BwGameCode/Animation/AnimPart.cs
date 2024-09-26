namespace BW.GameCode.Animation
{
    using System.Collections;

    using UnityEngine;

    /// <summary>
    /// 基础的动画部件接口
    /// </summary>
    public abstract class AnimPart : MonoBehaviour
    {
        public abstract float Duration { get; }

        float mProcess = 0;

        /// <summary>
        /// value from 0 to 1
        /// </summary>
        public float Process {
            get => mProcess;
            set {
                mProcess = Mathf.Clamp01(value);
                SetAnimationState(mProcess);
            }
        }

        public virtual void Init() {
        }

        /// <summary>
        /// 将动画设置成目标进度,
        /// 动画可以处在任意位置,通过0~1的proces来控制动画
        /// </summary>
        /// <param name="process"></param>
        protected abstract void SetAnimationState(float process);

        /// <summary>
        /// 播放动画迭代器接口,通过协程调用
        /// </summary>
        /// <param name="m_anim"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public static IEnumerator PlayProcess(AnimPart m_anim, float speed) {
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