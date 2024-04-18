namespace BW.GameCode.UI
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
    public class UIAnimationController : MonoBehaviour
    {
        [SerializeField] UIAnimation[] m_anims;
        [SerializeField] float m_speed = 1;
        [SerializeField] AnimtionLoopType m_loopType = AnimtionLoopType.None;

        IEnumerator animInstance;
        UIAnimation activeAnim;

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
                animInstance = m_loopType switch {
                    AnimtionLoopType.Restart => PlayRestart(activeAnim, m_speed),
                    AnimtionLoopType.Yoyo => PlayYoyo(activeAnim, m_speed),
                    _ => PlayOnce(activeAnim, m_speed),
                };
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

        IEnumerator PlayRestart(UIAnimation anim, float speed) {
            while (true) {
                yield return PlayOnce(anim, speed);
            }
        }

        IEnumerator PlayYoyo(UIAnimation anim, float speed) {
            while (true) {
                yield return PlayOnce(anim, speed);
                speed *= -1;
                Debug.Log($"Speed = {speed}");
            }
        }

        /// <summary>
        /// 播放一个片段
        /// </summary>
        /// <param name="anim"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        IEnumerator PlayOnce(UIAnimation anim, float speed) {
            var duration = anim.Duration;
            float elapsedTime = 0;
            while (Mathf.Abs(elapsedTime) < duration) {
                elapsedTime += Time.deltaTime * speed;
                float normal = elapsedTime / duration;
                if (normal < 0) {
                    normal += 1;
                }
                normal = Mathf.Clamp01(normal);
                Debug.Log(normal);
                anim.UpdateState(normal);
                yield return null;
            }

            //Debug.Log(normal);

            //anim.SetAnimationState(normal);
        }
    }
}