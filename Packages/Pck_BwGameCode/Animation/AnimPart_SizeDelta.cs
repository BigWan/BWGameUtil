namespace BW.GameCode.Animation
{
    using UnityEngine;

    public class AnimPart_SizeDelta : AnimPart
    {
        [SerializeField] AnimPartData_Vector2 m_data;
        public override float Duration => m_data.Duration;
        RectTransform Rect;
        public override void Init() {
            Rect = this.transform as RectTransform;
            base.Init();
            
        }

        protected override void SetAnimationState(float process) {
            Rect.sizeDelta = m_data.GetValue(process);
        }
    }

}