using Cysharp.Threading.Tasks;

using System;

using UnityEngine;
using UnityEngine.EventSystems;
using System.Threading.Tasks;
using System.Threading;

namespace BW.GameCode.UI
{
    /// <summary>
    /// 最基础的UI组件,可以打开,关闭,以及一些回调
    /// </summary>
    [RequireComponent(typeof(CanvasGroup))]
    [DisallowMultipleComponent]
    public abstract class BaseUI : UIBehaviour
    {
        [Header("Body Canvas")]
        [SerializeField] CanvasGroup m_body = default; // 不要Fade这个CanvasGroup,因为UI会直接设置他的值
        [SerializeField] GameUILayer m_layer;
        [Header("关闭时自动摧毁")]
        [SerializeField] bool m_autoDestroy = false;
        public bool AutoDestroy => m_autoDestroy;
        public GameUILayer UILayer => m_layer;
        public bool IsShow { get; private set; }

        public event Action Event_OnActive;
        public event Action Event_OnShow;
        public event Action Event_OnClose;
        public event Action Event_OnDeactive;
        public event Action Event_OnRefresh;

        CancellationTokenSource cts = new CancellationTokenSource();

        protected sealed override void Awake() {
            OnAwake();
            //SetBodyVisible(false);
            //SetBodyInteractable(false);
        }

        protected virtual void OnAwake() {
        }

        protected virtual void FindRefs() {
            if (m_body == null) {
                m_body = GetComponent<CanvasGroup>();
            }
        }

        protected void SetBodyVisible(bool value) {
            if (!gameObject.activeSelf) {
                gameObject.SetActive(true);
            }
            m_body.alpha = value ? 1 : 0;
            m_body.blocksRaycasts = value;
            Debug.Log($"[UI]SetBodyVisible{value}");
        }

        protected void SetBodyInteractable(bool value) {
            m_body.interactable = value;    // 所有控件aviable
            Debug.Log($"[UI]SetBodyInteractable{value}");
        }

        protected virtual async UniTask PlayShowAnimation(CancellationToken ct) {
            await UniTask.CompletedTask;
        }

        protected virtual async UniTask PlayHideAnimation(CancellationToken ct) {
            await UniTask.CompletedTask;
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <returns></returns>
        protected virtual async UniTask OnRefresh(CancellationToken ct) {
            await UniTask.CompletedTask;
        }

        public async UniTask Show() {
            Debug.Log($"UI <{this.name}> is Showing");
            cts.Cancel();
            cts = new CancellationTokenSource();
            try {
                IsShow = true;
                SetBodyVisible(true);
                OnActive();
                Event_OnActive?.Invoke();
                await OnRefresh(cts.Token);
                await PlayShowAnimation(cts.Token);
                OnShow();
                SetBodyInteractable(true);
                Event_OnShow?.Invoke();
            } finally { }

        }

        /// <summary>
        /// close
        /// </summary>
        /// <param name="deactiveCallback"> UI完全关闭后的回调</param>
        public async UniTask Close() {
            Debug.Log($"UI <{this.name}> is Closing");
            cts.Cancel();
            cts = new CancellationTokenSource();
            try {
                IsShow = false;
                SetBodyInteractable(false);
                OnClose();
                Event_OnClose?.Invoke();
                // 先播放动画后设置
                await PlayHideAnimation(cts.Token);
                SetBodyVisible(false);
                //canvasGroup.alpha = 0;

                OnDeactive();
                Event_OnDeactive?.Invoke();
            } catch (OperationCanceledException ex) {
                if (ex.CancellationToken == cts.Token) {
                    Debug.Log("[UI] Close Break");
                }
            }
        }

        public override bool IsActive() => base.IsActive() && IsShow && m_body.alpha > 0;

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