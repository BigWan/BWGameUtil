using UnityEngine;
using System.Collections;

namespace BW.Core { 

    /// <summary>
    /// 可以缓存的对象
    /// </summary>
    public class Poolable :MonoBehaviour {
        public int poolInitCapacity = 10;

        [HideInInspector]public Pool pool;

        public void TryDestroy() {
            if (pool != null)
                pool.RecycleToPool(this);
            else
                Destroy(gameObject);           
            
        }

    }

}
