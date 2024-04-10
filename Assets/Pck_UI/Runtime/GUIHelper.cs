using TMPro;

using UnityEngine;
using UnityEngine.EventSystems;

public static class GUIHelper
{
    /// <summary>
    /// 判断是否触摸或指针在UI上, return true 表示点击事件发生在UI上
    /// </summary>
    /// <returns></returns>
    public static bool IsPointOnUI() {
        if (EventSystem.current == null)
            return false;
#if ANDROID
            return EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId);
#else
        return EventSystem.current.IsPointerOverGameObject();
#endif
    }

    public static void FocusInput(TMP_InputField input) {
        EventSystem.current.SetSelectedGameObject(input.gameObject, null);
        input.OnPointerClick(new PointerEventData(EventSystem.current));
    }

    /// <summary>
    /// 屏幕坐标转anchored坐标
    /// </summary>
    /// <param name="rect"></param>
    /// <param name="screenPoint"></param>
    /// <returns></returns>
    public static Vector2 CalcAnchoredPoint_OverLay(this RectTransform rect, Vector2 screenPoint) {
        Vector2 vector2;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, screenPoint, null, out vector2);
        return vector2;
    }

    //public static Vector2 AnchoredPosition2ScreenPosition(this RectTransform rect,Vector2 anchoredPosition,Camera cam = null) {
    //    return RectTransformUtility.WorldToScreenPoint(cam ?? Camera.main, rect.position);
    //}
    /// <summary>
    /// uiAnchoredPos转ScreenPos
    /// </summary>
    /// <param name="rect"></param>
    /// <param name="zero"></param>
    /// <param name="camera"></param>
    /// <returns></returns>
    public static Vector2 AnchoredPosition2ScreenPosition(this RectTransform rect, Vector2 zero, Camera camera = null) {
        var cam = camera == null ? Camera.main : camera;
        return Camera.main.WorldToScreenPoint(rect.position);
    }
}