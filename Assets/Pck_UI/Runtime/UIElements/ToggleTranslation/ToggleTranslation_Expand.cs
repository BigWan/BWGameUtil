using DG.Tweening;

using UnityEngine;

namespace BW.GameCode.UI
{
    public sealed class ToggleTranslation_Expand : ToggleTranslation
    {
        [SerializeField] RectTransform m_expandPart = default;
        [SerializeField] ToggleTranslationData_V2 m_value;
        //[SerializeField] Vector2 m_sizeOff;
        //[SerializeField] Vector2 m_sizeOn;
        [SerializeField] float m_animTime = 0.15f;

        protected override void DOTranslation(bool isOn) {
            if (m_value != null && m_expandPart != null) {
                DOSize(m_value.GetValue(isOn));
            }
        }

        void DOSize(Vector2 size) {
            m_expandPart.DOSizeDelta(size, m_animTime);
        }
    }
}