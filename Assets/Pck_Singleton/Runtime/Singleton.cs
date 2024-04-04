using UnityEngine;

namespace BW.GameCode.Singleton
{
    /// <summary>
    /// 单例抽象类
    /// 需要挂载在GameObject上才能使用
    /// </summary>
    public abstract class SimpleSingleton<T> : MonoBehaviour where T : SimpleSingleton<T> {

        public static T I { get; protected set; }

        /// <summary> 单例是否存在 </summary>
        public static bool Exist {
            get { return I != null; }
        }

        protected virtual void OnAwake() { }
        protected void Awake() {
            if (Exist) 
                Destroy(gameObject);
            else
                I = this as T;
        }


        protected virtual void OnDestroy() {
            if (I == this) I = null;
        }

    }

}


