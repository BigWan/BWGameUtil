using DG.Tweening;

using UnityEngine;
using UnityEngine.UI;

using static BW.GameCode.UI.SelectableAnimationController;

namespace BW.GameCode.UI
{
    public class ST_ImageFade : SelectableTransition
    {
        [SerializeField] Graphic m_toggleImage = default;
        [SerializeField] STValue_Bool m_value;
        private void OnDestroy() {
            if (m_toggleImage != null) {

                m_toggleImage.DOKill();
            }
        }

        internal override void DoStateTransition(SelectableState state, bool instant) {
            switch (state) {
                case SelectableState.Normal:
                case SelectableState.Highlighted:
                case SelectableState.Pressed:
                case SelectableState.Selected:
                    TweenAlpha(1, instant); break;
                case SelectableState.Disabled:
                default:
                    TweenAlpha(0, instant); break;
            }
        }

        void TweenAlpha(float value, bool instant) {
            //Debug.Assert(m_toggleImage != null);
            if (m_toggleImage != null) {
                m_toggleImage.DOKill();
                m_toggleImage.DOFade(value, 0.15f);
                //Debug.Log($"Tween Fade {value}");
            } else {
                Debug.Log("Tween ALpha Image is null");
            }
        }
    }
}