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
    public class BaseUIPage : UIBehaviour
    {
        [Header("Body Canvas")]
        [SerializeField] CanvasGroup m_body = default; // 不要Fade这个CanvasGroup,因为UI会直接设置他的值
        [SerializeField] int m_uiType = default;

        [Header("关闭后卸载还是缓存")]
        [SerializeField] bool m_autoDestroyOnHide;
        public bool IsShow { get; private set; }
        public bool AutoDestroyOnHide => m_autoDestroyOnHide;
        public int UITypeCode => m_uiType;

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

        Coroutine mShowHideCoroutine; // 显示和关闭的携程

        protected sealed override void Awake() {
            SetUIVisible(false);
            SetUIInteractable(false);
            OnInit();
            BindUIEvent();
        }

        protected virtual void BindUIEvent() {
        }

        protected virtual void OnInit() {
        }

        protected virtual void FindRefs() {
            //if (m_animation == null) {
            //    m_animation = GetComponent<AbstractMotionNode>();
            //}
            if (m_body == null) {
                m_body = GetComponent<CanvasGroup>();
            }
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

        public void Show(Action callback = default) {
            Debug.Log($"UI <{this.name}> is Showing");

            IsShow = true;
            if (mShowHideCoroutine != null) {
                StopCoroutine(mShowHideCoroutine);
            }
            mShowHideCoroutine = StartCoroutine(ShowProcess(callback));
        }

        private IEnumerator ShowProcess(Action onShowedCallback = default) {
            Debug.Log($"<{this.name}> is ShowProcess");
            SetUIVisible(true);
            OnActive(); // 先执行页面初始化逻辑
            Event_OnActive?.Invoke();
            yield return DoPlayShowAnimation();
            SetUIInteractable(true);
            OnShow();
            Event_OnShow?.Invoke();
            onShowedCallback?.Invoke();
            Debug.Log($"<{this.name}> is Active");
        }

        /// <summary>
        /// close
        /// </summary>
        /// <param name="deactiveCallback"> UI完全关闭后的回调</param>
        public void Close(Action callback = default) {
            Debug.Log($"UI <{this.name}> is Closing");
            if (mShowHideCoroutine != null) {
                StopCoroutine(mShowHideCoroutine);
            }

            mShowHideCoroutine = StartCoroutine(ProgressClose(callback));
        }

        private IEnumerator ProgressClose(Action onHideCallback) {
            Debug.Log($"<{this.name}> is Close");
            SetUIInteractable(false);
            OnDeactive();
            Event_OnDeactive?.Invoke();
            // 先播放动画后设置
            yield return DoPlayHideAnimation();

            SetUIVisible(false);
            IsShow = false;
            OnHide();
            Event_OnHide?.Invoke();
            onHideCallback?.Invoke();
            Debug.Log($"{name} Is Deactive");
        }

        public override bool IsActive() => base.IsActive() && m_body.alpha > 0;

        protected override void Reset() => FindRefs();

        protected override void OnValidate() => FindRefs();

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