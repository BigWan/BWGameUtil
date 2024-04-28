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
            Ver,
            Hor
        }

        [SerializeField] LayoutElement m_element = default;
        [SerializeField] ToggleTranslationData_Float m_value;
        [SerializeField] TranslateType m_translateType;

        SimpleTween_Float tween = new SimpleTween_Float();

        protected override void Awake() {
        }

        private void UpdateElementWidth(float value) {
            m_element.preferredWidth = value;
        }

        private void UpdateElementHeight(float value) {
            m_element.preferredHeight = value;
        }

        protected override void DOTranslation(bool isOn) {
            if (m_value != null && m_element != null) {
                if (m_translateType == TranslateType.Ver) {
                    tween.SetStartAndEnd(m_element.preferredWidth, m_value.GetValue(isOn));
                    tween.SetCallback(UpdateElementWidth);
                }
                if (m_translateType == TranslateType.Hor) {
                    tween.SetStartAndEnd(m_element.preferredHeight, m_value.GetValue(isOn));
                    tween.SetCallback(UpdateElementHeight);
                }
                tween.StartTween(this);
            }
        }

        private void OnValidate() {
            if (m_element == null) {
                m_element = GetComponent<LayoutElement>();
            }
        }
    }
}