namespace BW.GameCode.Animation
{
    using UnityEngine;

    public class AnimPart_Anchors : AnimPart
    {
        [System.Serializable]
        protected enum MotionType
        {
            DOMax = 0,
            DOMin = 1
        }

        [SerializeField] AnimPartData_Vector2 m_data;
        [SerializeField] MotionType m_type = MotionType.DOMax;
        [SerializeField] RectTransform m_Rect;
        public override float Duration => m_data != null ? m_data.Duration : 0f;

        private void OnValidate() {
            if (m_Rect == null) {
                m_Rect = this.transform as RectTransform;
            }
        }

        protected override void SetAnimationState(float process) {
            var pos = Vector2.Lerp(m_data.StartValue, m_data.EndValue, process);

            //Debug.Log($"{this.transform.name} . AnchorPos = {pos},startValue={m_data.StartValue},endValue = {m_data.EndValue},process ={process}",this.transform);
            Debug.Assert(m_Rect != null, transform);
            if (m_type == MotionType.DOMax) {
                m_Rect.anchorMax = pos;
            } else {
                m_Rect.anchorMin = pos;
            }
        }
    }
}