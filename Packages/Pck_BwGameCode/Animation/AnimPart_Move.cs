namespace BW.GameCode.Animation
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



        protected override void SetAnimationState(float process) {
            UpdatePosition(m_data.GetValue(process));
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