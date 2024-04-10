
using DG.Tweening;

using UnityEngine;

namespace BW.GameCode.UI
{
    public sealed class ToggleTranslation_Expand : AbstractToggleTranslation
    {
        [SerializeField] RectTransform m_expandPart = default;
        [SerializeField] Vector2 m_sizeOff;
        [SerializeField] Vector2 m_sizeOn;
        [SerializeField] float m_animTime = 0.15f;

        public override void OnToggleChanged(bool isOn) {
            DOSize(isOn ? m_sizeOn : m_sizeOff);
        }

        void DOSize(Vector2 size) {
            m_expandPart.DOSizeDelta(size, m_animTime);
        }
    }
    
}