using UnityEngine;

namespace BW.GameCode.Animation
{
    public abstract class AnimPartData<T>
    {
        public T StartValue;
        public T EndValue;
        public float Duration = 1f;

        protected AnimPartData(T startValue, T endValue, float duration=0.25f) {
            StartValue = startValue;
            EndValue = endValue;
            Duration = duration;
        }

        protected abstract T Lerp(T start, T end, float process);

        public T GetValue(float process) {
            return Lerp(StartValue, EndValue, process);
        }
    }

    [System.Serializable]
    public class AnimPartData_Vector3 : AnimPartData<Vector3>
    {
        public AnimPartData_Vector3(Vector3 startValue, Vector3 endValue, float duration) : base(startValue, endValue, duration) {
        }

        protected override Vector3 Lerp(Vector3 start, Vector3 end, float process) {
            return Vector3.Lerp(start, end, process);
        }
    }

    [System.Serializable]
    public class AnimPartData_Color : AnimPartData<Color>
    {
        public AnimPartData_Color(Color startValue, Color endValue, float duration) : base(startValue, endValue, duration) {
        }

        protected override Color Lerp(Color start, Color end, float process) {
            return Color.Lerp(start, end, process);
        }
    }

    [System.Serializable]
    public class AnimPartData_Vector2 : AnimPartData<Vector2>
    {
        public AnimPartData_Vector2(Vector2 startValue, Vector2 endValue, float duration) : base(startValue, endValue, duration) {
        }

        protected override Vector2 Lerp(Vector2 start, Vector2 end, float process) {
            return Vector2.Lerp(start, end, process);
        }
    }

    [System.Serializable]
    public class AnimPartData_Float : AnimPartData<float>
    {
        public AnimPartData_Float(float startValue, float endValue, float duration) : base(startValue, endValue, duration) {
        }

        protected override float Lerp(float start, float end, float process) {
            return Mathf.Lerp(start, end, process);
        }
    }
}