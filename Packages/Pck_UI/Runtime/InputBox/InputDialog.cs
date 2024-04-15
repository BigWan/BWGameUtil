using DG.Tweening;

using System;
using System.Collections;

using TMPro;

using UnityEngine;

using static TMPro.TMP_InputField;

namespace BW.GameCode.UI
{
    public struct InputResult
    {
        /// <summary>
        /// 是否输入被取消了
        /// </summary>
        public bool Cancle { get; set; }
        /// <summary>
        /// 输入成功的值
        /// </summary>
        public string Text { get; set; }
    }

    public class InputBoxArgument
    {
        public string Title;
        public string Text = default;
        /// <summary>
        /// 输入框是否可以取消
        /// </summary>
        public bool cancelAble = true;  
        public ContentType ContentType = ContentType.Standard;
        public int CharacterLimit = 30;
        public CheckInputValueDelegate Checker = default;
    }

    public delegate bool CheckInputValueDelegate(string value);

    public abstract class InputDialog : MonoBehaviour
    {
        [SerializeField] CanvasGroup m_body;

        public event Action Event_OnHide;
        public InputResult? Result { get; private set; }
        public abstract string InputValue { get; }

        protected CheckInputValueDelegate checkFunc;

        /// <summary>
        /// 内容类型
        /// </summary>
        protected abstract ContentType ContentType { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        protected abstract string Title { get; set; }
        /// <summary>
        /// 说明文本
        /// </summary>
        protected abstract string Content { get; set; }

        /// <summary>
        /// 字数限制
        /// </summary>
        protected abstract int CharacterLimit { get; set; }
        /// <summary>
        /// 占位符
        /// </summary>
        protected abstract string PlaceHolder { get; set; }

        Coroutine handler;

        private void OnValueChanged(string curValue) {
        }

        public void SetCancelButtonActive(bool active) {
            //m_cancelButton.gameObject.SetActive(active);
        }

        public void Show(InputBoxArgument args, Action<InputResult> callback) {
            if (handler != null) {
                StopCoroutine(handler);
            }
            handler = StartCoroutine(Process(args, callback));
        }

        protected IEnumerator Process(InputBoxArgument args, Action<InputResult> callback) {
            Title = args.Title;
            Content = args.Text;
            Result = null;
            SetBodyVisible(true);
            yield return FadeInProcess();
            SetBodyInteractable(true);
            OnShow();
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

        protected void OnConfirmButtonClick() {
            if (checkFunc == null || checkFunc(InputValue)) {
                Result = new InputResult()
                {
                    Cancle = false,
                    Text = InputValue
                };
            } else {
                // TODO:display warning
                DisplayWarning();
            }
        }

        protected virtual void DisplayWarning() { }
        protected void OnCancelButtonClick() {
            Result = new InputResult()
            {
                Cancle = true,
                Text = string.Empty
            };
        }

        protected abstract void DisplayButtons(MessageBoxButton btnType);

        protected virtual void OnShow() { }

        //protected void OnShow() {
        //    if (m_input != null) {
        //        m_input.ActivateInputField();
        //    }
        //}
    }
}