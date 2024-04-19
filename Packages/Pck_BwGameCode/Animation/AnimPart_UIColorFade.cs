namespace BW.GameCode.Animation
{
    using UnityEngine;
    using UnityEngine.UI;

    public class AnimPart_UIColorFade : AnimPart
    {
        [SerializeField] AnimPartData_Float m_data;
        [SerializeField] Graphic m_graphic;
        public override float Duration => m_data.Duration;

        private void OnValidate() {
            if (m_graphic == null) {
                m_graphic = GetComponent<Graphic>();
            }
        }

        public override void Init() {
            Debug.Assert(m_graphic != null);
            base.Init();
        }

        protected override void SetAnimationState(float process) {
            var c = m_graphic.color;
            c.a = m_data.GetValue(process);
            m_graphic.color = c;
        }
    }
}