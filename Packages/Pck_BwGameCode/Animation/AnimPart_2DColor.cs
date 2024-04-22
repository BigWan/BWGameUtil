namespace BW.GameCode.Animation
{
    using UnityEngine;

    public class AnimPart_2DColor : AnimPart
    {
        [SerializeField] AnimPartData_Color m_data = new AnimPartData_Color(Color.white, Color.white, 0.25f);
        [SerializeField] SpriteRenderer m_target;
        public override float Duration => m_data.Duration;



        private void OnValidate() {
            if (m_target == null) {
                m_target = GetComponent<SpriteRenderer>();
            }
        }

        protected override void SetAnimationState(float process) {
            m_target.color = m_data.GetValue(process);
        }
    }
}