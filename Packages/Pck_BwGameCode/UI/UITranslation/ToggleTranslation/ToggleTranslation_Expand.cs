using UnityEngine;

namespace BW.GameCode.UI
{
    using BW.GameCode.Foundation;

    [RequireComponent(typeof(RectTransform))]
    public sealed class ToggleTranslation_Expand : ToggleTranslation
    {
        [SerializeField] RectTransform m_expandPart = default;
        [SerializeField] ToggleTranslationData_Vector2 m_value;
        //[SerializeField] Vector2 m_sizeOff;
        //[SerializeField] Vector2 m_sizeOn;
        [SerializeField] float m_animTime = 0.15f;

        SimpleTween_V2 tween = new SimpleTween_V2();

        private void OnValidate() {
            if (m_expandPart == null) {
                m_expandPart = GetComponent<RectTransform>();
            }
        }

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