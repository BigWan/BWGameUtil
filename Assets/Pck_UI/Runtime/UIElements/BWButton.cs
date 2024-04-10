using System;

using UnityEngine;

using UnityEngine.Events;
using UnityEngine.UI;
namespace BigDBG
{

    [DisallowMultipleComponent]
    public class BWButton : Button
    {
        
        [SerializeField] BWButtonEventDrive m_driver;

        [Serializable]
        public class BoolEvent : UnityEvent<bool>
        { }

        public enum ButtonState
        {
            Disable,
            Pressed,
            Hover,
            Normal
        }

        [Header("动画组件(可为空)")]
        [SerializeField] AbstractButtonTranslation[] m_buttonAnims = default;
        [SerializeField] bool m_interactable = true;

        [SerializeField] UnityEvent m_onClicked = new UnityEvent();
        [SerializeField] BoolEvent m_onHoverChanged = new BoolEvent();

        public UnityEvent Event_OnClick => m_onClicked;
        public BoolEvent Event_OnHover => m_onHoverChanged;

        bool mIsPointInside;
        bool mIsPointDown;



        protected override void Awake() {
            base.Awake();
            if (m_driver != null) {
                m_driver.Event_OnClick += Click;
                m_driver.Event_PointDown += SetPointDown;// () (x) => mIsPointDown = x;
                m_driver.Event_PointInSide += SetPointInside;// (x) => SetPointInside = x;
            } else {
                Debug.LogError("按钮没有ButtonDriver组件",this.transform);
            }
        }

        protected override void DoStateTransition(SelectionState state, bool instant) {
            base.DoStateTransition(state, instant);
            TranslateAnim(state);
        }
        protected void TranslateAnim(SelectionState state2) {
            var state = GetCurrentState();
            //lastState = state;
            if (m_buttonAnims != null) {
                //Debug.Log($"TranslateAnim {this.name}-{state}");
                foreach (var item in m_buttonAnims) {
                    item.OnStateChanged(state);
                }
            }
        }
        void SetPointInside(bool value) {
            if (mIsPointInside != value) {
                mIsPointInside = value;
                TranslateAnim();
                Event_OnHover?.Invoke(value);
            }
        }

        void SetPointDown(bool value) {
            if (mIsPointDown != value) {
                mIsPointDown = value;
                TranslateAnim();
            }
        }

        ButtonState GetCurrentState() {
            if (!Interactable) return ButtonState.Disable;
            if (mIsPointDown) return ButtonState.Pressed;
            if (mIsPointInside) return ButtonState.Hover;
            return ButtonState.Normal;
        }


        public bool Interactable {
            get => m_interactable && isActiveAndEnabled;
            set {
                m_interactable = value;
                TranslateAnim();
            }
        }

        protected virtual void OnValidate() {
            if (m_buttonAnims == null || m_buttonAnims.Length == 0) {
                m_buttonAnims = GetComponents<AbstractButtonTranslation>();
            }
            if (m_driver == null) {
                m_driver = GetComponent<BWButtonEventDrive>();
            }

        }

        protected virtual void Start() {
            TranslateAnim();
        }

        protected virtual void OnClick() {
        }

        protected void TranslateAnim() {
            var state = GetCurrentState();
            //lastState = state;
            if (m_buttonAnims != null) {
                //Debug.Log($"TranslateAnim {this.name}-{state}");
                foreach (var item in m_buttonAnims) {
                    item.OnStateChanged(state);
                }
            }
        }

        void Click() {
            if (!Interactable) return;
            Debug.Log($"Button Click {name}");
            OnClick();
            Event_OnClick.Invoke();
        }
    }
}