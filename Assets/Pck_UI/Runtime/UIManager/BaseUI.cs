using System;
using System.Collections;

using UnityEngine;
using UnityEngine.EventSystems;
namespace BW.GameCode.UI
{
    /// <summary>
    /// 最基础的UI组件,可以打开,关闭,以及一些回调
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    [DisallowMultipleComponent]
    public class BaseUI : UIBehaviour
    {
        [Header("Body Canvas")]
        [SerializeField] CanvasGroup m_body = default; // 不要Fade这个CanvasGroup,因为UI会直接设置他的值
        [SerializeField] UIType m_uiType = default;

        [Header("关闭后卸载还是缓存")]
        [SerializeField] bool m_autoDestroyOnHide;
        public bool IsShow { get; private set; }

        public bool AutoDestroyOnHide => m_autoDestroyOnHide;

        public UIType UIType => m_uiType;

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
        public event Action Event_OnClose;
        /// <summary>
        /// UI 完全隐藏
        /// </summary>
        public event Action Event_OnDeactive;
        /// <summary>
        /// 刷新数据
        /// </summary>
        public event Action Event_OnRefresh;

        Coroutine mShowHideCoroutine; // 显示和关闭的携程

        protected sealed override void Awake() {
            //// 5-28 最后处理: Awake的时候不处理动画,初始化放在每次Show的时候
            //// 5-29 还是这里Init吧,因为如果每次Show都Init的话,动画会闪烁
            //if (m_animation != null) {
            //    m_animation.InitNode();
            //}

            SetBodyVisible(false);
            SetBodyInteractable(false);
            OnAwake();
        }

        protected virtual void OnAwake() {
        }

        protected virtual void FindRefs() {
            //if (m_animation == null) {
            //    m_animation = GetComponent<AbstractMotionNode>();
            //}
            if (m_body == null) {
                m_body = GetComponent<CanvasGroup>();
            }
        }

        protected virtual void SetBodyVisible(bool value) {
            if (!gameObject.activeSelf) {
                gameObject.SetActive(true);
            }
            m_body.alpha = value ? 1 : 0;
            m_body.blocksRaycasts = value;
        }

        protected virtual void SetBodyInteractable(bool value) {
            m_body.interactable = value;    // 所有控件aviable
        }

        private IEnumerator DoProcessShowAnimation() {
            //if (m_animation != null) {
            //    yield return m_animation.PlayForward();
            //}
            yield return DoPlayShowAnimation();
        }

        protected virtual IEnumerator DoPlayShowAnimation() {
            yield break;
        }

        protected virtual IEnumerator DoPlayHideAnimation() {
            yield break;
        }

        public void Show() {
            Debug.Log($"UI <{this.name}> is Showing");

            IsShow = true;
            if (mShowHideCoroutine != null) {
                StopCoroutine(mShowHideCoroutine);
            }
            mShowHideCoroutine = StartCoroutine(ShowProcess());
        }

        /// <summary>
        /// close
        /// </summary>
        /// <param name="deactiveCallback"> UI完全关闭后的回调</param>
        public void Close() {
            Debug.Log($"UI <{this.name}> is Closing");
            if (mShowHideCoroutine != null) {
                StopCoroutine(mShowHideCoroutine);
            }

            mShowHideCoroutine = StartCoroutine(ProgressClose());
        }

        private IEnumerator ShowProcess() {
            //RichLog.Log($"<{this.name}> is show");
            SetBodyVisible(true);
            OnActive(); // 先执行页面初始化逻辑
            Event_OnActive?.Invoke();
            yield return DoProcessShowAnimation();

            OnShow();
            Event_OnShow?.Invoke();
            SetBodyInteractable(true);
            //callback?.Invoke();
            //RichLog.EndLog($"<{this.name}> is Active");
        }

        private IEnumerator ProgressClose() {
            //RichLog.Log($"<{this.name}> is Close");
            SetBodyInteractable(false);
            OnClose();
            Event_OnClose?.Invoke();
            // 先播放动画后设置
            yield return DoPlayHideAnimation();

            SetBodyVisible(false);

            OnDeactive();
            IsShow = false;
            Event_OnDeactive?.Invoke();
            //callback?.Invoke();
            //RichLog.Log($"{name} Is Deactive");
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
        protected virtual void OnClose() {
        }

        /// <summary>
        /// 界面消失,此时关闭动画已经完成
        /// </summary>
        protected virtual void OnDeactive() { }
    }
}