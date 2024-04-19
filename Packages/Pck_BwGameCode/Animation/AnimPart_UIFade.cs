﻿namespace BW.GameCode.Animation
{
    using UnityEngine;

    [RequireComponent(typeof(CanvasGroup))]
    public class AnimPart_UIFade : AnimPart
    {
        [SerializeField] AnimPartData_Float m_data;
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