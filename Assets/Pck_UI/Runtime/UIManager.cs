using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace BW.GameCode.UI
{


    public interface IUIData { }

    public struct UIStackItem
    {
        public Type UIType { get; set; }    // 存储的是UI的类型,而不是实例
        public IUIData Data { get; set; }
    }

    /// <summary>
    /// 管理场景中的常驻UI
    /// 改进UI系统
    /// https://github.com/feifeid47/Unity-Async-UIFrame/blob/main/Runtime/Core/UIFrame.cs
    /// 每个场景一个UIManager,作为单例会自动切换
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        [SerializeField] List<APanelUI> m_allUIs = default;
        [Header("RootUI,Root ui会在其他UI 都关闭时默认打开,默认的UI")]
        [SerializeField] APanelUI m_rootUI = default;
        [SerializeField] bool mDisplayLog = false;

        Dictionary<Type, BaseUI> instance = new Dictionary<Type, BaseUI>();
        Stack<UIStackItem> activeUI2s = new Stack<UIStackItem>();
        HashSet<APanelUI> activeUIs = new HashSet<APanelUI>(); // TODO:独立UI和群组UI需要隔离开,有些UI打开时还需要调整UI层级顺序


        


        APanelUI currentUI;
        public static UIManager I { get; private set; }






        void OnDestroy() {
            if (I == this) {
                I = null;
            }
        }

        public T GetUI<T>() where T : APanelUI {
            for (int i = 0; i < m_allUIs.Count; i++) {
                if (m_allUIs[i] is T) {
                    return m_allUIs[i] as T;
                }
            }
            return default;
        }

        public static T Show<T>(Action<T> initAction = default) where T : APanelUI {
            return I.GetAndShow<T>(initAction);
        }

        public static void Close<T>() where T : APanelUI {
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

        public T GetAndShow<T>(Action<T> initAction = default) where T : APanelUI {
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

        //// TODO:A显示,通知B 关,B关通知A显示,会出现死循环
        //void IUIManager.OnUIActive(APanelUI ui) {
        //    activeUIs.Add(ui);
        //    // 如果是独立UI 无所谓
        //    if (ui.IsMainUI) {
        //        return;
        //    }

        //    if (currentUI != null) {
        //        currentUI.Close();
        //        currentUI = null;
        //    }
        //    currentUI = ui;
        //}

        //void IUIManager.OnUIDeactive(APanelUI ui) {
        //    Log($"IUIManager.OnUIDeactive {ui.name} ");

        //    activeUIs.Remove(ui);
        //    // 当此刻导航UI关闭时,显示默认导航

        //    if (ui == currentUI) {
        //        currentUI = null;
        //        if (ui != m_rootUI) {
        //            m_rootUI.Show();
        //        }
        //    }
        //    Log($"IUIManager.OnUIDeactive {ui.name} ");
        //}

        void Log(string log) {
            if (mDisplayLog) {
                Debug.Log(log, this.transform);
            }
        }

#if UNITY_EDITOR

        [ContextMenu("获取UI引用")]
        void FindUI() {
            m_allUIs = FindObjectsOfType<APanelUI>().ToList();
        }

#endif
    }
}