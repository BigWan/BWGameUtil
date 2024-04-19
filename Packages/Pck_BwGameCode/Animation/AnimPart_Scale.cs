namespace BW.GameCode.Animation
{
    using UnityEngine;

    public class AnimPart_Scale : AnimPart
    {
        [SerializeField] AnimPartData_Float m_data;

        public override float Duration => m_data != null ? m_data.Duration : 0f;

        public override void Init() {
            base.Init();
        }

        protected override void SetAnimationState(float process) {
            transform.localScale = m_data.GetValue(process) * Vector3.one;
        }
    }
}