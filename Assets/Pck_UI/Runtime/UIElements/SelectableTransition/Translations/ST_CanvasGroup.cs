using DG.Tweening;

using UnityEngine;

namespace BW.GameCode.UI
{
    using static BW.GameCode.UI.SelectableAnimationController;

    /// <summary>
    /// 高亮一个Cg
    /// </summary>
    public class ST_CanvasGroup : SelectableTransition
    {
        [SerializeField] CanvasGroup m_canvasGroup = default;
        [SerializeField] STValue_Float m_value;
        [SerializeField] float m_animTime = 0.15f;

        internal override void DoStateTransition(SelectableState state, bool instant) {
            if (m_value != null && m_canvasGroup != null) {
                Fade(m_value.GetValue(state), instant);
            }
        }

        void Fade(float value, bool instant) {
            if (m_canvasGroup == null) {
                return;
            }

            m_canvasGroup.DOKill();
            if (instant) {
                m_canvasGroup.alpha = value;
            } else {
                m_canvasGroup.DOFade(value, m_animTime);
            }
        }


    }
}