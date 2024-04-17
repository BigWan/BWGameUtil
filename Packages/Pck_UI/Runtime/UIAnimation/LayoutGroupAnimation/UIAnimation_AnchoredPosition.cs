namespace BW.GameCode.UI
{

    using UnityEngine;

    public class UIAnimation_AnchoredPosition : UIAnimation
    {
        [SerializeField] UIAnimationData_Vector3 m_data;

        public override float Duration => m_data != null ? m_data.Duration : 0f;

        public override void Init() {
            base.Init();
            Rect.anchoredPosition = m_data.StartValue;
        }

        internal override void SetAnimationState(float process) {
            Rect.anchoredPosition = Vector2.Lerp(m_data.StartValue, m_data.EndValue, process);
        }
    }
}