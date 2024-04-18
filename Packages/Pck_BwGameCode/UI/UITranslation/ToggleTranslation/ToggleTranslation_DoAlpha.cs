
using UnityEngine;
using UnityEngine.UI;

namespace BW.GameCode.UI
{
    public sealed class ToggleTranslation_DoAlpha : ToggleTranslation
    {
        [SerializeField] Graphic m_targetGraphic;
        [SerializeField] ToggleTranslationData_Float m_value;
        [SerializeField] float m_duration = 0.2f;

        protected override void DOTranslation(bool isOn) {
            if(m_targetGraphic!=null && m_value != null) {
                m_targetGraphic.CrossFadeAlpha(m_value.GetValue(isOn), m_duration, false);
            }
        }

        //protected override void PlayToggleTranslation(bool isOn) {
        //    m_targetGraphic.CrossFadeAlpha(isOn ? m_onAlpha : m_offAlpha, m_duration, false);
        //}
    }
}