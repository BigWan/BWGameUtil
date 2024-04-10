namespace BW.GameCode.UI
{
    using DG.Tweening;

    using UnityEngine;

    /// <summary>
    /// 缩放
    /// </summary>
    public class ButtonTranslation_Scale3D : ButtonTransition
    {
        [SerializeField] Transform m_scalePart = default;

        [SerializeField] Vector3 m_selectScale = 1.1f * Vector3.one;
        [SerializeField] Vector3 m_hoverScale = 1.1f * Vector3.one;
        [SerializeField] Vector3 m_pressedScale = 1.1f * Vector3.one;
        [SerializeField] float m_animTime = 0.25f;

        internal override void DoStateTransition(BWButton.ButtonState state, bool instant) {
            switch (state) {
                //case AbstractButton.ButtonState.Selected:
                //    DOScale(m_selectScale);
                //    break;

                case BWButton.ButtonState.Pressed:
                    DOScale(m_pressedScale, instant); break;
                //case AbstractButton.ButtonState.SelectedHover:
                case BWButton.ButtonState.Highlighted:
                    DOScale(m_hoverScale, instant);
                    break;

                case BWButton.ButtonState.Disable:
                case BWButton.ButtonState.Normal:
                default:
                    DOScale(Vector3.one, instant);
                    break;
            }
        }


        private void DOScale(Vector3 target,bool instant) {
            if (instant) {
                m_scalePart.localScale = target;
            } else {

            m_scalePart.DOKill();
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