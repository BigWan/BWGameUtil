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
        //[Foldout("adfadf")]

        [Header("Root Animation")]
        [SerializeField] AbstractMotionNode m_animation = default;
        [Header("Body Canvas")]
        [SerializeField] CanvasGroup m_body = default; // 不要Fade这个CanvasGroup,因为UI会直接设置他的值

        public bool IsShow { get; private set; }

        public event Action Event_OnActive;
        public event Action Event_OnShow;
        public event Action Event_OnClose;
        public event Action Event_OnDeactive;
        public event Action Event_OnRefresh;
        Coroutine mShowHideCoroutine; // 显示和关闭的携程

        protected sealed override void Awake() {
            // 5-28 最后处理: Awake的时候不处理动画,初始化放在每次Show的时候
            // 5-29 还是这里Init吧,因为如果每次Show都Init的话,动画会闪烁
            if (m_animation != null) {
                m_animation.InitNode();
            }

            SetBodyVisible(false);
            SetBodyInteractable(false);
            OnAwake();
        }

        protected virtual void OnAwake() {
        }

        protected virtual void FindRefs() {
            if (m_animation == null) {
                m_animation = GetComponent<AbstractMotionNode>();
            }
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
            if (m_animation != null) {
                yield return m_animation.PlayForward();
            }
            yield return DoPlayShowAnimation();
        }

        protected virtual IEnumerator DoPlayShowAnimation() {
            yield break;
        }

        protected virtual IEnumerator DoPlayHideAnimation() {
            yield break;
        }

        private IEnumerator DoProcessHideAnimation() {
            yield return DoPlayHideAnimation();
            if (m_animation != null) {
                yield return m_animation.PlayBackword();
            }
        }

        public void Show() {
            RichLog.Log($"UI <{this.name}> is Showing");

            IsShow = true;
            if (mShowHideCoroutine != null) {
                StopCoroutine(mShowHideCoroutine);
            }
            mShowHideCoroutine = StartCoroutine(DoShow());
        }

        /// <summary>
        /// close
        /// </summary>
        /// <param name="deactiveCallback"> UI完全关闭后的回调</param>
        public void Close() {
            RichLog.Log($"UI <{this.name}> is Closing");
            if (mShowHideCoroutine != null) {
                StopCoroutine(mShowHideCoroutine);
            }
            IsShow = false;
            mShowHideCoroutine = StartCoroutine(DoClose());
        }

        private IEnumerator DoShow() {
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

        private IEnumerator DoClose() {
            //RichLog.Log($"<{this.name}> is Close");
            SetBodyInteractable(false);
            OnClose();
            Event_OnClose?.Invoke();
            // 先播放动画后设置
            yield return DoProcessHideAnimation();

            SetBodyVisible(false);
            //canvasGroup.alpha = 0;

            OnDeactive();
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