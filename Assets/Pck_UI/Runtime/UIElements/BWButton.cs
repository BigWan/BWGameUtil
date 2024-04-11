//using System;

//using UnityEngine;

//using UnityEngine.Events;
//using UnityEngine.UI;
//namespace BW.GameCode.UI
//{
//    [DisallowMultipleComponent]
//    public class BWButton : Button
//    {



//        [Header("动画组件(可为空)")]
//        [SerializeField] SelectableTransition[] m_buttonTransitions = default;

//        protected override void DoStateTransition(SelectionState state, bool instant) {
//            base.DoStateTransition(state, instant);
//            if (m_buttonTransitions == null) {
//                return;
//            }
//            var buttonState = ButtonState.Normal;
//            switch (state) {
//                case SelectionState.Normal:
//                    buttonState = ButtonState.Normal;
//                    break;

//                case SelectionState.Highlighted:
//                    buttonState = ButtonState.Highlighted;
//                    break;

//                case SelectionState.Pressed:
//                    buttonState = ButtonState.Pressed;
//                    break;

//                case SelectionState.Selected:
//                    buttonState = ButtonState.Selected;
//                    break;

//                case SelectionState.Disabled:
//                    buttonState = ButtonState.Disable;
//                    break;

//                default:
//                    break;
//            }
//            for (int i = 0; i < m_buttonTransitions.Length; i++) {
//                if (m_buttonTransitions[i] != null) {
//                    m_buttonTransitions[i].DoStateTransition(buttonState, instant);
//                }
//            }
//        }

//        protected override void OnValidate() {
//            base.OnValidate();
//            if (m_buttonTransitions == null || m_buttonTransitions.Length == 0) {
//                m_buttonTransitions = GetComponents<SelectableTransition>();
//            }
//        }
//    }
//}