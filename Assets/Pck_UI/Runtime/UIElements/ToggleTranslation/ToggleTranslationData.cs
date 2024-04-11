﻿using UnityEngine;
namespace BW.GameCode.UI
{
    public abstract class ToggleTranslationData<T>
    {
        public T OnValue;
        public T OffValue;

        public T GetValue(bool isOn) {
            return isOn ? OnValue : OffValue;
        }
    }

    [System.Serializable]
    public class ToggleTranslationData_Float : ToggleTranslationData<float> { }

    [System.Serializable]
    public class ToggleTranslationData_V2 : ToggleTranslationData<Vector2> { }

    [System.Serializable]
    public class ToggleTranslationData_V3 : ToggleTranslationData<Vector3> { }

    [System.Serializable]
    public class ToggleTranslationData_Bool : ToggleTranslationData<bool> { }
    public class ToggleTranslationData_Color : ToggleTranslationData<Color> { }
}