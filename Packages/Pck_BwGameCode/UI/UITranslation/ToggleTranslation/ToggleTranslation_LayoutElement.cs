using UnityEngine;
using UnityEngine.UI;

namespace BW.GameCode.UI
{
    using BW.GameCode.Foundation;

    [RequireComponent(typeof(LayoutElement))]
    public class ToggleTranslation_LayoutElement : ToggleTranslation
    {
        public enum TranslateType
        {
            Hor,
            Ver,
        }

        [SerializeField] LayoutElement m_element = default;
        [SerializeField] ToggleTranslationData_Float m_value;
        [SerializeField] TranslateType m_translateType;
        [SerializeField] float m_duration = 0.15f;
        SimpleTween_Float tween = new SimpleTween_Float();

        private void UpdateElementWidth(float value) {
            m_element.preferredWidth = value;
        }

        private void UpdateElementHeight(float value) {
            m_element.preferredHeight = value;
        }

        protected override void OnValueChanged(bool isOn) {
    
            if (m_value != null && m_element != null) {
                if (m_translateType == TranslateType.Ver) {
                    tween.SetStartAndEnd(m_element.preferredHeight, m_value.GetValue(isOn));
                    tween.SetCallback(UpdateElementHeight);
                }
                if (m_translateType == TranslateType.Hor) {
                    tween.SetStartAndEnd(m_element.preferredWidth, m_value.GetValue(isOn));
                    tween.SetCallback(UpdateElementWidth);
                }
                tween.SetDuration(m_duration);
                tween.StartTween(this);
            }
        }

        protected override void OnValidate() {
            base.OnValidate();
            if (m_element == null) {
                m_element = GetComponent<LayoutElement>();
            }
        }
    }
}