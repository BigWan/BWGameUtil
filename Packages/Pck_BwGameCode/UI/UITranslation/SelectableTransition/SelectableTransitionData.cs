using UnityEngine;

namespace BW.GameCode.UI
{
    public abstract class SelectableTransitionData<T>
    {
        public T NormalValue;
        public T HighlightedValue;
        public T PressedValue;
        public T SelectedValue;
        public T DisableValue;

        public void Reset(T def) {
            NormalValue = def;
            HighlightedValue = def;
            PressedValue = def;
            SelectedValue = def;
            DisableValue = def;
        }

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
        public static STValue_Float DEFAULT_FADE_VALUE = new STValue_Float() {
            DisableValue = 0,
            HighlightedValue =1,
            NormalValue = 0,
            PressedValue = 1,
            SelectedValue =1
        };

        public static STValue_Float DEFAULT_SCALE_VALUE = new STValue_Float()
        {
            DisableValue = 1,
            HighlightedValue = 1.1F,
            NormalValue = 1F,
            PressedValue = 0.95F,
            SelectedValue = 1F
        };

        public STValue_Float() {
        }
    }

    [System.Serializable]
    public class STValue_Color : SelectableTransitionData<Color>
    { }

    [System.Serializable]
    public class STValue_Bool : SelectableTransitionData<bool>
    { }

    [System.Serializable]
    public class STValue_V3 : SelectableTransitionData<Vector3>
    { }

    [System.Serializable]
    public class STValue_V2 : SelectableTransitionData<Vector2>
    { }
}