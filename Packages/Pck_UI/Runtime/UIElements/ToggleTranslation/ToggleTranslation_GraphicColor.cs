using UnityEngine;
using UnityEngine.UI;

namespace BW.GameCode.UI
{
    public sealed class ToggleTranslation_GraphicColor : ToggleTranslation
    {
        [SerializeField] Graphic m_targetGraphic;
        [SerializeField] ToggleTranslationData_Color m_value;
        [SerializeField] float m_duration = 0.2f;

        //public override void OnToggleChanged(bool isOn) {
        //    m_targetGraphic.CrossFadeColor(isOn ? m_colorOn : m_colorOff, m_duration, false, false);
        //}

        protected override void DOTranslation(bool isOn) {
            if (m_targetGraphic != null && m_value != null) {
                m_targetGraphic.CrossFadeColor(m_value.GetValue(isOn), m_duration, false, false);
            }
        }
    }
}