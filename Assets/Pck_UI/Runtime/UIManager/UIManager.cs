using System;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

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

    public class GameUICanvas : MonoBehaviour
    {
        [SerializeField] Transform m_sceneLayer;
        [SerializeField] Transform m_panelLayer;
        [SerializeField] Transform m_popupLayer;
        [SerializeField] Transform m_topLayer;

        public Transform GetLayer(UIType uiType) {
        }
    }

    /// <summary>
    /// 管理场景中的常驻UI
    /// 改进UI系统
    /// https://github.com/feifeid47/Unity-Async-UIFrame/blob/main/Runtime/Core/UIFrame.cs
    /// 每个场景一个UIManager,作为单例会自动切换
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        // 打开的UI
        Dictionary<Type, BaseUI> minstances = new Dictionary<Type, BaseUI>();
        // panel
        Stack<BaseUI> panelStack = new Stack<BaseUI>();
        // 当前活动的页面
        BaseUI activedPanel;
        // cached
        Dictionary<Type, BaseUI> cachedUI = new Dictionary<Type, BaseUI>();

        static GameUICanvas GameCanvas;

        public static UIManager I { get; private set; }

        public static string UI_RES_PATH = "UI/WINDOWS";

        string GetUIPath(Type uiType) => Path.Combine(UI_RES_PATH, uiType.Name);

        /// <summary>
        /// 加载一个UI资源
        /// </summary>
        T InstantiateUI<T>() where T : BaseUI {
            var uiPath = GetUIPath(typeof(T));
            var res = Resources.Load(uiPath) as T;
            if (res == null) {
                throw new FileNotFoundException(uiPath);
            }

            var ui = Instantiate<T>(res, GameCanvas.GetLayer(res.UIType));

            if (ui == null) {
                throw new NullReferenceException($"实例化UI为空{uiPath}/{typeof(T)}");
            }

            return ui;
        }

        /// <summary>
        /// 获取UI实例 按以下逻辑执行
        /// 1.已经打开的UI会直接返回实例,
        /// 2.缓存的UI则返回缓存的,
        /// 3.未加载的UI执行加载
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        T GetUIInstance<T>() where T : BaseUI {
            var uiType = typeof(T);
            // 在缓存中
            if (cachedUI.TryGetValue(uiType, out var ui)) {
                cachedUI[uiType] = null; // 从缓存中取出实例
                return ui as T;
            }
            // 啥都不在,重新实例化一个新的UI
            return InstantiateUI<T>();
        }

        bool AlreadyShowed<T>() where T : BaseUI {
            return minstances.ContainsKey(typeof(T));
        }

        public void Show<T>(Action<T> initCall = default) where T : BaseUI {
            if (AlreadyShowed<T>()) {
                return;
            }
            var ui = GetUIInstance<T>();
            minstances.Add(typeof(T), ui);
            initCall?.Invoke(ui);
            // 处理Panel堆栈
            if (ui.UIType == UIType.Panel) {
                if (activedPanel != null) {
                    panelStack.Push(activedPanel);
                    Close(activedPanel);
                }
            }
            activedPanel = ui;
            ui.Show();
        }

        void Close(BaseUI ui) {
            Debug.Assert(ui != null);
            minstances[ui.GetType()] = null;
            ui.Close();
        }

        /// <summary>
        /// 关闭UI,从打开的实例中移除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void Close<T>() where T : BaseUI {
            var uiType = typeof(T);
            if (minstances.TryGetValue(uiType, out var ui)) {
                minstances[uiType] = null;
                ui.Close();
            } else {
                Debug.LogWarning($"No such UI {uiType} Opened");
            }
        }

        void OnDestroy() {
            if (I == this) {
                I = null;
            }
        }

#if UNITY_EDITOR

#endif
    }
}