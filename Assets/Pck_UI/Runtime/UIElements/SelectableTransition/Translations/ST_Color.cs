using UnityEngine;

using static BW.GameCode.UI.SelectableAnimationController;

namespace BW.GameCode.UI
{
    public abstract class ST_Color : SelectableTransition
    {
        [SerializeField] STValue_Color m_color;
        [SerializeField] BWButtonTranslationColorSO m_colorPair;
        [SerializeField] float m_animTime = 0.4f;
        internal override void DoStateTransition(SelectableState state, bool instant) {
            
            switch (state) {
                case SelectableState.Disabled: SetColor(m_colorPair == null ? Color.grey : m_colorPair.DisableColor, m_animTime, instant); break;
                case SelectableState.Pressed: SetColor(m_colorPair == null ? Color.white : m_colorPair.PressColor, m_animTime, instant); break;
                case SelectableState.Highlighted: SetColor(m_colorPair == null ? Color.white : m_colorPair.HoverColor, m_animTime, instant); break;
                case SelectableState.Normal:
                default:
                    SetColor(m_colorPair.CommonColor, m_animTime, instant);
                    break;
            }
        }

        protected abstract void SetColor(Color color,float time,bool instant);
    }
}