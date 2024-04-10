namespace BW.GameCode.UI
{
    using DG.Tweening;

    using UnityEngine;

    /// <summary>
    /// Recttransform.SizeDelta
    /// </summary>
    public class ButtonTranslation_SizeDelta : ButtonTransition
    {
        [SerializeField] RectTransform m_expandPart = default;
        [SerializeField] Vector2 m_commonSize;
        [SerializeField] Vector2 m_hoverSize;
        [SerializeField] Vector2 m_pressSize;
        [SerializeField] float m_animTime = 0.15f;

        internal override void DoStateTransition(BWButton.ButtonState state, bool instant) {
            switch (state) {
                case BWButton.ButtonState.Highlighted: DOSize(m_hoverSize, instant); return;
                case BWButton.ButtonState.Pressed: DOSize(m_pressSize, instant); return;
                case BWButton.ButtonState.Disable:
                case BWButton.ButtonState.Normal:
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