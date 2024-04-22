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
        [SerializeField] RectTransform m_Rect;
        public override float Duration => m_data != null ? m_data.Duration : 0f;

        private void OnValidate() {
            if (m_Rect == null) {
                m_Rect = this.transform as RectTransform;
            }
        }


        protected override void SetAnimationState(float process) {
            var pos = Vector2.Lerp(m_data.StartValue, m_data.EndValue, process);
            if (!m_type.HasFlag(MotionType.X)) {
                pos.x = m_Rect.anchoredPosition.x;
            }
            if (!m_type.HasFlag(MotionType.Y)) {
                pos.y = m_Rect.anchoredPosition.y;
            }
            //Debug.Log($"{this.transform.name} . AnchorPos = {pos},startValue={m_data.StartValue},endValue = {m_data.EndValue},process ={process}",this.transform);
            Debug.Assert(m_Rect != null, transform);
            m_Rect.anchoredPosition = pos;
        }
    }
}