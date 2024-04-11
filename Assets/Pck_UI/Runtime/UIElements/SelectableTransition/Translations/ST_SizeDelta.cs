namespace BW.GameCode.UI
{
    using DG.Tweening;

    using UnityEngine;

    using static BW.GameCode.UI.SelectableAnimationController;

    /// <summary>
    /// Recttransform.SizeDelta
    /// </summary>
    public class ST_SizeDelta : SelectableTransition
    {
        [SerializeField] RectTransform m_expandPart = default;
        [SerializeField] STValue_Float m_value;
        [SerializeField] Vector2 m_commonSize;
        [SerializeField] Vector2 m_hoverSize;
        [SerializeField] Vector2 m_pressSize;
        [SerializeField] float m_animTime = 0.15f;

        internal override void DoStateTransition(SelectableState state, bool instant) {
            switch (state) {
                case SelectableState.Highlighted: DOSize(m_hoverSize, instant); return;
                case SelectableState.Pressed: DOSize(m_pressSize, instant); return;
                case SelectableState.Disabled:
                case SelectableState.Normal:
                default:
                    DOSize(m_commonSize,instant);
                    return;
            }
        }

        private void OnDestroy() {
            m_expandPart.DOKill();
        }

        void DOSize(Vector2 size, bool instant) {
            if (instant) {
                m_expandPart.sizeDelta = size;
            } else {
                m_expandPart.DOSizeDelta(size, m_animTime);
            }
        }
    }
}