namespace BW.GameCode.Animation
{
    using UnityEngine;

    /// <summary>
    /// 移动动画
    /// </summary>
    public class AnimPart_MoveXYZ : AnimPart
    {
        [SerializeField] bool m_useWorldPosition;
        [SerializeField] AnimPartData_Float m_data;
        [SerializeField] MoveAxisType m_moveType;

        public enum MoveAxisType
        {
            X,
            Y,
            Z
        }

        public override float Duration => m_data != null ? m_data.Duration : 0f;



        protected override void SetAnimationState(float process) {
            var value = m_data.GetValue(process);
            if (m_useWorldPosition) {
                UpdateWorld(value);
            } else {
                UpdateLocal(value);
            }
        }

        void UpdateLocal(float value) {
            var target = transform.localPosition;
            switch (m_moveType) {
                case MoveAxisType.X:
                    target.x = value;
                    break;

                case MoveAxisType.Y:
                    target.y = value;
                    break;

                case MoveAxisType.Z:
                    target.z = value;
                    break;

                default:
                    break;
            }
            transform.localPosition = target;
        }

        void UpdateWorld(float value) {
            var target = transform.position;
            switch (m_moveType) {
                case MoveAxisType.X:
                    target.x = value;
                    break;

                case MoveAxisType.Y:
                    target.y = value;
                    break;

                case MoveAxisType.Z:
                    target.z = value;
                    break;

                default:
                    break;
            }
            transform.position = target;
        }
    }
}