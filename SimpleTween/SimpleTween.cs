using System;
using System.Collections;

using UnityEngine;

namespace BW.GameCode.Core
{
    public delegate T EaseFunc<T>(T a, T b, float t);

    public class SimpleTween<T>
    {
        IEnumerator tweenInstance;

        public event Action<T> OnValueChanged;

        public MonoBehaviour Host { get; private set; }
        public float Duration { get; private set; } = 1f;
        public bool IgnoreTimeScale { get; private set; } = false;
        public T StartValue { get; set; } = default(T);
        public T EndValue { get; set; } = default(T);

        EaseFunc<T> mLerpFunc;

        public SimpleTween<T> SetLerp(EaseFunc<T> lerp) {
            mLerpFunc = lerp;
            return this;
        }

        public SimpleTween<T> SetDuration(float duration) {
            Duration = duration;
            return this;
        }

        public SimpleTween<T> SetStartAndEnd(T start, T end) {
            StartValue = start;
            EndValue = end;
            return this;
        }

        public SimpleTween<T> SetIgnoreTimeScale(bool value) {
            IgnoreTimeScale = value;
            return this;
        }

        public SimpleTween<T> SetCallback(Action<T> callback) {
            OnValueChanged += callback;
            return this;
        }

        public void StartTween(MonoBehaviour host) {
            this.Host = host;
            if (Host == null) {
                return;
            }
            StopTween();
            tweenInstance = SpawnIEnumerator();
            Host.StartCoroutine(tweenInstance);
        }

        public void StopTween() {
            if (tweenInstance != null) {
                Host.StopCoroutine(tweenInstance);
                tweenInstance = null;
            }
        }

        void TweenValueAndRaiseEvent(float progress) {
            var value = mLerpFunc(StartValue, EndValue, progress);
            OnValueChanged?.Invoke(value);
        }

        IEnumerator SpawnIEnumerator() {
            float elapsedTime = 0f;
            while (elapsedTime < Duration) {
                elapsedTime += (IgnoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime);
                float floatPercentage = Mathf.Clamp01(elapsedTime / Duration);
                TweenValueAndRaiseEvent(floatPercentage);
                yield return null;
            }

            TweenValueAndRaiseEvent(1f);
        }
    }
}