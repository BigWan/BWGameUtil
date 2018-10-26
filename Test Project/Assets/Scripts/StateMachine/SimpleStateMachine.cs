using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BW.Core {
    /// <summary>
    /// 简易状态机
    /// </summary>
    /// <typeparam name="TId">状态机索引key类型</typeparam>
    public class SimpleStateMachine<TId> {


        SimpleState<TId> m_currentState;
        Dictionary<TId, SimpleState<TId>> m_states = new Dictionary<TId, SimpleState<TId>>();

        public void Add(SimpleState<TId> newState) {
            m_states.Add(newState.id, newState);
        }

        public TId CurrentState() {
            return m_currentState.id;
        }


        public void Update() {
            m_currentState.Update();
        }

        public void Close() {
            if (m_currentState != null && m_currentState.Leave != null)
                m_currentState.Leave();

            m_currentState = null;
        }


        public void SwitchTo(TId stateID) {
            var newState = m_states[stateID];

            if (m_currentState != null && m_currentState.Leave != null) {
                m_currentState.Leave();
            }
            newState?.Enter?.Invoke();
            m_currentState = newState;
        }





    }



}