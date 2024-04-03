using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace BW.Core {


    /// <summary>
    /// 计时器的组件,内部可以包含若干个计时器
    /// </summary>
    public class TimerComponent : MonoBehaviour {
        
        readonly List<Timer> m_activeTimers = new List<Timer>();

        /// <summary>
        /// 开始一个计时器
        /// </summary>
        /// <param name="timer">Timer对象</param>
        protected void StartTImer(Timer timer) {
            if (m_activeTimers.Contains(timer)) {
                Debug.LogWarning("这个计时器已经存在");
            } else {
                m_activeTimers.Add(timer);
            }
        }

        /// <summary>
        /// 停止一个计时器
        /// </summary>
        /// <param name="timer"></param>
        protected void PauseTimer(Timer timer) {
            if (m_activeTimers.Contains(timer)) {
                m_activeTimers.Remove(timer);
            }
        }

        /// <summary>
        /// 停止一个计时器
        /// </summary>
        /// <param name="timer"></param>
        protected void StopTimer(Timer timer) {
            timer.Reset();
            PauseTimer(timer);
        }


        // 为所有计时器计时
        protected virtual void Update() {
            for (int i = m_activeTimers.Count-1; i>=0; i--) {
                if (m_activeTimers[i].Tick(Time.deltaTime)) {
                    StopTimer(m_activeTimers[i]);
                }
            }
        }


    }
}