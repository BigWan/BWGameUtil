using UnityEngine;

namespace BW.GameCode.UI
{
    public class ButtonTranslation_SwitchObject : ButtonTransition
    {
        [SerializeField] GameObject m_toggleObject = default;
        [SerializeField] bool m_defaultValue = false;

        internal override void DoStateTransition(BWButton.ButtonState state, bool instant) {
            switch (state) {
                case BWButton.ButtonState.Highlighted:
                    SetActive(!m_defaultValue);
                    break;

                default:
                    SetActive(m_defaultValue);
                    break;
            }
        }

        void SetActive(bool value) {
            if (m_toggleObject != null) {
                m_toggleObject.SetActive(value);
            }
        }
    }
}