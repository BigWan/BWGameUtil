using UnityEngine;

namespace BW.Core {
    /// <summary>
    /// 单例抽象类
    /// 需要挂载在GameObject上才能使用
    /// </summary>
    public abstract class Singleton<T> : MonoBehaviour where T : Singleton<T> {

        public static T instance { get; protected set; }

        /// <summary> 单例是否存在 </summary>
        public static bool exists {
            get { return instance != null; }
        }


        protected virtual void Awake() {
            if (exists) Destroy(gameObject);
            instance = this as T;
        }


        protected virtual void OnDestroy() {
            if (instance == this) instance = null;
        }

    }

}


