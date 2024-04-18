using UnityEngine;

namespace BW.GameCode.UI
{
    public abstract class AnimPartData<T>
    {
        public T StartValue;
        public T EndValue;
        public float Duration = 1f;
    }

    [System.Serializable]
    public class AnimPartData_Vector3 : AnimPartData<Vector3>
    {
    }
    [System.Serializable]
    public class AnimPartData_Color : AnimPartData<Color>
    {
    }
    [System.Serializable]
    public class AnimPartData_Vector2 : AnimPartData<Vector2>
    {
    }

    [System.Serializable]
    public class AnimPartData_Float : AnimPartData<float>
    {
    }
}