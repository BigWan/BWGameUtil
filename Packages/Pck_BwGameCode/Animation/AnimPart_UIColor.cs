namespace BW.GameCode.Animation
{
    using UnityEngine;
    using UnityEngine.UI;

    [RequireComponent(typeof(Graphic))]
    public class AnimPart_UIColor : AnimPart
    {
        [SerializeField] AnimPartData_Color m_data = new AnimPartData_Color(Color.white, Color.white, 0.4f);
        [SerializeField] Graphic m_graphic;
        public override float Duration => m_data.Duration;

        private void OnValidate() {
            if (m_graphic == null) {
                m_graphic = GetComponent<Graphic>();
            }
        }

        protected override void SetAnimationState(float process) {
            m_graphic.color = m_data.GetValue(process);
        }
    }
}