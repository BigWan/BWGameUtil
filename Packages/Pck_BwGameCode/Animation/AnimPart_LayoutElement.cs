namespace BW.GameCode.Animation
{
    using UnityEngine;
    using UnityEngine.UI;

    public class AnimPart_LayoutElement : AnimPart
    {
        [SerializeField] AnimPartData_Float m_data;
        [SerializeField] LayoutElement m_le;
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

        public override void Init() {
            if (m_le == null) {
                m_le = GetComponent<LayoutElement>();
            }
            base.Init();
        }

        protected override void SetAnimationState(float process) {
            switch (m_drivenValue) {
                case DrivenValue.MiniWidth:  m_le.minWidth = m_data.GetValue(process); break;
                case DrivenValue.MiniHeight: m_le.minHeight = m_data.GetValue(process); break;
                case DrivenValue.PreferredWidth: m_le.preferredWidth = m_data.GetValue(process); break;
                case DrivenValue.PreferredHeight: m_le.preferredHeight = m_data.GetValue(process); break;
                case DrivenValue.FlexibleWidth: m_le.flexibleWidth = m_data.GetValue(process); break;
                case DrivenValue.FlexibleHeight: m_le.flexibleHeight = m_data.GetValue(process); break;
                default:
                    break;
            }
        }
    }
}