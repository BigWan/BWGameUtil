using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BW.GameCode.UI
{
    [RequireComponent(typeof(Selectable))]
    [DisallowMultipleComponent]
    public class SelectableAnimationController : UIBehaviour,
        IPointerEnterHandler, IPointerExitHandler,
        IPointerDownHandler, IPointerUpHandler,
        ISelectHandler, IDeselectHandler
    {
        public enum SelectableState
        {
            Disabled,
            Normal,
            Pressed,
            Selected,
            Highlighted
        }

        [SerializeField] Selectable m_selectable;

        [SerializeField] SelectableTransition[] m_animations;
#if UNITY_EDITOR
        [SerializeField] SelectableState debugState;

        void Update() {
            debugState = CurrentSelectionState;
        }

#endif
        bool isPointerInside;
        bool isPointerDown;
        bool hasSelection;



        protected override void Start() {
            DoStateTransition(CurrentSelectionState, true);
        }

        protected override void OnEnable() {
            isPointerDown = false;
            DoStateTransition(CurrentSelectionState, true);
        }

        protected override void OnCanvasGroupChanged() {
            DoStateTransition(CurrentSelectionState, false);
        }

        protected override void OnDisable() {
            isPointerInside = false;
            isPointerDown = false;
            hasSelection = false;
        }

        protected override void OnValidate() {
            if (m_selectable == null) {
                m_selectable = GetComponent<Selectable>();
            }
            if (m_selectable != null) {
                m_selectable.transition = Selectable.Transition.None;
            }
            m_animations = GetComponents<SelectableTransition>();
        }

        private void DoStateTransition(SelectableState currentSelectionState, bool instant) {
            //Debug.Log($"{this.name}+DO Translation {currentSelectionState}");
            if (m_animations != null) {
                for (int i = 0; i < m_animations.Length; i++) {
                    if (m_animations[i] != null) {
                        m_animations[i].DoStateTransition(currentSelectionState, instant);
                    }
                }
            }
        }

        protected SelectableState CurrentSelectionState {
            get {
                if (!m_selectable.IsInteractable())
                    return SelectableState.Disabled;
                if (isPointerDown)
                    return SelectableState.Pressed;
                if (isPointerInside)
                    return SelectableState.Highlighted;
                if (hasSelection)
                    return SelectableState.Selected;

                return SelectableState.Normal;
            }
        }

        protected bool IsHighlighted() {
            if (!m_selectable.IsActive() || !m_selectable.IsInteractable())
                return false;
            return isPointerInside && !isPointerDown && !hasSelection;
        }

        protected bool IsPressed() {
            if (!m_selectable.IsActive() || !m_selectable.IsInteractable())
                return false;
            return isPointerDown;
        }

        public void OnDeselect(BaseEventData eventData) {
            hasSelection = false;
            EvaluateAndTransitionToSelectionState();
        }

        public void OnPointerDown(PointerEventData eventData) {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;
            isPointerDown = true;
            EvaluateAndTransitionToSelectionState();
        }

        private void EvaluateAndTransitionToSelectionState() {
            if (!m_selectable.IsActive() || !m_selectable.IsInteractable())
                return;

            DoStateTransition(CurrentSelectionState, false);
        }

        public void OnPointerEnter(PointerEventData eventData) {
            isPointerInside = true;
            EvaluateAndTransitionToSelectionState();
        }

        public void OnPointerExit(PointerEventData eventData) {
            isPointerInside = false;
            EvaluateAndTransitionToSelectionState();
        }

        public void OnPointerUp(PointerEventData eventData) {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            isPointerDown = false;
            EvaluateAndTransitionToSelectionState();
        }

        public void OnSelect(BaseEventData eventData) {
            hasSelection = true;
            EvaluateAndTransitionToSelectionState();
        }
    }
}