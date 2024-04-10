using UnityEngine;

namespace BW.GameCode.UI
{
    public class ButtonTranslation_SwitchObject : AbstractButtonTranslation
    {
        [SerializeField] GameObject m_toggleObject = default;
        [SerializeField] bool m_defaultValue = false;
        public override void OnStateChanged(BWButton.ButtonState state) {
            switch (state) {
                //case AbstractButton.ButtonState.Selected:
                //case AbstractButton.ButtonState.SelectedHover:
                //    SetActive(true); break;
                case BWButton.ButtonState.Hover:
                    SetActive(!m_defaultValue); break;
                case BWButton.ButtonState.Disable:
                case BWButton.ButtonState.Normal:
                default:
                    SetActive(m_defaultValue); break;
            }
        }

        void SetActive(bool value) {
            if (m_toggleObject != null) {
                m_toggleObject.SetActive(value);
            }
        }
    }
}