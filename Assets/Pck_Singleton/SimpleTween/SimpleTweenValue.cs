using System;
using System.Collections;

using UnityEngine;

namespace BW.GameCode.Singleton
{
    /// <summary>
    /// 一个支持渐变的值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class SimpleTweenValue<T>
    {
        protected SimpleTweenValue(T startValue, T endValue, float duration, bool ignoreTimeScale = true) {
            this.StartValue = startValue;
            this.EndValue = endValue;
            Duration = duration;
            IgnoreTimeScale = ignoreTimeScale;
        }

        public T StartValue { get; }
        public T EndValue { get; }
        public float Duration { get; }
        public bool IgnoreTimeScale { get; }

        public event Action<T> callback;

        public void TweenValue(float progress) {
            var value = GetValue(StartValue,EndValue,progress);
            callback?.Invoke(value);
        }

        protected abstract T GetValue(T start,T end,float progress);

        public IEnumerator Start() {
            float elapsedTime = 0f;
            while (elapsedTime < Duration) {
                elapsedTime += (IgnoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime);
                float floatPercentage = Mathf.Clamp01(elapsedTime / Duration);
                TweenValue(floatPercentage);
                yield return null;
            }

            TweenValue(1f);
        }
    }
}