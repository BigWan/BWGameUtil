namespace BW.GameCode.Animation
{
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Graphic))]
    public class AnimPart_UIColor : AnimPart
    {
        [SerializeField] AnimPartData_Color m_data;
        [SerializeField] Graphic m_graphic;
        public override float Duration => m_data.Duration;

        private void OnValidate() {
            if (m_graphic == null) {
                m_graphic = GetComponent<Graphic>();
            }
        }

        public override void Init() {
            if (m_graphic == null) {
                m_graphic = GetComponent<Graphic>();
            }
            Debug.Assert(m_graphic != null);
            base.Init();
        }

        protected override void SetAnimationState(float process) {
            m_graphic.color = m_data.GetValue(process);
        }
    }
}