namespace BW.GameCode.Animation
{
    using UnityEngine;

    [RequireComponent(typeof(RectTransform))]
    public class AnimPart_AnchoredPosition : AnimPart
    {
        [System.Serializable]
        [System.Flags]
        protected enum MotionType
        {
            X = 1,
            Y = 2
        }

        [SerializeField] AnimPartData_Vector2 m_data;
        [SerializeField] MotionType m_type = MotionType.X | MotionType.Y;
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
            if (!m_type.HasFlag(MotionType.X)) {
                pos.x = Rect.anchoredPosition.x;
            }
            if (!m_type.HasFlag(MotionType.Y)) {
                pos.y = Rect.anchoredPosition.y;
            }
            //Debug.Log($"{this.transform.name} . AnchorPos = {pos},startValue={m_data.StartValue},endValue = {m_data.EndValue},process ={process}",this.transform);
            Debug.Assert(Rect != null, transform);
            Rect.anchoredPosition = pos;
        }
    }
}