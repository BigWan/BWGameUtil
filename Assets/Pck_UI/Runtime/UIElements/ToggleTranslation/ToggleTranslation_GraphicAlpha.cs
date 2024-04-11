//using DG.Tweening;

//using UnityEngine;
//using UnityEngine.UI;

//namespace BW.GameCode.UI
//{
//    public sealed class ToggleTranslation_GraphicAlpha : ToggleTranslation
//    {
//        [SerializeField] Graphic m_targetGraphic;
//        [SerializeField] float m_onAlpha = 1;
//        [SerializeField] float m_offAlpha = 0;
//        [SerializeField] float m_duration = 0.2f;

//        public override void OnToggleChanged(bool isOn) {
//            m_targetGraphic.CrossFadeAlpha(isOn ? m_onAlpha : m_offAlpha, m_duration, false);
//        }
//    }
//}