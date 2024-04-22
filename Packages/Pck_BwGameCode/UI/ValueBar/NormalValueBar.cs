
using BW.GameCode.Animation;
using BW.GameCode.Foundation;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace BW.GameCode.UI
{
    [DisallowMultipleComponent]
    public class NormalValueBar : UIBehaviour
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

        protected override void Awake() {
            m_anim.Init();
            tween.SetCallback(x => m_anim.Process = x);
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