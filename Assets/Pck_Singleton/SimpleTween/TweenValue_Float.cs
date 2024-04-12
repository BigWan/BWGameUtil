 
using UnityEngine;

namespace BW.GameCode.Singleton
{
    public class TweenValue_Float : SimpleTweenValue<float>
    {
        public TweenValue_Float(float startValue, float endValue, float duration, bool ignoreTimeScale = true) : base(startValue, endValue, duration, ignoreTimeScale) {
        }

        protected override float GetValue(float start, float end, float progress) {
            return Mathf.Lerp(start, end, progress);
        }
    }
}