using DG.Tweening;

using UnityEngine;

namespace BW.GameCode.UI
{
    public class ButtonTranslation_CanvasFade : AbstractButtonTranslation
    {
        [SerializeField] CanvasGroup m_canvasGroup = default;
        [SerializeField] float m_animTime = 0.15f;

        public override void OnStateChanged(BWButton.ButtonState state) {
            switch (state) {
                //case AbstractButton.ButtonState.Selected:
                //case AbstractButton.ButtonState.SelectedHover:
                case BWButton.ButtonState.Hover:
                    Fade(1); break;
                case BWButton.ButtonState.Disable:
                case BWButton.ButtonState.Normal:
                default:
                    Fade(0); break;
            }
        }

        void Fade(float value) {
            if (m_canvasGroup != null) {
                m_canvasGroup.DOKill();
                m_canvasGroup.DOFade(value, m_animTime);
            }
        }

        private void OnDestroy() {
            m_canvasGroup.DOKill();
        }
    }
}