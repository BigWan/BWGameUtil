using System;

using UnityEngine;

namespace BW.GameCode.Singleton
{
    public class TweenValue_Color : SimpleTweenValue<Color>
    {
        public TweenValue_Color(Color startValue, Color endValue, float duration, bool ignoreTimeScale = true) : base(startValue, endValue, duration, ignoreTimeScale) {
        }



        protected override Color GetValue(Color start, Color end, float progress) {
            return Color.Lerp(start, end, progress);
        }
    }
}