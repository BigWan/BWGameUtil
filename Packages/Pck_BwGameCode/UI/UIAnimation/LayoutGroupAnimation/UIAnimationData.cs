
using UnityEngine;

namespace BW.GameCode.UI
{
    public abstract class UIAnimationData<T>
    {
        public T StartValue;
        public T EndValue;
        public float Duration = 1f;
    }
    [System.Serializable]
    public class UIAnimationData_Vector3 : UIAnimationData<Vector3>
    {
    }

    [System.Serializable]
    public class UIAnimationData_Vector2 : UIAnimationData<Vector2>
    {
    }
    [System.Serializable]
    public class UIAnimationData_Float : UIAnimationData<float>
    {
    }
}