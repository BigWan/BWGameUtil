using UnityEngine;

namespace BW.GameCode.Animation
{
    public abstract class AnimPartData<T>
    {
        public T StartValue;
        public T EndValue;
        public float Duration = 1f;

        protected abstract T Lerp(T start, T end, float process);

        public T GetValue(float process) {
            return Lerp(StartValue, EndValue, process);
        }
    }

    [System.Serializable]
    public class AnimPartData_Vector3 : AnimPartData<Vector3>
    {
        protected override Vector3 Lerp(Vector3 start, Vector3 end, float process) {
            return Vector3.Lerp(start, end, process);
        }
    }

    [System.Serializable]
    public class AnimPartData_Color : AnimPartData<Color>
    {
        protected override Color Lerp(Color start, Color end, float process) {
            return Color.Lerp(start, end, process);
        }
    }

    [System.Serializable]
    public class AnimPartData_Vector2 : AnimPartData<Vector2>
    {
        protected override Vector2 Lerp(Vector2 start, Vector2 end, float process) {
            return Vector2.Lerp(start, end, process);
        }
    }

    [System.Serializable]
    public class AnimPartData_Float : AnimPartData<float>
    {
        protected override float Lerp(float start, float end, float process) {
            return Mathf.Lerp(start, end, process);
        }
    }
}