using System;
using System.Collections;
using System.Linq;

using UnityEngine;
using UnityEngine.EventSystems;
namespace BW.GameCode.UI
{
    /// <summary>
    /// 最基础的UI组件,可以打开,关闭,以及一些回调
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    [DisallowMultipleComponent]
    public abstract class BaseUIPage : UIBehaviour
    {
        [Header("-------------------------------------------------------------")]
        [Header("基础信息")]
        [Space(10)]
        [SerializeField] CanvasGroup m_body = default; // 不要Fade这个CanvasGroup,因为UI会直接设置他的值

        [SerializeField] bool m_autoDestroyOnHide;

        public bool IsShow { get; private set; }
        public bool AutoDestroyOnHide => m_autoDestroyOnHide;
        public abstract string UILayer { get; }

        /// <summary>
        /// UI激活
        /// </summary>
        public event Action Event_OnActive;
        /// <summary>
        /// UI 显示完成
        /// </summary>
        public event Action Event_OnShow;
        /// <summary>
        /// UI开始关闭
        /// </summary>
        public event Action Event_OnDeactive;
        /// <summary>
        /// UI 完全隐藏
        /// </summary>
        public event Action Event_OnHide;
        /// <summary>
        /// 刷新数据
        /// </summary>
        public event Action Event_OnRefresh;

        IEnumerator Process;

        protected sealed override void Awake() {
            OnInit();
            SetUIVisible(false);
            SetUIInteractable(false);
            BindUIEvent();
        }

        protected virtual void BindUIEvent() {
        }

        protected virtual void OnInit() {
        }

        protected void SetUIVisible(bool value) {
            if (!gameObject.activeSelf) {
                gameObject.SetActive(true);
            }
            m_body.alpha = value ? 1 : 0;
            m_body.blocksRaycasts = value;
        }

        protected void SetUIInteractable(bool value) {
            m_body.interactable = value;    // 所有控件aviable
        }

        protected virtual IEnumerator DoPlayShowAnimation() {
            yield break;
        }

        protected virtual IEnumerator DoPlayHideAnimation() {
            yield break;
        }

        public Coroutine Show(bool sendEvent = true) {
            Debug.Log($"UI <{this.name}> is Showing");
            IsShow = true;
            if (Process != null) {
                StopCoroutine(Process);
                Process = null;
            }
            Process = ProgressShow(sendEvent);
            return StartCoroutine(Process);
        }

        private IEnumerator ProgressShow(bool sendEvent) {
            
            SetUIVisible(true);
            OnActive(); // 先执行页面初始化逻辑
            if (sendEvent) {
                Event_OnActive?.Invoke();
            }
            yield return DoPlayShowAnimation();
            SetUIInteractable(true);
            OnShow();
            if (sendEvent) {
                Event_OnShow?.Invoke();
            }
        }

        /// <summary>
        /// close
        /// </summary>
        /// <param name="deactiveCallback"> UI完全关闭后的回调</param>
        public Coroutine Close(bool raiseEvent = false) {
            Debug.Log($"UI <{this.name}> is Closing");
            if (Process != null) {
                StopCoroutine(Process);
                Process = null;
            }
            Process = ProgressClose(raiseEvent);
            return StartCoroutine(Process);
        }

        IEnumerator ProgressClose(bool raiseEvent) {
            
            SetUIInteractable(false);
            OnDeactive();
            if (raiseEvent) {
                Event_OnDeactive?.Invoke();
            }
            // 先播放动画后设置
            yield return DoPlayHideAnimation();

            SetUIVisible(false);
            IsShow = false;
            OnHide();
            if (raiseEvent) {
                Event_OnHide?.Invoke();
            }
            //onHideCallback?.Invoke();
        }

        public override bool IsActive() => base.IsActive() && m_body.alpha > 0;

        protected override void OnValidate() {
            if (m_body == null) {
                m_body = GetComponent<CanvasGroup>();
            }
        }

        public virtual void ResetUI() {
        }

        /// <summary>
        /// 界面激活,每次打开都会触发这个函数
        /// 在动画播放前执行
        /// </summary>
        protected virtual void OnActive() {
        }

        /// <summary>
        /// 界面完成显示,每次打开都会触发这个函数
        /// 此时主界面动画已经播放完成
        /// </summary>
        protected virtual void OnShow() {
        }

        /// <summary>
        /// 界面开始关闭,此时还没开始播放关闭动画
        /// </summary>
        protected virtual void OnDeactive() {
        }

        /// <summary>
        /// 界面消失,此时关闭动画已经完成
        /// </summary>
        protected virtual void OnHide() { }

#if UNITY_EDITOR

        [ContextMenu("改名")]
        void ChangeREsName() {
            UnityEditor.Selection.gameObjects.First().name = this.GetType().Name;
        }

#endif
    }
}