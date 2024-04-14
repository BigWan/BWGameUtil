namespace BW.GameCode.UI
{

    using UnityEngine;

    using static BW.GameCode.UI.SelectableAnimationController;
    using BW.GameCode.Singleton;

    /// <summary>
    /// 缩放
    /// </summary>
    public class ST_Scale : SelectableTransition
    {
        [SerializeField] Transform m_scalePart = default;
        [SerializeField] STValue_Float m_value;
        //[SerializeField] float m_selectScale = 1.1f;

        [SerializeField] float m_animTime = 0.25f;

        SimpleTween<float> m_tweenRunner = new SimpleTween<float>(Mathf.Lerp);
        
        internal override void DoStateTransition(SelectableState state, bool instant) {
           if(m_scalePart!=null && m_value != null) {
                m_tweenRunner.StartValue = m_scalePart.transform.localScale.x;
                m_tweenRunner.EndValue = m_value.GetValue(state);
                m_tweenRunner.Duration = m_animTime;
                m_tweenRunner.Host = this;
                DOScale(m_value.GetValue(state), instant);
            }
        }

        private void DOScale(float target, bool instant) {
            //m_scalePart.DOKill();
            if (instant) {
                m_scalePart.localScale = target * Vector3.one;
            } else {
                m_tweenRunner.StartTween();
            }
        }



        private void OnValidate() {
            if (m_scalePart == null) {
                m_scalePart = this.transform;
            }
        }
    }
}