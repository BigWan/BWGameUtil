using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using UnityEngine;

namespace BW.GameCode.UI
{

    public enum UIPanelType
    {
        Back,       // 场景UI,背景UI
        Page,       // 大的页面,Page控制弹窗,关闭Page的时候,这个Page的所有弹窗都会消失
        Popup,      // 整体的弹窗,弹窗会遮盖底层UI的交互
        Top         // 顶层UI
    }
    public interface IUIManager
    {
        void OnUIActive(BaseUI ui);

        void OnUIDeactive(BaseUI ui);
    }

    public interface IUIData { }

    public struct UIStackItem
    {
        public Type UIType { get; set; }    // 存储的是UI的类型,而不是实例
        public IUIData Data { get; set; }
    }

    public class GameUICanvas : MonoBehaviour
    {
        [SerializeField] Transform m_topLayer;
        [SerializeField] Transform m_backLayer;
        [SerializeField] Transform m_panelLayer;
        [SerializeField] Transform m_popupLayer;
    }

    /// <summary>
    /// 管理场景中的常驻UI
    /// 改进UI系统
    /// https://github.com/feifeid47/Unity-Async-UIFrame/blob/main/Runtime/Core/UIFrame.cs
    /// 每个场景一个UIManager,作为单例会自动切换
    /// </summary>
    public class UIManager : MonoBehaviour, IUIManager
    {
        // 打开的UI
        Dictionary<Type, BaseUI> minstances = new Dictionary<Type, BaseUI>();

        // panel
        Stack<UIStackItem> panelStack = new Stack<UIStackItem>();
        BaseUI currentPanel;
        // cached
        Dictionary<Type, BaseUI> cachedUI = new Dictionary<Type, BaseUI>();

        static GameUICanvas GameCanvas;

        public static UIManager I { get; private set; }

        public static string UI_RES_PATH = "UI/WINDOWS";



        /// <summary>
        /// 加载一个UI资源
        /// </summary>
         T InstantiateUI<T>() where T : BaseUI {
            var uiPath = GetUIPath(typeof(T));
            var res = Resources.Load(uiPath) as T;
            if (res == null) {
                throw new FileNotFoundException(uiPath);
            }

            var ins = Instantiate<T>(res, GameCanvas.transform);
            if (ins == null) {
                throw new NullReferenceException($"实例化UI为空{uiPath}/{typeof(T)}");
            }
            return ins;
        }

        /// <summary>
        /// 获取UI实例,已经打开的UI会直接返回实例,缓存的UI则返回缓存的,未加载的UI执行加载
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetUIInstance<T>() where T : BaseUI {
            var uiType = typeof(T);
            // 实例已经打开
            if (minstances.TryGetValue(uiType, out var ui)) {
                return ui as T;
            }
            // 在缓存中
            if (cachedUI.TryGetValue(uiType, out ui)) {
                cachedUI[uiType] = null; // 从缓存中取出实例
                return ui as T;
            }
            // 啥都不在,重新实例化一个新的UI
            return InstantiateUI<T>();
        }

        public void ShowUI<T>() where T : BaseUI { 
            var ui = GetUIInstance<T>();
            if (ui.isPanel) {

            }
            ui.Show();
        }


        string GetUIPath(Type uiType) {
            return Path.Combine(UI_RES_PATH, uiType.Name);
        }

        void OnDestroy() {
            if (I == this) {
                I = null;
            }
        }

        public T GetUI<T>() where T : BaseUI {
            for (int i = 0; i < m_allUIs.Count; i++) {
                if (m_allUIs[i] is T) {
                    return m_allUIs[i] as T;
                }
            }
            return default;
        }

        public static T Show<T>(Action<T> initAction = default) where T : BaseUI {
            return I.GetAndShow<T>(initAction);
        }

        public static void Close<T>() where T : BaseUI {
            foreach (var ui in I.activeUIs) {
                if (ui is T) {
                    ui.Close();
                    return;
                }
            }

            Debug.LogWarning($"{typeof(T) }没有打开,无法关闭.");

            //var ui = I.GetUI<T>();
            //if (ui != null) {
            //    ui.Close();
            //}
        }

        public T GetAndShow<T>(Action<T> initAction = default) where T : BaseUI {
            var ui = GetUI<T>();

            if (ui != null) {
                if (ui != currentUI) {
                    ui.Show();
                }

                initAction?.Invoke(ui);
                return ui;
            } else {
                Debug.LogError($"Show UI Error,{typeof(T) } 没有注册!");
                return null;
            }
        }

        // TODO:A显示,通知B 关,B关通知A显示,会出现死循环
        void IUIManager.OnUIActive(BaseUI ui) {
            activeUIs.Add(ui);
            // 如果是独立UI 无所谓
            if (ui.IsMainUI) {
                return;
            }

            if (currentUI != null) {
                currentUI.Close();
                currentUI = null;
            }
            currentUI = ui;
        }

        void IUIManager.OnUIDeactive(BaseUI ui) {
            Log($"IUIManager.OnUIDeactive {ui.name} ");

            activeUIs.Remove(ui);
            // 当此刻导航UI关闭时,显示默认导航

            if (ui == currentUI) {
                currentUI = null;
                if (ui != m_rootUI) {
                    m_rootUI.Show();
                }
            }
            Log($"IUIManager.OnUIDeactive {ui.name} ");
        }

        void Log(string log) {
            if (mDisplayLog) {
                Debug.Log(log, this.transform);
            }
        }

#if UNITY_EDITOR

        [ContextMenu("获取UI引用")]
        void FindUI() {
            m_allUIs = FindObjectsOfType<BaseUI>().ToList();
        }

#endif
    }
}