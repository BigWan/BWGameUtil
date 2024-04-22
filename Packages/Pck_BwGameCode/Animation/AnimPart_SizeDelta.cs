namespace BW.GameCode.Animation
{
    using UnityEngine;

    public class AnimPart_SizeDelta : AnimPart
    {
        [SerializeField] AnimPartData_Vector2 m_data;
        [SerializeField] MotionType m_motionType = MotionType.Width | MotionType.Height;
        public override float Duration => m_data.Duration;
        [SerializeField]RectTransform m_rect;

        private void OnValidate() {
            if (m_rect == null) {
            m_rect = this.transform as RectTransform;

            }
        }
        [System.Serializable]
        [System.Flags]
        protected enum MotionType
        {
            None,
            Width,
            Height

        }


        protected override void SetAnimationState(float process) {

            var sizeDelta = m_data.GetValue(process);
            if (!m_motionType.HasFlag(MotionType.Width)) {
                sizeDelta.x = m_rect.sizeDelta.x;
            }
            if (!m_motionType.HasFlag(MotionType.Height)) {
                sizeDelta.y = m_rect.sizeDelta.y;
            }

            m_rect.sizeDelta = sizeDelta;
        }
    }

}