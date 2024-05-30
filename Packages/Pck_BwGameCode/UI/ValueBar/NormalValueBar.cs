
using BW.GameCode.Animation;
using BW.GameCode.Foundation;

using UnityEngine;
using UnityEngine.Events;

namespace BW.GameCode.UI
{
    [DisallowMultipleComponent]
    public class NormalValueBar : MonoBehaviour
    {
        [System.Serializable]
        public class NormalValueBarEvent : UnityEvent<float>
        { }

        [SerializeField] [Range(0, 1)] float m_value;
        [SerializeField] NormalValueBarEvent m_onChanged;
        [SerializeField] AnimPart m_anim;
        SimpleTween_Float tween = new SimpleTween_Float();

        public NormalValueBarEvent OnChanged => m_onChanged;

        public float Value {
            get {
                return Mathf.Clamp01(m_value);
            }
            set {
                m_value = Mathf.Clamp01(value);
                OnBarChanged(m_value);
            }
        }

        protected void Awake() {
            if (m_anim != null) {
                m_anim.Init();
            }
            tween.SetUpdateCall(x => m_anim.Process = x);
        }

        [ContextMenu("set full")]
        void Set1() {
            Value = 1;
        }

        void OnBarChanged(float value) {
            m_onChanged.Invoke(m_value);
            if (m_anim != null) {
                tween.SetStartAndEnd(m_anim.Process, value)
                    .SetDuration(m_anim.Duration)
                    .StartTween(this);
            }
        }
    }
}