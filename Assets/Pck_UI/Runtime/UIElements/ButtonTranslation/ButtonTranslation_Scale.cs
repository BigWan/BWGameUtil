namespace BW.GameCode.UI
{
    using DG.Tweening;

    using UnityEngine;

    /// <summary>
    /// 缩放
    /// </summary>
    public class ButtonTranslation_Scale : ButtonTransition
    {
        [SerializeField] Transform m_scalePart = default;

        //[SerializeField] float m_selectScale = 1.1f;
        [SerializeField] float m_hoverScale = 1.2f;
        [SerializeField] float m_pressedScale = 1.1f;
        [SerializeField] float m_animTime = 0.25f;
        internal override void DoStateTransition(BWButton.ButtonState state, bool instant) {
            switch (state) {
                //case AbstractButton.ButtonState.Selected:
                //    DOScale(m_selectScale);
                //    break;

                case BWButton.ButtonState.Pressed:
                    DOScale(m_pressedScale,instant); break;
                //case AbstractButton.ButtonState.SelectedHover:
                case BWButton.ButtonState.Highlighted:
                    DOScale(m_hoverScale, instant);
                    break;
                case BWButton.ButtonState.Disable:
                case BWButton.ButtonState.Normal:
                default:
                    DOScale(1, instant);
                    break;
            }
        }

        private void DOScale(float target, bool instant) {
            //m_scalePart.DOKill();
            if (instant) {
                m_scalePart.localScale = target * Vector3.one;
            } else {
                m_scalePart.DOScale(target, m_animTime);
            }
        }

        private void OnDestroy() {
            m_scalePart.DOKill();
        }

        private void OnValidate() {
            if (m_scalePart == null) {
                m_scalePart = this.transform;
            }
        }
    }
}