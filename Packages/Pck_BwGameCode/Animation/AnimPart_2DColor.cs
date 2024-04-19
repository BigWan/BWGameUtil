namespace BW.GameCode.Animation
{
    using UnityEngine;

    public class AnimPart_2DColor : AnimPart
    {
        [SerializeField] AnimPartData_Color m_data = new AnimPartData_Color(Color.white,Color.white,0.25f);
        [SerializeField] SpriteRenderer m_renderer;
        public override float Duration => m_data.Duration;

        private void OnValidate() {
            if (m_renderer == null) {
                m_renderer = GetComponent<SpriteRenderer>();
            }
        }

        public override void Init() {
            if (m_renderer == null) {
                m_renderer = GetComponent<SpriteRenderer>();
            }
            Debug.Assert(m_renderer != null);
            base.Init();
        }

        protected override void SetAnimationState(float process) {
            m_renderer.color = m_data.GetValue(process);
        }
    }

}