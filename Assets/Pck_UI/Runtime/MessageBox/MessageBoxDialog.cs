using TMPro;

using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

namespace BW.GameCode.UI
{


    public delegate void MessageBoxClickDelegate(MessageBoxButton clickButton);

    /// <summary>
    /// 消息对话框
    /// </summary>
    [DisallowMultipleComponent]
    public abstract class MessageBoxDialog : MonoBehaviour
    {
        [SerializeField] CanvasGroup m_body;
        public event Action Event_OnHide;
        public MessageBoxButton? Result { get; private set; }

        protected void SetResult(MessageBoxButton clickBtn) {
            Result = clickBtn;
        }

        protected abstract string Title { get; set; }
        protected abstract string Content { get; set; }

        Coroutine handler;

        public void Show(string content, string title, MessageBoxButton btnType, MessageBoxClickDelegate callback) {
            if (handler != null) {
                StopCoroutine(handler);
            }
            handler = StartCoroutine(Process(content, title, btnType, callback));
        }

        protected IEnumerator Process(string content, string title, MessageBoxButton btnType, MessageBoxClickDelegate callback = default) {
            Title = title;
            Content = content;
            Result = null;
            DisplayButtons(btnType);
            SetBodyVisible(true);
            yield return FadeInProcess();
            SetBodyInteractable(true);
            yield return new WaitUntil(() => Result != null);
            SetBodyInteractable(false);
            callback?.Invoke(Result.Value);
            yield return FadeOutProcess();
            SetBodyVisible(false);
            Event_OnHide?.Invoke();
        }

        protected virtual IEnumerator FadeInProcess() {
            yield break;
        }

        protected virtual IEnumerator FadeOutProcess() {
            yield break;
        }

        protected void SetBodyVisible(bool value) {
            if (!gameObject.activeSelf) {
                gameObject.SetActive(true);
            }
            m_body.alpha = value ? 1 : 0;
            m_body.blocksRaycasts = value;
        }

        protected void SetBodyInteractable(bool value) {
            m_body.interactable = value;    // 所有控件aviable
        }

        protected void OnButtonClick(MessageBoxButton button) {
            Result = button;
        }

        protected void OnYesButtonClick() {
            Result = MessageBoxButton.Yes;
        }

        protected void OnNoButtonClick() {
            Result = MessageBoxButton.No;
        }

        protected void OnCancelButtonClick() {
            Result = MessageBoxButton.Cancel;
        }

        protected abstract void DisplayButtons(MessageBoxButton btnType);
    }
}