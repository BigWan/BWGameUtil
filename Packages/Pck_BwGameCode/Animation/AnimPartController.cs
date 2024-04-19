namespace BW.GameCode.Animation
{
    using System.Collections;

    using UnityEngine;

    public enum AnimtionLoopType
    {
        /// <summary>
        /// 不循环
        /// </summary>
        None,
        /// <summary>
        /// 重头开始
        /// </summary>
        Restart,
        /// <summary>
        /// 转圈循环
        /// </summary>
        Yoyo,
    }

    [DisallowMultipleComponent]
    public class AnimPartController : MonoBehaviour
    {
        [Header(" 动画元素，元素不是都会播放，根据需要播放某一个")]
        [SerializeField] AnimPart[] m_anims;
        [SerializeField] float m_speed = 1;
        [SerializeField] AnimtionLoopType m_loopType = AnimtionLoopType.None;

        IEnumerator animInstance;
        AnimPart activeAnim;

        public AnimtionLoopType LoopType { get => m_loopType; set { m_loopType = value; } }

        public void InitAnimations() {
            if (m_anims != null) {
                foreach (var a in m_anims) {
                    a.Init();
                }
            }
        }

        public Coroutine Play(int animIndex = 0, bool resetCurrent = false) {
            Stop(resetCurrent);
            if (m_anims != null && m_anims.Length >= animIndex) {
                activeAnim = m_anims[animIndex];
                switch (m_loopType) {
                    case AnimtionLoopType.Restart:
                        animInstance = PlayRestart(activeAnim, m_speed);
                        break;

                    case AnimtionLoopType.Yoyo:
                        animInstance = PlayYoyo(activeAnim, m_speed);
                        break;

                    case AnimtionLoopType.None:
                    default:
                        animInstance = PlayOnce(activeAnim, m_speed);
                        break;
                }

                return StartCoroutine(animInstance);
            }
            return null;
        }

        public void Stop(bool reset = true) {
            if (animInstance != null) {
                StopCoroutine(animInstance);
                if (reset) {
                    activeAnim.Process = 0;
                }
            }
        }

        IEnumerator PlayRestart(AnimPart anim, float speed) {
            while (true) {
                yield return PlayOnce(anim, speed);
                anim.Process = speed > 0 ? 0f : 1f;
            }
        }

        IEnumerator PlayYoyo(AnimPart anim, float speed) {
            while (true) {
                yield return PlayOnce(anim, speed);
                speed *= -1;
            }
        }

        IEnumerator PlayOnce(AnimPart part, float speed) {
            var process = part.Process;

            float target = speed > 0 ? 1f : 0f; // 速度大于0,目标是1,速度小于0,目标是0

            float duration = part.Duration;

            while (!IsAnimReachedTarget(process, speed)) {
                //Debug.Log("A" + process + "/" + speed + "+" + IsAnimReachedTarget(process, speed));
                process += Time.deltaTime / duration * speed;
                part.Process = process;
                //Debug.Log("B" + process + "/" + speed);
                yield return null;
            }
            part.Process = target;
        }

        bool IsAnimReachedTarget(float process, float speed) {
            if (speed > 0) {
                return process >= 1f;
            } else {
                return process <= 0f;
            }
        }

        /// <summary>
        /// 播放一个片段
        /// </summary>
        /// <param name="anim"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        IEnumerator PlayOnce2(AnimPart anim, float speed) {
            var duration = anim.Duration;
            float elapsedTime = 0;
            while (Mathf.Abs(elapsedTime) < duration) {
                elapsedTime += Time.deltaTime * speed;
                float normal = elapsedTime / duration;
                if (normal < 0) {
                    normal += 1;
                }
                normal = Mathf.Clamp01(normal);
                anim.Process = normal;
                yield return null;
            }

            //Debug.Log(normal);

            //anim.SetAnimationState(normal);
        }
    }
}