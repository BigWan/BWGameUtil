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
        [SerializeField] AnimtionLoopType m_loopType = AnimtionLoopType.None;

        IEnumerator animInstance;
        AnimPart activeAnim;

        public AnimtionLoopType LoopType { get => m_loopType; set { m_loopType = value; } }


        bool isInited = false;

        private void Awake() {
            InitAnimations();
        }

        [ContextMenu("Init Animations")]
        public void InitAnimations() {
            if (m_anims != null && !isInited) {
                foreach (var a in m_anims) {
                    a.Init();
                }
                isInited = true;
            }
        }

        [ContextMenu("Start")]
        public void SetStartPoint() {
            if (m_anims != null) {
                foreach (var a in m_anims) {
                    a.Process = 0;
                }
            }
        }
        [ContextMenu("End")]
        public void SetEndPoint() {
            if (m_anims != null) {
                foreach (var a in m_anims) {
                    a.Process = 1;
                }
            }
        }
        public Coroutine Play(float speed = 1f, int animIndex = 0, bool resetCurrent = false) {
            Stop(resetCurrent);
            if (m_anims != null && m_anims.Length > animIndex) {
                activeAnim = m_anims[animIndex];
                switch (m_loopType) {
                    case AnimtionLoopType.Restart:
                        animInstance = PlayRestart(activeAnim, speed);
                        break;

                    case AnimtionLoopType.Yoyo:
                        animInstance = PlayYoyo(activeAnim, speed);
                        break;

                    case AnimtionLoopType.None:
                    default:
                        animInstance = PlayOnce(activeAnim, speed);
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
    }
}