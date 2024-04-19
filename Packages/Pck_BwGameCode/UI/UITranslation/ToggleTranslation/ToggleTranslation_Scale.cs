﻿using BW.GameCode.Foundation;

using UnityEngine;

namespace BW.GameCode.UI
{
    public sealed class ToggleTranslation_Scale : ToggleTranslation
    {
        [SerializeField] Transform m_scalePart;
        [SerializeField] ToggleTranslationData_Float m_value;
        [SerializeField] float m_animTime = 0.25f;

        SimpleTween<float> m_tween = new SimpleTween<float>();

        protected override void Awake() {
            base.Awake();
            m_tween.SetCallback(x => { if (m_scalePart != null) { m_scalePart.localScale = Vector3.one * x; } })
                .SetDuration(m_animTime);
        }

        protected override void DOTranslation(bool isOn) {
            m_tween.SetStartAndEnd(m_scalePart.localScale.x, m_value.GetValue(isOn))
                .StartTween(this);
        }
    }
}