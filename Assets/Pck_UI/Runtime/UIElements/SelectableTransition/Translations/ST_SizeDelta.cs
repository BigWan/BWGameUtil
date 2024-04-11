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
        [SerializeField] STValue_V2 m_value;
        [SerializeField] float m_animTime = 0.15f;

        internal override void DoStateTransition(SelectableState state, bool instant) {
            if(m_expandPart!=null && m_value != null) {
                DOSize(m_value.GetValue(state), instant);
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