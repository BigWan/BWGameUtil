namespace BW.GameCode.Animation
{

    using UnityEngine;

    [RequireComponent(typeof(CanvasGroup))]
    public class AnimPart_UIFade : AnimPart
    {
        [SerializeField] AnimPartData_Float m_data;
        [SerializeField] CanvasGroup m_cg;
        public override void Init() {
            base.Init();
            if (m_cg == null) {
                m_cg = GetComponent<CanvasGroup>();
            }
            Debug.Assert(m_cg != null);
        }

        public override float Duration => m_data != null ? m_data.Duration : 0f;

        protected override void SetAnimationState(float process) {
            m_cg.alpha = Mathf.Lerp(m_data.StartValue, m_data.EndValue, process);
        }
    }
}