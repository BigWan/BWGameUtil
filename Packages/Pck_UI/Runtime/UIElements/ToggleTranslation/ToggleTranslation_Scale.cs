//using DG.Tweening;

//using UnityEngine;

//namespace BW.GameCode.UI
//{
//    public sealed class ToggleTranslation_Scale : ToggleTranslation
//    {
//        [SerializeField] Transform m_scalePart;
//        [SerializeField] float m_scaleOn = 1f;
//        [SerializeField] float m_scaleOff = 0f;
//        [SerializeField] float m_animTime = 0.25f;

//        public override void OnToggleChanged(bool isOn) {
//            DOScale(isOn ? m_scaleOn : m_scaleOff);
//        }

//        private void DOScale(float target) {
//            //m_scalePart.DOKill();
//            m_scalePart.DOScale(target, m_animTime);
//        }
    
//    }
//}