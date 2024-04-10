using DG.Tweening;

using UnityEngine;

namespace BW.GameCode.UI
{
    public class ButtonTranslation_CanvasFade : ButtonTransition
    {
        [SerializeField] CanvasGroup m_canvasGroup = default;
        [SerializeField] float m_animTime = 0.15f;

        internal override void DoStateTransition(BWButton.ButtonState state, bool instant) {
            switch (state) {
                //case AbstractButton.ButtonState.Selected:
                //case AbstractButton.ButtonState.SelectedHover:
                case BWButton.ButtonState.Highlighted:
                    Fade(1, instant); break;

                default:
                    Fade(0, instant); break;
            }
        }

        void Fade(float value, bool instant) {
            if (m_canvasGroup == null) {
                return;
            }

            m_canvasGroup.DOKill();
            if (instant) {
                m_canvasGroup.alpha = value;
            } else {
                m_canvasGroup.DOFade(value, m_animTime);
            }
        }

        private void OnDestroy() {
            m_canvasGroup.DOKill();
        }
    }
}