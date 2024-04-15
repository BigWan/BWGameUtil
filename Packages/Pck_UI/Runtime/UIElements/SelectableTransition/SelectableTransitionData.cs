using UnityEngine;

namespace BW.GameCode.UI
{
    public abstract class SelectableTransitionData<T>
    {
        public T NormalValue;
        public T DisableValue;
        public T PressedValue;
        public T SelectedValue;
        public T HighlightedValue;

        public T GetValue(SelectableAnimationController.SelectableState state) {
            switch (state) {
                case SelectableAnimationController.SelectableState.Disabled: return DisableValue;
                case SelectableAnimationController.SelectableState.Normal: return NormalValue;
                case SelectableAnimationController.SelectableState.Pressed: return PressedValue;
                case SelectableAnimationController.SelectableState.Selected: return SelectedValue;
                case SelectableAnimationController.SelectableState.Highlighted: return HighlightedValue;
                default:
                    return NormalValue;
            }
        }
    }

    [System.Serializable]
    public class STValue_Float : SelectableTransitionData<float>
    {
        public STValue_Float() {
        }
    }

    [System.Serializable]
    public class STValue_Color : SelectableTransitionData<Color> { }

    [System.Serializable]
    public class STValue_Bool : SelectableTransitionData<bool> { }

    [System.Serializable]
    public class STValue_V3 : SelectableTransitionData<Vector3> { }
    [System.Serializable]
    public class STValue_V2 : SelectableTransitionData<Vector2> { }
}