using UnityEngine;
using System;
using System.Timers;

namespace BW.Core {

    /// <summary>
    /// 计时器类
    /// </summary>
    public class Timer {

        // 到点触发的回调
        private readonly Action m_callBack;

        // 当前时间和最大时间
        private float m_time, m_currentTime;

        /// <summary>
        /// 标准进度
        /// </summary>
        public float normal {
            get { return Mathf.Clamp01(m_currentTime / m_time); }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="interval">时间间隔毫秒计算</param>
        /// <param name="onElapsed">到点的行为</param>
        public Timer(float time,Action onElapsed = null) {
            m_time = Mathf.Max(time,0.05f);
            m_currentTime = 0;
            m_callBack += onElapsed;
        }


        public virtual bool Tick(float deltaTime) {
            return AssessTime(deltaTime);
        }

        protected bool AssessTime(float deltaTime) {
            m_currentTime += deltaTime;
            if (m_currentTime >= m_time) {
                FireEvent();
                return true;
            }
            return false;
        }


        public void Reset() {
            m_time = 0;
        }

        public void FireEvent() {
            m_callBack.Invoke();
        }


    }

    /// <summary>
    /// 循环的计时器
    /// </summary>
    public class RepeatingTimer:Timer {

        public RepeatingTimer(float time,Action onElapsed = null) :base(time,onElapsed) {}

        public override bool Tick(float deltaTime) {
            if (AssessTime(deltaTime)) {
                Reset();
            }
            return false;
        }

    }

}
