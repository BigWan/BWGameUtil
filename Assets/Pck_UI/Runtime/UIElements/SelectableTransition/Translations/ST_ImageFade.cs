using UnityEngine;
using UnityEngine.UI;

using static BW.GameCode.UI.SelectableAnimationController;

namespace BW.GameCode.UI
{
    public class ST_ImageFade : SelectableTransition
    {
        [SerializeField] Graphic m_toggleImage = default;
        [SerializeField] STValue_Float m_value;
        [SerializeField] float m_duration = 0.4f;


        internal override void DoStateTransition(SelectableState state, bool instant) {
            if (m_value != null && m_toggleImage != null) {
                m_toggleImage.CrossFadeAlpha(m_value.GetValue(state), instant ? 0 : m_duration, true);
            }
        }
    }
}