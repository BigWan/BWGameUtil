using UnityEngine.EventSystems;

#if UNITY_EDITOR

#endif

namespace BW.GameCode.UI
{
    [UnityEngine.DisallowMultipleComponent]
    public sealed class BWButtonEventDrive_UIKind : BWButtonEventDrive, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
    {
        public void OnPointerClick(PointerEventData eventData) {
            RaisClickEvent();
        }

        public void OnPointerEnter(PointerEventData eventData) {
            RaisePointInSideEvent(true);
            //IsPointInside = true;
        }

        public void OnPointerExit(PointerEventData eventData) {
            RaisePointInSideEvent(false);
            //IsPointInside = false;
        }

        public void OnPointerDown(PointerEventData eventData) {
            RaisePointDownEvent(true);
            //IsPointDown = true;
        }

        public void OnPointerUp(PointerEventData eventData) {
            RaisePointDownEvent(false);
            //IsPointDown = false;
        }
    }

    //#if UNITY_EDITOR

    //    [CustomEditor(typeof(UIToggle))]
    //    public class UIToggleInspector : Editor
    //    {
    //        public override void OnInspectorGUI() {
    //            base.OnInspectorGUI();
    //            GUILayout.Label((target as UIToggle).myState.ToString());
    //        }
    //    }

    //#endif
}