namespace BW.GameCode.UI
{
    using UnityEngine;

    /// <summary>
    /// 
    /// </summary>
    public struct AnimationClipData
    {
        /// <summary>
        /// 单独的动画
        /// </summary>
        public UIAnimation Clip; 
        /// <summary>
        /// 动画的起始时间
        /// </summary>
        public float startTime;
    }

    /// <summary>
    /// 并行执行的动画组
    /// </summary>
    public class UIAnimation_Parallel : UIAnimation
    {
        [SerializeField] AnimationClipData[] m_anims;

        public override float Duration => throw new System.NotImplementedException();

        internal override void SetAnimationState(float process) {
            
        }
    }

    /// <summary>
    /// 串联执行的动画组
    /// </summary>
    public class UIAnimation_Sequence : UIAnimation
    {
        [SerializeField] UIAnimation[] m_anims;

        public override float Duration {
            get {
                float result = 0;
                for (int i = 0; i < m_anims.Length; i++) {
                    if (m_anims[i] != null) {
                        result += m_anims[i].Duration;
                    }
                }
                return result;
            }
        }

        float[] normalTimes; // 时间轴(0-1)

        public override void Init() {
            base.Init();
            normalTimes = new float[m_anims.Length];
            float timeSnap = 0;
            float duration = Duration;
            if (duration > 0) {
                for (int i = 0; i < m_anims.Length; i++) {
                    if (m_anims[i] != null) {
                        timeSnap += m_anims[i].Duration;
                        normalTimes[i] = timeSnap / Duration;
                    } else {
                        normalTimes[i] = timeSnap / Duration;
                    }
                }
            }
        }


        protected override void SetAnimationState(float process) {
            int activedIndex = 0;
            for (int i = 0; i < normalTimes.Length; i++) {
                if (process <= normalTimes[i]) {
                    activedIndex = i;
                    break;
                }
            }
            for (int i = 0; i < m_anims.Length; i++) {
                m_anims[i].SetAnimationState(process);
            }

            // 有个当前动画,
            // 在当前动画之前,所有都已完成
            // 在当前动画之后,所有都未开始
            // TODO20240417
        }
    }
}