using UnityEngine;

namespace BW.GameCode.Singleton
{
    /// <summary>
    /// 单例抽象类
    /// 需要挂载在GameObject上才能使用
    /// </summary>
    public abstract class SimpleSingleton<T> : MonoBehaviour where T : SimpleSingleton<T> {

        public static T instance { get; protected set; }

        /// <summary> 单例是否存在 </summary>
        public static bool exists {
            get { return instance != null; }
        }

        protected virtual void OnAwake() { }
        protected void Awake() {
            if (exists) Destroy(gameObject);
            instance = this as T;
        }


        protected virtual void OnDestroy() {
            if (instance == this) instance = null;
        }

    }

}


