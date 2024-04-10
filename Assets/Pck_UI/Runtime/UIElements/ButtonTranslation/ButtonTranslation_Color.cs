using UnityEngine;

namespace BW.GameCode.UI
{
    public abstract class ButtonTranslation_Color : ButtonTransition
    {
        [SerializeField] BWButtonTranslationColorSO m_colorPair;
        [SerializeField] float m_animTime = 0.4f;
        internal override void DoStateTransition(BWButton.ButtonState state, bool instant) {
            
            switch (state) {
                case BWButton.ButtonState.Disable: SetColor(m_colorPair == null ? Color.grey : m_colorPair.DisableColor, m_animTime, instant); break;
                case BWButton.ButtonState.Pressed: SetColor(m_colorPair == null ? Color.white : m_colorPair.PressColor, m_animTime, instant); break;
                case BWButton.ButtonState.Highlighted: SetColor(m_colorPair == null ? Color.white : m_colorPair.HoverColor, m_animTime, instant); break;
                case BWButton.ButtonState.Normal:
                default:
                    SetColor(m_colorPair.CommonColor, m_animTime, instant);
                    break;
            }
        }

        protected abstract void SetColor(Color color,float time,bool instant);
    }
}