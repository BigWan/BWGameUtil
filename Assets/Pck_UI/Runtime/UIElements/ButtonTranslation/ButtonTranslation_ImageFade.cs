using DG.Tweening;

using UnityEngine;
using UnityEngine.UI;

namespace BW.GameCode.UI
{
    public class ButtonTranslation_ImageFade : AbstractButtonTranslation
    {
        [SerializeField] Graphic m_toggleImage = default;

        private void OnDestroy() {
            m_toggleImage.DOKill();
        }

        public override void OnStateChanged(BWButton.ButtonState state) {
            switch (state) {
                //case AbstractButton.ButtonState.Selected:
                //case AbstractButton.ButtonState.SelectedHover:
                case BWButton.ButtonState.Hover:
                case BWButton.ButtonState.Pressed:
                    TweenAlpha(1); break;
                case BWButton.ButtonState.Disable:
                case BWButton.ButtonState.Normal:
                default:
                    TweenAlpha(0); break;
            }
        }

        void TweenAlpha(float value) {
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