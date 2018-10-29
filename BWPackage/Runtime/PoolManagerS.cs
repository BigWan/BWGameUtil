using UnityEngine;
using System.Collections.Generic;

namespace BW.Core {

    /// <summary>
    /// s结尾表示单例
    /// /// </summary>
    public class PoolManagerS : Singleton<PoolManagerS> {

        [SerializeField] private List<Poolable> poolables;

        protected readonly Dictionary<Poolable,Pool> m_allPools = new Dictionary<Poolable, Pool>();


        protected override void Awake() {
            base.Awake();
            BuildPool();
        }

        public void BuildPool() {
            for (int i = 0; i < poolables.Count; i++) {
                GetPool(poolables[i]);
            }
        }


        public Pool GetPool(Poolable poolable) {
            if (!m_allPools.ContainsKey(poolable)) {
                GameObject go = new GameObject($"{poolable.name} Pool");
                go.transform.SetParent(transform);
                Pool pool = go.AddComponent<Pool>();

                pool.prefab = poolable;

                pool.Build();

                m_allPools.Add(poolable, pool);
            }
            return m_allPools[poolable];
        }

        public Poolable GetPoolable(Poolable prefab) {
            return GetPool(prefab).Get(); 

        }

        public void Pool(Poolable ins) {
            
        }

    }

}
