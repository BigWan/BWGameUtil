namespace BW.GameCode.Animation
{
    using UnityEngine;
    using UnityEngine.UI;

    public class AnimPart_UIColorFade : AnimPart
    {
        [SerializeField] AnimPartData_Float m_data;
        [SerializeField] Graphic m_graphic;
        public override float Duration => m_data.Duration;

        public override void Init() {
            if (m_graphic == null) {
                m_graphic = GetComponent<Graphic>();
            }
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