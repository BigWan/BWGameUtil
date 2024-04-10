namespace BW.GameCode.UI
{
    using DG.Tweening;

    using UnityEngine;

    /// <summary>
    /// Recttransform.SizeDelta
    /// </summary>
    public class ButtonTranslation_SizeDelta : AbstractButtonTranslation
    {
        [SerializeField] RectTransform m_expandPart = default;
        [SerializeField] Vector2 m_commonSize;
        [SerializeField] Vector2 m_hoverSize;
        [SerializeField] Vector2 m_pressSize;
        [SerializeField] float m_animTime = 0.15f;

        public override void OnStateChanged(BWButton.ButtonState state) {
            switch (state) {
                case BWButton.ButtonState.Hover: DOSize(m_hoverSize); return;
                case BWButton.ButtonState.Pressed: DOSize(m_pressSize); return;
                case BWButton.ButtonState.Disable:
                case BWButton.ButtonState.Normal:
                default:
                    DOSize(m_commonSize);
                    return;
            }
        }

        private void OnDestroy() {
            m_expandPart.DOKill();
        }

        void DOSize(Vector2 size) {
            m_expandPart.DOSizeDelta(size, m_animTime);
        }
    }
}