using UnityEngine;
using UnityEngine.UI;

namespace BW.GameCode.UI
{
    using BW.GameCode.Foundation;

    using System;

    public class ToggleTranslation_LayoutElement : ToggleTranslation
    {
        [SerializeField] LayoutElement m_element = default;
        [SerializeField] ToggleTranslationData_Float m_value;
        [SerializeField] bool m_ver;
        [SerializeField] bool m_hor;

        SimpleTween_Float tween = new SimpleTween_Float();

        protected override void Awake() {
            tween.SetCallback(UpdateElement);
        }

        private void UpdateElement(float obj) {
            throw new NotImplementedException();
        }

        protected override void DOTranslation(bool isOn) {
            if (m_value != null && m_element != null) {
                DOSize(m_value.GetValue(isOn));
            }
        }

        private void DOSize(float v) {
        }

        private void OnValidate() {
            if (m_element == null) {
                m_element = GetComponent<LayoutElement>();
            }
        }
    }

    public sealed class ToggleTranslation_Expand : ToggleTranslation
    {
        [SerializeField] RectTransform m_expandPart = default;
        [SerializeField] ToggleTranslationData_Vector2 m_value;
        //[SerializeField] Vector2 m_sizeOff;
        //[SerializeField] Vector2 m_sizeOn;
        [SerializeField] float m_animTime = 0.15f;

        SimpleTween_V2 tween = new SimpleTween_V2();

        protected override void Awake() {
            tween.SetCallback(x => {
                if (m_expandPart != null) {
                    m_expandPart.sizeDelta = x;
                }
            });
            base.Awake();
        }

        protected override void DOTranslation(bool isOn) {
            if (m_value != null && m_expandPart != null) {
                DOSize(m_value.GetValue(isOn));
            }
        }

        void DOSize(Vector2 size) {
            tween.SetStartAndEnd(m_expandPart.sizeDelta, size).SetDuration(m_animTime).StartTween(this);
        }
    }
}