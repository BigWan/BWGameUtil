namespace BW.GameCode.Animation
{
    using UnityEngine;

    public class AnimPart_SizeDeltaY : AnimPart
    {
        [SerializeField] AnimPartData_Float m_data;
        public override float Duration => m_data.Duration;
        RectTransform Rect;

        public override void Init() {
            Rect = this.transform as RectTransform;
            base.Init();
        }

        protected override void SetAnimationState(float process) {
            Rect.sizeDelta = new Vector2(Rect.sizeDelta.x, m_data.GetValue(process));
        }
    }
}