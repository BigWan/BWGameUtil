namespace BW.GameCode.Animation
{
    using UnityEngine;

    [RequireComponent(typeof(RectTransform))]
    public class AnimPart_AnchoredPosition : AnimPart
    {
        [SerializeField] AnimPartData_Vector2 m_data;
        RectTransform Rect;
        public override float Duration => m_data != null ? m_data.Duration : 0f;

        public override void Init() {
            Rect = this.transform as RectTransform;
            Debug.Log(this.transform.name);
            base.Init();
        }

        protected override void SetAnimationState(float process) {
            Rect.anchoredPosition = Vector2.Lerp(m_data.StartValue, m_data.EndValue, process);
        }
    }
}