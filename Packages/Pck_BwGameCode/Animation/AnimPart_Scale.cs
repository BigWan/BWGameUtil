namespace BW.GameCode.Animation
{
    using UnityEngine;

    public class AnimPart_Scale : AnimPart
    {
        [SerializeField] AnimPartData_Float m_data = new AnimPartData_Float(1,1,0.4f);

        public override float Duration => m_data != null ? m_data.Duration : 0f;



        protected override void SetAnimationState(float process) {
            transform.localScale = m_data.GetValue(process) * Vector3.one;
        }
    }
}