namespace BW.GameCode.Animation
{
    using UnityEngine;

    public class AnimPart_2DFade : AnimPart
    {
        [SerializeField] AnimPartData_Float m_data = new AnimPartData_Float(0,1,0.4f);
        [SerializeField] SpriteRenderer m_renderer;
        public override float Duration => m_data.Duration;

        private void OnValidate() {
            if (m_renderer == null) {
                m_renderer = GetComponent<SpriteRenderer>();
            }
        }

        public override void Init() {
            Debug.Assert(m_renderer != null);
            base.Init();
        }

        protected override void SetAnimationState(float process) {
            var c = m_renderer.color;
            c.a = m_data.GetValue(process);
            m_renderer.color = c;
        }
    }

}