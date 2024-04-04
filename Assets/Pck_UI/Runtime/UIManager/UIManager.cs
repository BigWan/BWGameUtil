
using Cysharp.Threading.Tasks;

using System;
using System.Collections.Generic;
using System.IO;

using UnityEngine;

using Object = UnityEngine.Object;

namespace BW.GameCode.UI
{



    /// <summary>
    /// 管理场景中的常驻UI
    /// 改进UI系统
    /// https://github.com/feifeid47/Unity-Async-UIFrame/blob/main/Runtime/Core/UIFrame.cs
    /// 每个场景一个UIManager,作为单例会自动切换
    /// </summary>
    public static class UIManager
    {
        static HashSet<BaseUI> instance = new HashSet<BaseUI>(); // 所有打开UI的实例
        static Stack<Type> panelStack = new Stack<Type>();              // 页面UI的堆栈记录
        static Dictionary<Type, BaseUI> cacheUIs = new Dictionary<Type, BaseUI>(); // 缓存的UI 对象

        static BaseUI mCurrentPanel;

        static string GameUICanvasObject = "[GameCanvas]";

        static string UIResourcesFolder = "UIPanel";

        static GameUICanvas UICanvas {
            get {
                if (uiCanvas == null) {
                    uiCanvas = LoadUICanvas();
                }
                return uiCanvas;
            }
        } // UI画布对象

        static GameUICanvas uiCanvas;

        /// <summary>
        /// 初始化UI系统
        /// </summary>
        /// <returns></returns>
        static GameUICanvas LoadUICanvas() {
            var res = Resources.Load<GameUICanvas>("[GameCanvas]");
            if (res == null) {
                throw new UIException("Game Canvas Game Object is Not Exist");
            }
            var c = Object.Instantiate(res);
            c.hideFlags = HideFlags.DontSave;
            return c;
        }

        /// <summary>
        /// 获取UI实例,如果有缓存则取出缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        static async UniTask<T> LoadUI<T>() where T : BaseUI {
            var uiType = typeof(T);
            if (cacheUIs.TryGetValue(uiType, out var ui)) {
                cacheUIs.Remove(uiType);
                return ui as T;
            } else {
                var path = Path.Combine(UIResourcesFolder, uiType.Name);
                var t = await Resources.LoadAsync<T>(path) as T;
                if (t == null) {
                    Debug.Log($"UI bucunzai  {path}");
                    return default;
                }
                Object.Instantiate(t, UICanvas.GetLayer(t.UILayer));
                return t;
            }
        }

        static T GetOpenedUI<T>() where T : BaseUI {
            foreach (var ui in instance) {
                if (ui is T) {
                    return ui as T;
                }
            }
            return default;
        }

        // 获取UI实例
        static async UniTask<T> RequestUI<T>() where T : BaseUI {
            T ui = GetOpenedUI<T>();
            if (ui != null) {
                return ui;
            }

            ui = await LoadUI<T>();
            if (ui == null) {
                throw new UIException($"UI 不存在 {typeof(T).Name}");
            } else {
                instance.Add(ui);
            }

            return ui;
        }

        static async UniTask Show<T>(T ui) where T : BaseUI {
            // 处理层级等细节
            if (ui.UILayer == GameUILayer.PanelPage && mCurrentPanel != null) {
                // Page需要处理
                Debug.Log("[UI]Push Current Panel");
                panelStack.Push(mCurrentPanel.GetType()); // 将当前页面塞入历史
                await Close(mCurrentPanel);//.Close();
                mCurrentPanel = null;
            }

            await ui.Show();
        }

        public static async void Show<T>() where T : BaseUI {
            var ui = await RequestUI<T>();
            await Show(ui);
        }

        static async UniTask Close(BaseUI ui) {
            //实例存在对象,直接显示对象
            Debug.Assert(ui != null);
            instance.Remove(ui);
            await ui.Close();
            if (ui.AutoDestroy) {
                Object.Destroy(ui.gameObject);  // 删除UI
            } else {
                cacheUIs.Add(ui.GetType(), ui);// 缓存UI
            }
        }

        public static async void Close<T>() where T : BaseUI {
            var ui = GetOpenedUI<T>();
            if (ui != null) {
                await Close(ui);
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

        //void Log(string log) {
        //    if (mDisplayLog) {
        //        Debug.Log(log, this.transform);
        //    }
        //}

#if UNITY_EDITOR

        //[ContextMenu("获取UI引用")]
        //void FindUI() {
        //    m_allUIs = FindObjectsOfType<BaseUI>().ToList();
        //}

#endif
    }
}