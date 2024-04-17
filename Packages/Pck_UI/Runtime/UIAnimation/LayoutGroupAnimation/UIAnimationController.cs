namespace BW.GameCode.UI
{
    using System.Collections;

    using UnityEngine;

    public class UIAnimationController : MonoBehaviour
    {
        [SerializeField] UIAnimation[] m_anims = default;
        [SerializeField] float m_animInterval = 0f;
        [Header("动画速度")]
        [SerializeField] float m_speed = 1f;

        WaitForSeconds m_animIntervalWait;

        IEnumerator animInstance;

        public void InitAnimations() {
            if (m_anims == null) {
                return;
            }

            for (int i = 0; i < m_anims.Length; i++) {
                if (m_anims[i] != null) {
                    m_anims[i].Init();
                }
            }
            m_animIntervalWait = new WaitForSeconds(m_animInterval);
        }

        public Coroutine Play() {
            Stop();
            animInstance = DoPlay();
            return StartCoroutine(animInstance);
        }

        IEnumerator DoPlay() {
            for (int i = 0; i < m_anims.Length; i++) {
                if (m_anims[i] != null) {
                    yield return m_anims[i].Play(m_speed);
                    yield return m_animIntervalWait;
                }
            }
        }

        public void Stop() {
            if (animInstance != null) {
                StopCoroutine(animInstance);
            }
        }
    }
}