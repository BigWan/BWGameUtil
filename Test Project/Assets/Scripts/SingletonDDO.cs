namespace BW.Core {

    /// <summary>
    /// DontDestroyOnLoad 跨场景的单例
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingletonDDO<T> : Singleton<T> where T : Singleton<T> {
        protected override void Awake() {
            base.Awake();
            DontDestroyOnLoad(gameObject);
        }
    }
}