namespace BW.GameCode.UI
{
    using System;

    using UnityEngine;

    /// <summary>
    ///
    /// </summary>
    [System.Serializable]
    public struct AnimtionTrack
    {
        /// <summary>
        /// 动画片段
        /// </summary>
        public UIAnimation Clip;
        /// <summary>
        /// 动画的起始时间
        /// </summary>
        public float StartTime;

        [NonSerialized] public float NormalStartPoint;
        [NonSerialized] public float NormalLength;

        public float SafeDuration {
            get {
                if (Clip != null) return Clip.Duration;
                return 0;
            }
        }

        public float GetProcessInWholeAnimation(float wholeProcess) {
            return Mathf.Clamp01((wholeProcess - NormalStartPoint) / NormalLength);
        }
    }

    /// <summary>
    /// 动画组
    /// </summary>
    public class UIAnimation_Sequence : UIAnimation
    {
        [SerializeField] AnimtionTrack[] m_tracks;

        public override float Duration {
            get {
                float result = 0;
                for (int i = 0; i < m_tracks.Length; i++) {
                    if (m_tracks[i].Clip != null) {
                        result += m_tracks[i].Clip.Duration;
                    }
                }
                return result;
            }
        }

        public override void Init() {
            base.Init();

            if (m_tracks != null) {
                foreach (var item in m_tracks) {
                    if (item.Clip != null) {
                        item.Clip.Init();
                    }
                }
            }
            float duration = Duration;
            if (duration > 0) {
                for (int i = 0; i < m_tracks.Length; i++) {
                    m_tracks[i].NormalStartPoint = m_tracks[i].StartTime / duration;
                    m_tracks[i].NormalLength = m_tracks[i].SafeDuration / duration;
                }
            }
        }

        protected override void SetAnimationState(float process) {
            for (int i = 0; i < m_tracks.Length; i++) {
                if (m_tracks[i].Clip != null) {
                    float pc = m_tracks[i].GetProcessInWholeAnimation(process);
                    m_tracks[i].Clip.UpdateState(pc);
                }
            }
        }
    }
}