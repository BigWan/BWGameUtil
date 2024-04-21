namespace BW.GameCode.Animation
{
    using UnityEngine;

    public class AnimPart_SizeDelta : AnimPart
    {
        [SerializeField] AnimPartData_Vector2 m_data;
        [SerializeField] MotionType m_motionType = MotionType.Width | MotionType.Height;
        public override float Duration => m_data.Duration;
        RectTransform Rect;
        [System.Serializable]
        [System.Flags]
        protected enum MotionType
        {
            None,
            Width,
            Height

        }
        public override void Init() {
            Rect = this.transform as RectTransform;
            base.Init();
        }

        protected override void SetAnimationState(float process) {

            var sizeDelta = m_data.GetValue(process);
            if (!m_motionType.HasFlag(MotionType.Width)) {
                sizeDelta.x = Rect.sizeDelta.x;
            }
            if (!m_motionType.HasFlag(MotionType.Height)) {
                sizeDelta.y = Rect.sizeDelta.y;
            }

            Rect.sizeDelta = sizeDelta;
        }
    }

}