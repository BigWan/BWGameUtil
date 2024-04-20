namespace BW.GameCode.Animation
{
    using UnityEngine;

    [RequireComponent(typeof(CanvasGroup))]
    public class AnimPart_CanvasFade : AnimPart
    {
        [SerializeField] AnimPartData_Float m_data = new AnimPartData_Float(0,1,0.4f);
        [SerializeField] CanvasGroup m_cg;

        private void OnValidate() {
            if (m_cg == null) {
                m_cg = GetComponent<CanvasGroup>();
            }
        }

        public override void Init() {
            Debug.Assert(m_cg != null);
            base.Init();
        }

        public override float Duration => m_data != null ? m_data.Duration : 0f;

        protected override void SetAnimationState(float process) {
            m_cg.alpha = m_data.GetValue(process);
        }
    }
}