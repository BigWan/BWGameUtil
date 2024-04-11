namespace BW.GameCode.UI
{
    using DG.Tweening;

    using UnityEngine;

    using static BW.GameCode.UI.SelectableAnimationController;

    /// <summary>
    /// 缩放
    /// </summary>
    public class ST_Scale3D : SelectableTransition
    {
        [SerializeField] Transform m_scalePart = default;
        [SerializeField] STValue_V3 m_value;
        [SerializeField] Vector3 m_selectScale = 1.1f * Vector3.one;
        [SerializeField] Vector3 m_hoverScale = 1.1f * Vector3.one;
        [SerializeField] Vector3 m_pressedScale = 1.1f * Vector3.one;
        [SerializeField] float m_animTime = 0.25f;

        internal override void DoStateTransition(SelectableState state, bool instant) {
            switch (state) {
                case SelectableState.Selected:
                    DOScale(m_selectScale,instant);
                    break;

                case SelectableState.Pressed:
                    DOScale(m_pressedScale, instant); break;
                //case AbstractButton.ButtonState.SelectedHover:
                case SelectableState.Highlighted:
                    DOScale(m_hoverScale, instant);
                    break;

                case SelectableState.Disabled:
                case SelectableState.Normal:
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