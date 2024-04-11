//using UnityEngine;

//namespace BW.GameCode.UI
//{
//    public abstract class ToggleTranslation : MonoBehaviour
//    {
//        [SerializeField] BWToggle m_target;
//        protected virtual void Awake() {
//            if (m_target == null) {
//                m_target = GetComponent<BWToggle>();
//            }
//            if (m_target != null) {
//                m_target.Event_OnValueChanged.AddListener(OnToggleChanged);
//            }
//        }

//        protected void Start() {
//            OnToggleChanged(m_target.IsOn);
//        }

//        public abstract void OnToggleChanged(bool isOn);

//        private void OnValidate() {
//            if (m_target == null) {
//                m_target = GetComponent<BWToggle>();
//            }
//        }
//    }
//}