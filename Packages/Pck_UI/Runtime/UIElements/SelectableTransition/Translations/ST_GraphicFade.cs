using UnityEngine;
using UnityEngine.UI;

using static BW.GameCode.UI.SelectableAnimationController;

namespace BW.GameCode.UI
{
    public class ST_GraphicFade : SelectableTransition
    {
        [SerializeField] Graphic m_graphic = default;
        [SerializeField] STValue_Bool m_value;
        [SerializeField] float m_duration = 0.4f;


        internal override void DoStateTransition(SelectableState state, bool instant) {
            if (m_value != null && m_graphic != null) {
                m_graphic.CrossFadeAlpha(m_value.GetValue(state)?1:0, instant ? 0 : m_duration, true);
            }
        }
    }
}