namespace BW.GameCode.Animation
{
    using UnityEngine;
    using UnityEngine.UI;

    public class AnimPart_ImageFill : AnimPart
    {
        [SerializeField] AnimPartData_Float m_data = new AnimPartData_Float(0, 1, 0.25f);
        [SerializeField] Image m_target;
        private void OnValidate() {
            if (m_target == null) {
                m_target = GetComponent<Image>();
            }
        }
        public override float Duration => m_data.Duration;

        protected override void SetAnimationState(float process) {
            m_target.fillAmount = m_data.GetValue(process);
        }
    }
}