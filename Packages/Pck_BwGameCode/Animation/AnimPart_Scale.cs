namespace BW.GameCode.Animation
{
    using UnityEngine;

    public class AnimPart_Scale : AnimPart
    {
        [SerializeField] AnimPartData_Float m_data = new AnimPartData_Float(1, 1, 0.4f);
        [SerializeField] ScaleType m_scaleType = ScaleType.X | ScaleType.Y | ScaleType.Y;

        [System.Flags]
        protected enum ScaleType
        {
            X,
            Y,
            Z
        }

        public override float Duration => m_data != null ? m_data.Duration : 0f;

        protected override void SetAnimationState(float process) {
            var scale = m_data.GetValue(process) * Vector3.one;
            if (!m_scaleType.HasFlag(ScaleType.X)) {
                scale.x = transform.localScale.x;
            }
            if (!m_scaleType.HasFlag(ScaleType.Y)) {
                scale.y = transform.localScale.y;
            }
            if (!m_scaleType.HasFlag(ScaleType.Z)) {
                scale.z = transform.localScale.z;
            }
            transform.localScale = scale;
        }
    }
}