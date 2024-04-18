namespace BW.GameCode.Animation
{

    using UnityEngine;

    public class AnimPart_Scale : AnimPart
    {
        [SerializeField] AnimPartData_Float m_data;

        public override float Duration => m_data != null ? m_data.Duration : 0f;

        protected override void SetAnimationState(float process) {
            transform.localScale = Mathf.Lerp(m_data.StartValue, m_data.EndValue, process) * Vector3.one;
        }
    }
}