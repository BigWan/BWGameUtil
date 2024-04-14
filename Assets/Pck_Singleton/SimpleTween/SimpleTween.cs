using System;
using System.Collections;

using UnityEngine;

namespace BW.GameCode.Singleton
{
    public class SimpleTween<T>
    {
        IEnumerator tweenProgress;

        Func<T, T, float, T> mLerpFunc;
        public event Action<T> OnValueChanged;
        public MonoBehaviour Host {
            get; set;
        }

        public SimpleTween(Func<T, T, float, T> lerpFunc) {
            mLerpFunc = lerpFunc;
        }

        public float Duration { get; set; }
        public bool IgnoreTimeScale { get; set; }
        public T StartValue { get; set; }
        public T EndValue { get; set; }

        public void Init(MonoBehaviour host,T start,T end,float duration) {
            Host = host;
            StartValue = start;
            EndValue = end;
            Duration = duration;
        }

        public void StartTween() {
            if (Host == null) {
                return;
            }
            StopTween();
            tweenProgress = SpawnIenumerator();
            Host.StartCoroutine(tweenProgress);
        }

        public void StopTween() {
            if (tweenProgress != null) {
                Host.StopCoroutine(tweenProgress);
                tweenProgress = null;
            }
        }

        void TweenValueAndRaiseEvent(float progress) {
            var value = mLerpFunc(StartValue, EndValue, progress);
            OnValueChanged?.Invoke(value);
        }

        IEnumerator SpawnIenumerator() {
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