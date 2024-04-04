using BW.GameCode.Singleton;

using Cysharp.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using UnityEngine;
namespace BW.GameCode.UI
{
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
    /// https://github.com/feifeid47/Unity-Async-UIFrame/blob/main/Runtime/Core/UIFrame.cs
    /// 每个场景一个UIManager,作为单例会自动切换
    /// </summary>
    public class UIManager : SimpleSingleton<UIManager>
    {
        Dictionary<Type, BaseUI> instance = new Dictionary<Type, BaseUI>(); // 所有打开UI的实例
        Stack<UIStackItem> uiStack = new Stack<UIStackItem>();              // UI的堆栈记录
        HashSet<BaseUI> activeUIs = new HashSet<BaseUI>(); // TODO:独立UI和群组UI需要隔离开,有些UI打开时还需要调整UI层级顺序

        Dictionary<Type, BaseUI> cacheUIs = new Dictionary<Type, BaseUI>(); // 缓存的UI 对象

        static string UIResourcesFolder = "UIPanel";

        public async UniTask<T> LoadUI<T>() where T : BaseUI {
            var obj = await Resources.LoadAsync(Path.Combine(UIResourcesFolder, typeof(T).Name));
            if (obj == null) {
                return default;
            }
            return (obj as GameObject).GetComponent<T>();
        }

        public async void Show<T>() where T : BaseUI {
            if (instance.TryGetValue(typeof(T), out var ui)) {
                //实例存在对象,直接显示对象
                await ui.Show();
            } else {
                ui = await LoadUI<T>();
                instance.Add(typeof(T), ui);
                await ui.Show();
            }
        }

        public async void Close<T>() where T : BaseUI {
            if (instance.TryGetValue(typeof(T), out var ui)) {
                //实例存在对象,直接显示对象
                instance.Remove(typeof(T));
                await ui.Close();
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
            m_allUIs = FindObjectsOfType<BaseUI>().ToList();
        }

#endif
    }
}