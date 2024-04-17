namespace BW.GameCode.UI
{
    using System;
    using System.Collections;

    using BW.GameCode.Foundation;

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

    [RequireComponent(typeof(RectTransform))]
    public abstract class UIAnimation : MonoBehaviour
    {
        [SerializeField] float m_duration;
        [Header("动画延迟(运行时修改无效)")]
        [SerializeField] float m_delay;

        RectTransform m_rect;
        WaitForSeconds m_delayWait;
        protected RectTransform Rect => m_rect;
        public abstract float Duration { get; }

        public virtual void Init() {
            m_rect = transform as RectTransform;
            Debug.Assert(m_rect != null);
            m_delayWait = new WaitForSeconds(m_delay);
        }

        public IEnumerator Play(float speed) {
            yield return DoPlayAnimation(Duration, speed);
        }

        /// <summary>
        /// 处理动画进度
        /// </summary>
        /// <param name="process"></param>
        protected abstract void AnimationProcess(float process);

        IEnumerator DoPlayAnimation(float duration, float speed) {
            float elapsedTime = 0f;
            if (duration <= 0) {
                AnimationProcess(1);
                yield break;
            }
            while (elapsedTime < duration) {
                elapsedTime += Time.deltaTime * speed;
                float floatPercentage = Mathf.Clamp01(elapsedTime / duration);
                AnimationProcess(floatPercentage);
                yield return null;
            }

            AnimationProcess(1);
        }
    }

    public abstract class UIAnimationData<T>
    {
        public T StartValue;
        public T EndValue;
        public float Duration;
    }

    [System.Serializable]
    public class UIAnimationData_Vector3 : UIAnimationData<Vector3>
    {
    }

    [System.Serializable]
    public class UIAnimationData_Vector2 : UIAnimationData<Vector2>
    {
    }

    /// <summary>
    /// 移动动画
    /// </summary>
    public class UIAnimation_Move : UIAnimation
    {
        [SerializeField] bool m_useWorldPosition;
        [SerializeField] UIAnimationData_Vector3 m_data;

        public override float Duration => m_data != null ? m_data.Duration : 0f;

        public override void Init() {
            base.Init();
            UpdatePosition(m_data.StartValue);
        }

        protected override void AnimationProcess(float progress) {
            UpdatePosition(Vector3.Lerp(m_data.StartValue, m_data.EndValue, progress));
        }

        void UpdatePosition(Vector3 pos) {
            if (m_useWorldPosition) {
                Rect.position = pos;
            } else {
                Rect.localPosition = pos;
            }
        }
    }

    public class UIAnimation_AnchoredPosition : UIAnimation
    {
        [SerializeField] UIAnimationData_Vector3 m_data;

        public override float Duration => m_data != null ? m_data.Duration : 0f;

        public override void Init() {
            base.Init();
            Rect.anchoredPosition = m_data.StartValue;
        }

        protected override void AnimationProcess(float process) {
            Rect.anchoredPosition = Vector2.Lerp(m_data.StartValue, m_data.EndValue, process);
        }
    }

    [System.Serializable]
    public class UIAnimationData_Float : UIAnimationData<float>
    {
    }

    [RequireComponent(typeof(CanvasGroup))]
    public class UIAnimation_Fade : UIAnimation
    {
        [SerializeField] UIAnimationData_Float m_data;
        [SerializeField] CanvasGroup m_cg;

        public override void Init() {
            base.Init();
            if (m_cg == null) {
                m_cg = GetComponent<CanvasGroup>();
            }
            Debug.Assert(m_cg != null);
        }

        public override float Duration => m_data != null ? m_data.Duration : 0f;

        protected override void AnimationProcess(float process) {
            m_cg.alpha = Mathf.Lerp(m_data.StartValue, m_data.EndValue, process);
        }
    }

    public class UIAnimation_Scale : UIAnimation
    {
        [SerializeField] UIAnimationData_Float m_data;

        public override float Duration => m_data != null ? m_data.Duration : 0f;

        protected override void AnimationProcess(float process) {
            Rect.localScale = Mathf.Lerp(m_data.StartValue, m_data.EndValue, process) * Vector3.one;
        }
    }

    /// <summary>
    /// 动画序列
    /// </summary>
    public class UIAnimation_Sequence : UIAnimation
    {
        [SerializeField] UIAnimation[] m_anims;
        [SerializeField] float m_interval;

        public override float Duration {
            get {
                float result = 0;
                for (int i = 0; i < m_anims.Length; i++) {
                    if (m_anims[i] != null) {
                        result += m_anims[i].Duration;
                    }
                }
                return result;
            }
        }

        float[] keyPoints;

        public override void Init() {
            base.Init();
            keyPoints = new float[m_anims.Length];
            float timeSnap = 0;
            float duration = Duration;
            if (duration > 0) {
                for (int i = 0; i < m_anims.Length; i++) {
                    if (m_anims[i] != null) {
                        timeSnap += m_anims[i].Duration;
                        keyPoints[i] = timeSnap / Duration;
                    } else {
                        keyPoints[i] = timeSnap / Duration;
                    }
                }
            }
        }

        protected override void AnimationProcess(float process) {

            for (int i = 0; i < keyPoints[]; i++) {

            }

            // 有个当前动画,
            // 在当前动画之前,所有都已完成
            // 在当前动画之后,所有都未开始
            // TODO20240417
        }
    }
}