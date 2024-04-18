namespace BW.GameCode.UI
{
    using UnityEngine;

    /// <summary>
    /// 移动动画
    /// </summary>
    public class AnimPart_Move : AnimPart
    {
        [SerializeField] bool m_useWorldPosition;
        [SerializeField] AnimPartData_Vector3 m_data;

        public override float Duration => m_data != null ? m_data.Duration : 0f;

        public override void Init() {
            base.Init();
            UpdatePosition(m_data.StartValue);
        }

        protected override void SetAnimationState(float progress) {
            UpdatePosition(Vector3.Lerp(m_data.StartValue, m_data.EndValue, progress));
        }

        void UpdatePosition(Vector3 pos) {
            if (m_useWorldPosition) {
                transform.position = pos;
            } else {
                transform.localPosition = pos;
            }
        }
    }
}