namespace BW.GameCode.Animation
{
    using System.Collections;

    using UnityEngine;

    /// <summary>
    /// �����Ķ��������ӿ�
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
        /// ���������ó�Ŀ�����,
        /// �������Դ�������λ��,ͨ��0~1��proces�����ƶ���
        /// </summary>
        /// <param name="process"></param>
        protected abstract void SetAnimationState(float process);

        /// <summary>
        /// ���Ŷ����������ӿ�,ͨ��Э�̵���
        /// </summary>
        /// <param name="m_anim"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public static IEnumerator PlayProcess(AnimPart m_anim, float speed) {
            var process = m_anim.Process;

            float target = speed > 0 ? 1f : 0f; // �ٶȴ���0,Ŀ����1,�ٶ�С��0,Ŀ����0

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