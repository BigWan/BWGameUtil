namespace BW.GameCode.Animation
{
    using System;

    using UnityEngine;

    /// <summary>
    ///
    /// </summary>
    [Serializable]
    public struct AnimtionTrackData
    {
        /// <summary>
        /// 动画片段
        /// </summary>
        public AnimPart Clip;
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
    }

    /// <summary>
    /// 多轨道动画
    /// </summary>
    public class AnimPart_Group : AnimPart
    {
        [SerializeField] AnimtionTrackData[] m_tracks;

        public override float Duration {
            get {
                float result = 0;
                for (int i = 0; i < m_tracks.Length; i++) {
                    if (m_tracks[i].Clip != null) {
                        result = Mathf.Max(m_tracks[i].SafeDuration + m_tracks[i].StartTime, result);
                    }
                }
                return result;
            }
        }

        public override void Init() {
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
            base.Init();
        }

        protected override void SetAnimationState(float process) {
            for (int i = 0; i < m_tracks.Length; i++) {
                if (m_tracks[i].Clip != null) {
                    float value = GetProcessInWholeAnimation(m_tracks[i], process);
                    m_tracks[i].Clip.Process = value;
                }
            }
        }

        float GetProcessInWholeAnimation(AnimtionTrackData data, float wholeProcess) {
            return Mathf.Clamp01((wholeProcess - data.NormalStartPoint) / data.NormalLength);
        }
    }
}