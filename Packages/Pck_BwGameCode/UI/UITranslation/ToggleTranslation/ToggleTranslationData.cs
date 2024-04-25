using UnityEngine;
namespace BW.GameCode.UI
{
    public abstract class ToggleTranslationData<T>
    {
        public T OffValue;
        public T OnValue;

        protected ToggleTranslationData(T offValue, T onValue) {
            OffValue = offValue;
            OnValue = onValue;
        }

        public T GetValue(bool isOn) {
            return isOn ? OnValue : OffValue;
        }
    }
    [System.Serializable]
    public class ToggleData_Sprite : ToggleTranslationData<Sprite>
    {
        public ToggleData_Sprite(Sprite offValue, Sprite onValue) : base(offValue, onValue) {
        }
    }
    [System.Serializable]
    public class ToggleTranslationData_Float : ToggleTranslationData<float>
    {
        public ToggleTranslationData_Float(float offValue, float onValue) : base(offValue, onValue) {
        }
    }

    [System.Serializable]
    public class ToggleTranslationData_Vector2 : ToggleTranslationData<Vector2>
    {
        public ToggleTranslationData_Vector2(Vector2 offValue, Vector2 onValue) : base(offValue, onValue) {
        }
    }

    [System.Serializable]
    public class ToggleTranslationData_Vector3 : ToggleTranslationData<Vector3>
    {
        public ToggleTranslationData_Vector3(Vector3 offValue, Vector3 onValue) : base(offValue, onValue) {
        }
    }

    [System.Serializable]
    public class ToggleTranslationData_Bool : ToggleTranslationData<bool>
    {
        public ToggleTranslationData_Bool(bool offValue, bool onValue) : base(offValue, onValue) {
        }
    }
    public class ToggleTranslationData_Color : ToggleTranslationData<Color>
    {
        public ToggleTranslationData_Color(Color offValue, Color onValue) : base(offValue, onValue) {
        }
    }
}