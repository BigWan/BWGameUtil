using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;

namespace BW.Core {

    /// <summary>
    /// 缓存池,同时作位缓存对象的父物体
    /// </summary>
    public class Pool : MonoBehaviour {

        public Poolable prefab;

        protected readonly Stack<Poolable> m_avilables = new Stack<Poolable>();

        protected readonly List<Poolable> m_all = new List<Poolable>();


        public virtual void Build() {
            Grow(prefab.poolInitCapacity);
        }
        protected virtual Poolable AddNewElement() {
            Poolable newItem = Instantiate(prefab);
            newItem.transform.SetParent(transform);
            newItem.gameObject.SetActive(false);
            newItem.pool = this;
            m_all.Add(newItem);
            m_avilables.Push(newItem);
            return newItem;
        }


        /// <summary>
        /// 扩展池
        /// </summary>
        /// <param name="count"></param>
        public void Grow(int count) {
            for (int i = 0; i < count; i++) {
                AddNewElement();
            }
        }

               
        /// <summary>
        /// 取一个缓存的对象
        /// </summary>
        /// <param name="action">提供一个函数,取完拿函数操作下这个对象</param>
        /// <returns></returns>
        public virtual Poolable Get(Action<Poolable> action = null) {
            if (m_avilables.Count == 0)
                AddNewElement();
            if (m_avilables.Count == 0)
                throw new UnityException("池内元素为0,无法扩张池");

            Poolable item = m_avilables.Pop();
            action?.Invoke(item);
            return item;            
        }

        /// <summary>
        /// 回收到池内
        /// </summary>
        /// <param name="item"></param>
        public virtual void RecycleToPool(Poolable item,Action<Poolable> action  = null) {
            if(m_all.Contains(item) && !m_avilables.Contains(item)) {
                m_avilables.Push(item);
                item.transform.SetParent(transform);
                item.gameObject.SetActive(false);
                action?.Invoke(item);
            } else {
                Debug.LogWarning("不能回收物体,该物体已经被回收或者没有关联池");
            }
           
        }

        public virtual void ReturnAll(Action<Poolable> action) {
            for (int i = 0; i < m_avilables.Count; i++) {
                RecycleToPool(m_avilables.Pop(),action);
            }
        }



    }

}
