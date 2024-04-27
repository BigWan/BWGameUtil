using UnityEngine;
using UnityEngine.UI;

namespace BW.GameCode.UI
{
    using BW.GameCode.Foundation;

    public class ToggleTranslation_LayoutElement : ToggleTranslation
    {
        public enum TranslateType
        {
            Ver,
            Hor
        }

        [SerializeField] LayoutElement m_element = default;
        [SerializeField] ToggleTranslationData_Float m_value;
        [SerializeField] TranslateType m_translateType;

        SimpleTween_Float tween = new SimpleTween_Float();

        protected override void Awake() {
            tween.SetCallback(UpdateElement);
        }

        private void UpdateElementWidth(float process) {
            if (m_translateType == TranslateType.Hor) {
                m_element.preferredWidth = process;
            }
            if (m_translateType == TranslateType.Ver) {
                m_element.preferredHeight = process;
            }
        }

        protected override void DOTranslation(bool isOn) {
            if (m_value != null && m_element != null) {
                tween.SetStartAndEnd(m_value.GetValue(isOn));
            }
        }

        private void OnValidate() {
            if (m_element == null) {
                m_element = GetComponent<LayoutElement>();
            }
        }
    }
}