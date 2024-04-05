//using UnityEngine;

//namespace BigDBG
//{
//    [RequireComponent(typeof(AudioSource))]
//    public class UIPanelSoundSFX : MonoBehaviour
//    {
//        [SerializeField] BaseUI m_uiBase = default;

//        //[SerializeField] AudioEvent m_activeSound = default;
//        //[SerializeField] AudioEvent m_closeSound = default;
//        //[SerializeField] AudioSource m_audioSource = default;


//        private void OnEnable() {
//            if (m_uiBase != null) {
//                m_uiBase.Event_OnShow += PlayShowSound;
//                m_uiBase.Event_OnClose += PlayCloseSound;
//            }
//        }

//        private void OnDisable() {
//            if (m_uiBase != null) {
//                m_uiBase.Event_OnShow -= PlayShowSound;
//                m_uiBase.Event_OnClose += PlayCloseSound;
//            }
//        }

//        public void PlayShowSound() {
//            if (m_activeSound != null) {
//                m_activeSound.Play(m_audioSource);
//            }
//        }

//        public void PlayCloseSound() {
//            if (m_closeSound != null) {
//                m_closeSound.Play(m_audioSource);
//            }
//        }

//        private void OnValidate() {
//            m_uiBase = GetComponent<BaseUI>();
//            m_audioSource = GetComponent<AudioSource>();
//        }
//    }
//}