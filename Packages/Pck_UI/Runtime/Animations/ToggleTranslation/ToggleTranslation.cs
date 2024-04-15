using UnityEngine;
using UnityEngine.UI;
namespace BW.GameCode.UI
{


    public abstract class ToggleTranslation : MonoBehaviour
    {
        [SerializeField] Toggle m_target;
    

        protected virtual void Awake() {
            if (m_target == null) {
                m_target = GetComponent<Toggle>();
            }
            if (m_target != null) {
                m_target.onValueChanged.AddListener(DOTranslation);
            }
        }



        private void OnEnable() {
            if (m_target != null) {
                DOTranslation(m_target.isOn);
            }
        }

        protected abstract void DOTranslation(bool isOn);

      

        private void OnValidate() {
            if (m_target == null) {
                m_target = GetComponent<Toggle>();
            }
            
        }
    }
}