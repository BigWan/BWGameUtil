using BW.GameCode.Foundation;
using UnityEngine;
using UnityEngine.UI;

using static BW.GameCode.UI.SelectableAnimationController;

namespace BW.GameCode.UI
{
    public class ST_Color : SelectableTransition
    {
        [SerializeField] STValue_Color m_color;
        [SerializeField] Graphic m_image = default;
        [SerializeField] float m_animTime = 0.4f;
        
        internal override void DoStateTransition(SelectableState state, bool instant) {
            if (m_image == null || m_color == null) return;
            m_image.CrossFadeColor(m_color.GetValue(state), instant ? 0 : m_animTime, true, true);
        }

        private void OnValidate() {
            if (m_image == null) {
                m_image = GetComponent<Image>();
            }
            //OnStateChanged( AbstractToggle.AnimState.Normal);
        }

    }
}