using System;
using System.Collections.Generic;
using System.IO;

using UnityEngine;
using BW.GameCode.Singleton;
namespace BW.GameCode.UI
{
    public enum UIType
    {
        SceneUI,       // 场景UI,背景UI
        Panel,       // 大的页面,Page控制弹窗,关闭Page的时候,这个Page的所有弹窗都会消失
        Popup,      // 整体的弹窗,弹窗会遮盖底层UI的交互
        Top         // 顶层UI
    }

    public interface IUIManager
    {
        void OnUIActive(BaseUI ui);

        void OnUIDeactive(BaseUI ui);
    }

    public interface IUIData
    { }

    public struct UIStackItem
    {
        public Type UIType { get; set; }    // 存储的是UI的类型,而不是实例
        public IUIData Data { get; set; }
    }

    /// <summary>
    /// 管理场景中的常驻UI
    /// 改进UI系统
    ///
    /// 所有在操作还是放在UI对象上面，UIManager通过回调来管理
    /// https://github.com/feifeid47/Unity-Async-UIFrame/blob/main/Runtime/Core/UIFrame.cs
    /// 每个场景一个UIManager,作为单例会自动切换
    /// </summary>
    public class UIManager : SimpleSingleton<UIManager>
    {
        [SerializeField] GameUICanvas m_canvas;
        [SerializeField] string m_uiResPath = "BaseUI";
        // 打开的UI
        Dictionary<Type, BaseUI> minstances = new Dictionary<Type, BaseUI>();
        // panel
        Stack<Type> panelStack = new Stack<Type>();
        // 当前活动的页面
        BaseUI activedPanel;
        // cached
        Dictionary<Type, BaseUI> cachedUI = new Dictionary<Type, BaseUI>();


        //string GetUIPath(Type uiType) => Path.Combine(m_uiResPath, uiType.Name);
        string GetUIPath(Type uiType) => m_uiResPath + "/" + uiType.Name;


        public string GetStackLayer() {
            string result = panelStack.Count.ToString();
            foreach (var item in panelStack) {
                result += item.Name + "\n";
            }
            return result;
            
        }

        /// <summary>
        /// 加载一个UI资源
        /// </summary>
        T InstantiateUI<T>() where T : BaseUI {
            return InstantiateUI(typeof(T)) as T;
        }

        BaseUI InstantiateUI(Type uiType) {
            var uiPath = GetUIPath(uiType);
            var uiObj = Resources.Load<GameObject>(uiPath) ;
            if (uiObj == null) {
                throw new FileNotFoundException(uiPath);
            }
            var uiRes = uiObj.GetComponent<BaseUI>();
            if(uiRes == null) {
                throw new ArgumentNullException(uiPath);
            }
            var ui = Instantiate(uiRes, m_canvas.GetLayer(uiRes.UIType));
            if (ui == null) {
                throw new NullReferenceException($"实例化UI为空{uiPath}/{uiType}");
            }
            ui.Event_OnDeactive += () => OnUIDeactived(ui);
            ui.Event_OnClose += () => OnUIClose(ui);
            ui.Event_OnActive += () => OnUIActived(ui);

            return ui;
        }

        BaseUI GetUIInstance(Type uiType) {
            if (cachedUI.TryGetValue(uiType, out BaseUI result)) {
                cachedUI[uiType] = null; // 从缓存中取出实例
                if (result == null) {
                    cachedUI.Remove(uiType);
                }
            }
            if (result != null) {
                return result;
            }
            // 啥都不在,重新实例化一个新的UI
            return InstantiateUI(uiType);
        }

        /// <summary>
        /// 获取UI实例 按以下逻辑执行
        /// 1.已经打开的UI会直接返回实例,
        /// 2.缓存的UI则返回缓存的,
        /// 3.未加载的UI执行加载
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetUIInstance<T>() where T : BaseUI => GetUIInstance(typeof(T)) as T;

        bool AlreadyShowd(Type uiType) {
            return minstances.ContainsKey(uiType);
        }

        bool AlreadyShowed<T>() where T : BaseUI => minstances.ContainsKey(typeof(T));

        public bool Show<T>() where T : BaseUI {
            return Show(typeof(T));
        }

        public bool Show(Type uiType) {
            if (AlreadyShowd(uiType)) {
                return false;
            }
            var ui = GetUIInstance(uiType);

            ui.Show();
            return true;
        }

        void OnUIActived(BaseUI ui) {
            minstances.Add(ui.GetType(), ui);
            ui.transform.SetAsLastSibling();
            // 如果是主页面
            if (ui.UIType == UIType.Panel) {
                var temp = activedPanel;
                activedPanel = ui;
                if (temp != null) {
                    panelStack.Push(temp.GetType());
                    temp.Close();
                }
            }
        }

        public void TryClose(BaseUI ui) {
            Debug.Assert(ui != null);
            ui.Close();
        }

        /// <summary>
        /// 关闭UI,从打开的实例中移除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void TryClose<T>() where T : BaseUI {
            var uiType = typeof(T);
            if (minstances.TryGetValue(uiType, out var ui)) {
                ui.Close();
            } else {
                Debug.LogWarning($"No such UI {uiType} Opened");
            }
        }

        private void OnUIClose(BaseUI ui) {


        }

        void OnUIDeactived(BaseUI ui) {
            // 移除实例引用
            Debug.Log("UIManager On UIDeactive");
            var type = ui.GetType();
            if (minstances.ContainsKey(type)) {
                Debug.Log("移除打开的实例引用");
                minstances.Remove(type);
            }

            if (ui == activedPanel || activedPanel == null) {
                activedPanel = null;
                if (panelStack.Count > 0) {
                    var stackUIType = panelStack.Pop();
                    Show(stackUIType);
                }
            }

            if (ui.AutoDestroyOnHide) {
                Debug.Log($"删除UI{ui.name}");
                Destroy(ui.gameObject);
            } else {
                Debug.Log($"缓存UI{ui.name}");
                cachedUI[type] = ui;
            }
        }



#if UNITY_EDITOR

#endif
    }
}