namespace BW.GameCode.UI
{
    using System;
    using System.Collections;

    using UnityEngine;

    [RequireComponent(typeof(RectTransform))]
    public abstract class UIAnimation : MonoBehaviour
    {
        [SerializeField] float m_duration;
        [Header("动画延迟(运行时修改无效)")]

        RectTransform m_rect;
        protected RectTransform Rect => m_rect;
        public abstract float Duration { get; }

        public virtual void Init() {
            m_rect = transform as RectTransform;
            Debug.Assert(m_rect != null);
        }

        public IEnumerator Play(float speed) {
            yield return DoPlayAnimation(Duration, speed);
        }

        /// <summary>
        /// 处理动画进度
        /// </summary>
        /// <param name="process"></param>
        internal abstract void SetAnimationState(float process);

        IEnumerator DoPlayAnimation(float duration, float speed) {
            float elapsedTime = 0f;
            if (duration <= 0) {
                SetAnimationState(1);
                yield break;
            }
            while (elapsedTime < duration) {
                elapsedTime += Time.deltaTime * speed;
                float floatPercentage = Mathf.Clamp01(elapsedTime / duration);
                SetAnimationState(floatPercentage);
                yield return null;
            }

            SetAnimationState(1);
        }
    }
}