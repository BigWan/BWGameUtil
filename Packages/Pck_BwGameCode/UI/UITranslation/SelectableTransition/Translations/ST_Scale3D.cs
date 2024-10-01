namespace BW.GameCode.UI
{
    using BW.GameCode.Foundation;

    using UnityEngine;

    using static BW.GameCode.UI.SelectableAnimationController;

    /// <summary>
    /// 缩放
    /// </summary>
    public class ST_Scale3D : SelectableTransition
    {
        [SerializeField] Transform m_scalePart = default;
        [SerializeField] STValue_V3 m_value;

        [SerializeField] float m_animTime = 0.25f;

        SimpleTween_V3 runner = new SimpleTween_V3();

        private void Awake() {
            runner.SetUpdateCall((x) => {
                if (m_scalePart != null) {
                    m_scalePart.localScale = x;
                }
            });
        }
        internal override void DoStateTransition(SelectableState state, bool instant) {
            if (m_scalePart != null && m_value != null) {
                DOScale(m_value.GetValue(state), instant);
            }
        }

        private void DOScale(Vector3 value, bool instant) {
            if (instant) {
                m_scalePart.localScale = value;
            } else {
                runner.SetStartAndEnd(m_scalePart.localScale, value)
                       .SetDuration(m_animTime)
                       .StartTween(this);
            }
        }

        private void OnDestroy() {
            
        }

        private void OnValidate() {
            if (m_scalePart == null) {
                m_scalePart = this.transform;
            }
        }
    }
}