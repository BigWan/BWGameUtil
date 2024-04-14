namespace BW.GameCode.UI
{
    using BW.GameCode.Core;

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
        SimpleTween<Vector2> runner = new SimpleTween<Vector2>();

        private void Awake() {
            runner.SetCallback((x) => {
                if (m_expandPart != null) {
                    m_expandPart.sizeDelta = x;
                }
            })
            .SetDuration(m_animTime)
            .SetLerp(Vector2.Lerp);
        }
        internal override void DoStateTransition(SelectableState state, bool instant) {
            if(m_expandPart!=null && m_value != null) {
                DOSize(m_value.GetValue(state), instant);
            }
        }

        private void OnDestroy() {
            m_expandPart.DOKill();
        }

        void DOSize(Vector2 value, bool instant) {
            if (instant) {
                m_expandPart.sizeDelta = value;
            } else {
                runner.SetStartAndEnd(m_expandPart.sizeDelta, value);
                runner.StartTween(this);
            }
        }
    }
}