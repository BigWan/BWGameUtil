using UnityEngine;
using UnityEngine.UI;
namespace BW.GameCode.UI
{

    [RequireComponent(typeof(Toggle))]
    public abstract class ToggleTranslation : MonoBehaviour
    {
        [SerializeField] Toggle m_target;
    

        protected virtual void Awake() {
            if (m_target == null) {
                m_target = GetComponent<Toggle>();
            }
            if (m_target != null) {
                m_target.onValueChanged.AddListener(OnValueChanged);
            }
        }



        private void OnEnable() {
            if (m_target != null) {
                OnValueChanged(m_target.isOn);
            }
        }

        //void OnValueChanged2(bool isOn) {
        //    Debug.Log($"Toggle 's value is {isOn}");
        //    OnValueChanged(isOn);
        //}
        protected abstract void OnValueChanged(bool isOn);

      

        protected virtual void OnValidate() {
            if (m_target == null) {
                m_target = GetComponent<Toggle>();
            }
            
        }
    }
}