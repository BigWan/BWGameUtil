
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using UnityEngine;
namespace BW.GameCode.UI
{
    /// <summary>
    /// 管理场景中的常驻UI
    /// 改进UI系统
    ///
    /// 所有在操作还是放在UI对象上面，UIManager通过回调来管理
    /// https://github.com/feifeid47/Unity-Async-UIFrame/blob/main/Runtime/Core/UIFrame.cs
    /// 每个场景一个UIManager,作为单例会自动切换
    /// </summary>
    public class UIManager : MonoBehaviour
    {
        [SerializeField] UICanvasLayerManager m_canvas;
        [SerializeField] string m_uiResPath = "BaseUI";
        // 打开的UI
        Dictionary<Type, BaseUIPage> minstances = new Dictionary<Type, BaseUIPage>();
        // panel
        Stack<Type> panelStack = new Stack<Type>();
        // 当前活动的页面
        BaseUIPage activedPanel;
        // cached
        Dictionary<Type, BaseUIPage> cachedUI = new Dictionary<Type, BaseUIPage>();

        //string GetUIPath(Type uiType) => Path.Combine(m_uiResPath, uiType.Name);
        string GetUIPath(Type uiType) => m_uiResPath + "/" + uiType.Name;

        public string GetStackLayer() {
            string result = panelStack.Count.ToString();
            foreach (var item in panelStack) {
                result += item.Name + "\n";
            }
            return result;
        }

        public void CloseAll() {
            panelStack.Clear();
            var actived = minstances.Values.ToArray();
            if (actived != null) {
                for (int i = 0; i < actived.Length; i++) {
                    StartCoroutine(JustCloseUI(actived[i]));
                }
            }
        }

        /// <summary>
        /// 加载一个UI资源
        /// </summary>
        T InstantiateUI<T>() where T : BaseUIPage {
            return InstantiateUI(typeof(T)) as T;
        }

        BaseUIPage InstantiateUI(Type uiType) {
            var uiPath = GetUIPath(uiType);
            var uiObj = Resources.Load<GameObject>(uiPath);
            if (uiObj == null) {
                throw new FileNotFoundException(uiPath);
            }
            var uiRes = uiObj.GetComponent<BaseUIPage>();
            if (uiRes == null) {
                throw new ArgumentNullException(uiPath);
            }
            var ui = Instantiate(uiRes, m_canvas.GetUILayer(uiRes.UILayer));
            if (ui == null) {
                throw new NullReferenceException($"实例化UI为空{uiPath}/{uiType}");
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
        T GetUIInstance<T>() where T : BaseUIPage {
            var uiType = typeof(T);

            var ui = GetUIInstance(uiType);
            return ui as T;
        }

        BaseUIPage GetUIInstance(Type uiType) {
            if (cachedUI.TryGetValue(uiType, out BaseUIPage result)) {
                cachedUI[uiType] = null; // 从缓存中取出实例
                if (result == null) {
                    cachedUI.Remove(uiType);
                }
            }
            if (result == null) {
                result = InstantiateUI(uiType);
            }
            return result;
        }


        public T Show<T>() where T : BaseUIPage {
            // already showed
            var uiType = typeof(T);
            if (minstances.ContainsKey(uiType)) {
                var instance = minstances[uiType] as T;
                if(instance.IsShow == false) {
                    StartCoroutine(ShowProcess(instance));
                }
            }
            // not instance
            var ui = GetUIInstance<T>();
            ui.transform.SetAsLastSibling();
            StartCoroutine(ShowProcess(ui));
            return ui;
        }

        private IEnumerator ShowProcess(BaseUIPage ui) {
            Debug.Assert(ui != null);
            Debug.Log($"UIManager.ShowProcess({ui.name})");
            // 处理panel逻辑
            if (activedPanel != null) {
                panelStack.Push(activedPanel.GetType());
                yield return JustCloseUI(activedPanel);
            }
            yield return JustActiveUI(ui);
        }

        IEnumerator JustActiveUI(BaseUIPage ui) {
            Debug.Assert(ui != null);
            Debug.Log("----------------JustActiveUI");
            if (m_canvas.IsPanelLayer(ui.UILayer)) {
                activedPanel = ui;
                Debug.Log($"UIManager.SetActivePanel({activedPanel.name})");
            }

            minstances.Add(ui.GetType(), ui);
            yield return ui.Show();
        }

        /// <summary>
        /// 关闭UI,从打开的实例中移除
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void TryClose<T>() where T : BaseUIPage {
            var uiType = typeof(T);
            Close(uiType);
        }

        public void Close(Type uiType) {
            if (minstances.TryGetValue(uiType, out var ui)) {
                StartCoroutine(ProcessClose(ui));
            } else {
                Debug.LogWarning($"No such UI {uiType} Opened");
            }
        }

        IEnumerator ProcessClose(BaseUIPage ui) {
            Debug.Assert(ui != null);
            yield return JustCloseUI(ui);   // 关旧
            yield return PopStackPanel();   // 开新
        }

        IEnumerator JustCloseUI(BaseUIPage ui) {
            Debug.Log("----------------JustCloseUI");
            Debug.Assert(ui != null);
            var type = ui.GetType();
            if (minstances.ContainsKey(type)) {
                minstances.Remove(type);
            }
            if (activedPanel == ui) {
                activedPanel = null;
            }
            yield return ui.Close();
            if (ui.AutoDestroyOnHide) {
                Destroy(ui.gameObject);
            } else {
                cachedUI[ui.GetType()] = ui;
            }
        }

        IEnumerator PopStackPanel() {
            Debug.Log("----------------PopPanelStack");
            if (panelStack.Count <= 0) {
                yield break;
            }
            var uiType = panelStack.Pop();
            var ui = GetUIInstance(uiType);
            yield return JustActiveUI(ui);
        }
    }
}