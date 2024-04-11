namespace BW.GameCode.UI
{
    using DG.Tweening;

    using UnityEngine;

    using static BW.GameCode.UI.SelectableAnimationController;

    /// <summary>
    /// 缩放
    /// </summary>
    public class ST_Scale : SelectableTransition
    {
        [SerializeField] Transform m_scalePart = default;
        [SerializeField] STValue_Float m_value;
        //[SerializeField] float m_selectScale = 1.1f;

        [SerializeField] float m_animTime = 0.25f;
        internal override void DoStateTransition(SelectableState state, bool instant) {
           if(m_scalePart!=null && m_value != null) {
                DOScale(m_value.GetValue(state), instant);
            }
        }

        private void DOScale(float target, bool instant) {
            //m_scalePart.DOKill();
            if (instant) {
                m_scalePart.localScale = target * Vector3.one;
            } else {
                m_scalePart.DOScale(target, m_animTime);
            }
        }



        private void OnValidate() {
            if (m_scalePart == null) {
                m_scalePart = this.transform;
            }
        }
    }
}