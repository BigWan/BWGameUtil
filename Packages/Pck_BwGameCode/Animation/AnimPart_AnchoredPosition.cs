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
            base.Init();
            Debug.Assert(m_data != null);
            Debug.Assert(m_data.Duration > 0);
        }

        protected override void SetAnimationState(float process) {
            var pos = Vector2.Lerp(m_data.StartValue, m_data.EndValue, process);
            //Debug.Log($"{this.transform.name} . AnchorPos = {pos},startValue={m_data.StartValue},endValue = {m_data.EndValue},process ={process}",this.transform);
            Debug.Assert(Rect != null, transform);
            Rect.anchoredPosition = pos;
        }
    }
}