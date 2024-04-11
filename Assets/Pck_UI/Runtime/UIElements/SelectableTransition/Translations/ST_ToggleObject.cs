using UnityEngine;

using static BW.GameCode.UI.SelectableAnimationController;

namespace BW.GameCode.UI
{
    public class ST_ToggleObject : SelectableTransition
    {
        [SerializeField] GameObject m_toggleObject = default;
        [SerializeField] STValue_Bool m_value;

        internal override void DoStateTransition(SelectableState state, bool instant) {
            if (m_value != null) {
                SetActive(m_value.GetValue(state));
            }
        }

        void SetActive(bool value) {
            if (m_toggleObject != null) {
                m_toggleObject.SetActive(value);
            }
        }
    }
}