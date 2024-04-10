using DG.Tweening;

using UnityEngine;
using UnityEngine.UI;

namespace BW.GameCode.UI
{
    public class ButtonTranslation_ImageFade : ButtonTransition
    {
        [SerializeField] Graphic m_toggleImage = default;

        private void OnDestroy() {
            m_toggleImage.DOKill();
        }

        internal override void DoStateTransition(BWButton.ButtonState state, bool instant) {
            switch (state) {
                case BWButton.ButtonState.Normal:
                case BWButton.ButtonState.Highlighted:
                case BWButton.ButtonState.Pressed:
                case BWButton.ButtonState.Selected:
                    TweenAlpha(1, instant); break;
                case BWButton.ButtonState.Disable:
                default:
                    TweenAlpha(0, instant); break;
            }
        }

        void TweenAlpha(float value, bool instant) {
            //Debug.Assert(m_toggleImage != null);
            if (m_toggleImage != null) {
                m_toggleImage.DOKill();
                m_toggleImage.DOFade(value, 0.15f);
                //Debug.Log($"Tween Fade {value}");
            } else {
                Debug.Log("Tween ALpha Image is null");
            }
        }
    }
}