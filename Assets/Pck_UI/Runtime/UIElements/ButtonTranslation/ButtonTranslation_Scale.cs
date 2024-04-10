namespace BW.GameCode.UI
{
    using DG.Tweening;

    using UnityEngine;

    /// <summary>
    /// 缩放
    /// </summary>
    public class ButtonTranslation_Scale : AbstractButtonTranslation
    {
        [SerializeField] Transform m_scalePart = default;

        //[SerializeField] float m_selectScale = 1.1f;
        [SerializeField] float m_hoverScale = 1.2f;
        [SerializeField] float m_pressedScale = 1.1f;
        [SerializeField] float m_animTime = 0.25f;

        public override void OnStateChanged(BWButton.ButtonState state) {
            switch (state) {
                //case AbstractButton.ButtonState.Selected:
                //    DOScale(m_selectScale);
                //    break;

                case BWButton.ButtonState.Pressed:
                    DOScale(m_pressedScale); break;
                //case AbstractButton.ButtonState.SelectedHover:
                case BWButton.ButtonState.Hover:
                    DOScale(m_hoverScale);
                    break;

                case BWButton.ButtonState.Disable:
                case BWButton.ButtonState.Normal:
                default:
                    DOScale(1);
                    break;
            }
        }

        private void DOScale(float target) {
            //m_scalePart.DOKill();
            m_scalePart.DOScale(target, m_animTime);
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