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

        [SerializeField] float m_animTime = 0.25f;

        internal override void DoStateTransition(SelectableState state, bool instant) {
            if (m_scalePart != null && m_value != null) {
                DOScale(m_value.GetValue(state), instant);
            }
        }

        private void DOScale(Vector3 target, bool instant) {
            if (instant) {
                m_scalePart.localScale = target;
            } else {
                m_scalePart.DOKill();
                m_scalePart.DOScale(target, m_animTime);
            }
        }

        private void OnDestroy() {
            
        }

        private void OnValidate() {
            if (m_scalePart == null) {
                m_scalePart = this.transform;
            }
        }
    }
}