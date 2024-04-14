using UnityEngine;

namespace BW.GameCode.Core
{
    /// <summary>
    /// 单例抽象类
    /// 需要挂载在GameObject上才能使用
    /// </summary>
    public abstract class SimpleSingleton<T> : MonoBehaviour where T : SimpleSingleton<T> {

        public static T I { get; private set; }

        public static bool IsActived => I != null;

        protected virtual void OnAwake() {
        }

        protected void Awake() {
            if (I != null)
                Destroy(gameObject); // 保持场景中只有一个_instance
            else {
                I = this as T;
            }
            OnAwake();
        }

        protected void OnDestroy() {
            if (I == this) {
                I = null;
            }
        }

    }

}


