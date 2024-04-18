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
                        animInstance = PlayYoyo(activeAnim, m_speed);
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
                    activeAnim.UpdateState(0);
                }
            }
        }

        IEnumerator PlayRestart(AnimPart anim, float speed) {
            while (true) {
                yield return PlayOnce(anim, speed);
            }
        }

        IEnumerator PlayYoyo(AnimPart anim, float speed) {
            while (true) {
                yield return PlayOnce(anim, speed);
                speed *= -1;
            }
        }

        /// <summary>
        /// 播放一个片段
        /// </summary>
        /// <param name="anim"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        IEnumerator PlayOnce(AnimPart anim, float speed) {
            var duration = anim.Duration;
            float elapsedTime = 0;
            while (Mathf.Abs(elapsedTime) < duration) {
                elapsedTime += Time.deltaTime * speed;
                float normal = elapsedTime / duration;
                if (normal < 0) {
                    normal += 1;
                }
                normal = Mathf.Clamp01(normal);
                anim.UpdateState(normal);
                yield return null;
            }

            //Debug.Log(normal);

            //anim.SetAnimationState(normal);
        }
    }
}