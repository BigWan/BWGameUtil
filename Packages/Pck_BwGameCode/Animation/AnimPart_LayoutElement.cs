namespace BW.GameCode.Animation
{
    using UnityEngine;
    using UnityEngine.UI;

    public class AnimPart_LayoutElement : AnimPart
    {
        [SerializeField] AnimPartData_Float m_data;
        [SerializeField] LayoutElement m_layoutElement;
        [Header("DrivenValue")] [SerializeField] DrivenValue m_drivenValue;

        protected enum DrivenValue
        {
            None,
            MiniWidth,
            MiniHeight,
            PreferredWidth,
            PreferredHeight,
            FlexibleWidth,
            FlexibleHeight,
        }

        public override float Duration => m_data.Duration;

        private void OnValidate() {
            if (m_layoutElement == null) {
                m_layoutElement = GetComponent<LayoutElement>();
            }
        }


        protected override void SetAnimationState(float process) {
            switch (m_drivenValue) {
                case DrivenValue.MiniWidth: m_layoutElement.minWidth = m_data.GetValue(process); break;
                case DrivenValue.MiniHeight: m_layoutElement.minHeight = m_data.GetValue(process); break;
                case DrivenValue.PreferredWidth: m_layoutElement.preferredWidth = m_data.GetValue(process); break;
                case DrivenValue.PreferredHeight: m_layoutElement.preferredHeight = m_data.GetValue(process); break;
                case DrivenValue.FlexibleWidth: m_layoutElement.flexibleWidth = m_data.GetValue(process); break;
                case DrivenValue.FlexibleHeight: m_layoutElement.flexibleHeight = m_data.GetValue(process); break;
                default:
                    break;
            }
        }
    }
}