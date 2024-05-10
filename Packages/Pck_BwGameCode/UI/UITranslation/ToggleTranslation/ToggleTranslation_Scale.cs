using BW.GameCode.Foundation;

using UnityEngine;

namespace BW.GameCode.UI
{
    public sealed class ToggleTranslation_Scale : ToggleTranslation
    {
        [SerializeField] Transform m_scalePart;
        [SerializeField] ToggleTranslationData_Float m_value;
        [SerializeField] float m_animTime = 0.25f;

        SimpleTween_Float m_tween = new SimpleTween_Float();

        protected override void Awake() {
            base.Awake();
            m_tween.SetUpdateCall(x => { if (m_scalePart != null) { m_scalePart.localScale = Vector3.one * x; } })
                ;
        }

        protected override void OnValueChanged(bool isOn) {
            m_tween.SetStartAndEnd(m_scalePart.localScale.x, m_value.GetValue(isOn)).SetDuration(m_animTime)
                .StartTween(this);
        }
    }
}