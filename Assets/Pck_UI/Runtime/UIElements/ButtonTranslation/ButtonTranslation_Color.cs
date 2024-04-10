using UnityEngine;

namespace BW.GameCode.UI
{
    public abstract class ButtonTranslation_Color : AbstractButtonTranslation
    {
        [SerializeField] BWButtonTranslationColorSO m_colorPair;
        [SerializeField] float m_animTime = 0.4f;
        public override void OnStateChanged(BWButton.ButtonState state) {
            switch (state) {
                case BWButton.ButtonState.Disable: SetColor(m_colorPair == null ? Color.grey : m_colorPair.DisableColor, m_animTime); break;
                case BWButton.ButtonState.Pressed: SetColor(m_colorPair == null ? Color.white : m_colorPair.PressColor, m_animTime); break;
                case BWButton.ButtonState.Hover: SetColor(m_colorPair == null ? Color.white : m_colorPair.HoverColor, m_animTime); break;
                case BWButton.ButtonState.Normal:
                default:
                    SetColor(m_colorPair.CommonColor, m_animTime);
                    break;
            }
        }

        protected abstract void SetColor(Color color,float time);
    }
}