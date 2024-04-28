namespace BW.GameCode.UI
{
    using BW.GameCode.Foundation;

    using UnityEngine;

    using static BW.GameCode.UI.SelectableAnimationController;

    /// <summary>
    /// Recttransform.SizeDelta
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class ST_SizeDelta : SelectableTransition
    {
        [SerializeField] RectTransform m_expandPart = default;
        [SerializeField] STValue_V2 m_value;
        [SerializeField] float m_animTime = 0.15f;
        SimpleTween_V2 tween = new SimpleTween_V2();

        void InitTween() {
            tween.SetCallback((x) => {
                if (m_expandPart != null) {
                    m_expandPart.sizeDelta = x;
                }
            });
        }

        private void Awake() {
            InitTween();
        }

        internal override void DoStateTransition(SelectableState state, bool instant) {
            if (m_expandPart != null && m_value != null) {
                DOSize(m_value.GetValue(state), instant);
            }
        }

        private void OnDestroy() {
        }

        void DOSize(Vector2 value, bool instant) {
            if (instant) {
                m_expandPart.sizeDelta = value;
            } else {
                tween.SetStartAndEnd(m_expandPart.sizeDelta, value).SetDuration(m_animTime);
                tween.StartTween(this);
            }
        }
    }
}