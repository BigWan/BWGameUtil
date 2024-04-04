using Cysharp.Threading.Tasks;

using DG.Tweening;

using System;

using UnityEngine;

namespace BW.GameCode.UI
{

    public abstract class InputWindow : MonoBehaviour
    {
        [SerializeField] CanvasGroup m_body;
        public abstract string Title { get; set; }
        public abstract string Content { get; set; }
        public abstract int CharacterLimit { get; set; }
        public abstract string PlaceHolder { get; set; }
        protected abstract string Value { get; }

        UniTaskCompletionSource<bool> completionSource;
        Predicate<string> inputPredicate;

        public async UniTask<InputData> Show(string content, string title, string placeHolder, int charLimit = 30, Predicate<string> inputPredicate = default) {
            Title = title;
            Content = content;
            PlaceHolder = placeHolder;
            CharacterLimit = charLimit;
            this.inputPredicate = inputPredicate;
            completionSource = new UniTaskCompletionSource<bool>();

            SetBodyVisible(true);
            await PlayShowAnimation();
            SetBodyInteractable(true);
            var result = await completionSource.Task;
            SetBodyInteractable(false);
            await PlayHideAnimation();
            SetBodyVisible(false);
            return new InputData() {
                Cancel = result,
                InputValue = Value
            };
        }

        protected virtual async UniTask PlayShowAnimation() {
            await m_body.DOFade(1f, 0.25f);
        }

        protected virtual async UniTask PlayHideAnimation() {
            await m_body.DOFade(0f, 0.25f);
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

        protected bool IsValueAviable() {
            return inputPredicate?.Invoke(Value) ?? true;
        }

        protected void OnConfirmButtonClick() {
            if (IsValueAviable()) {
                completionSource.TrySetResult(true);
            } else {
                // todo:显示警告信息
            }
        }

        protected void OnCancelButtonClick() {
            completionSource.TrySetResult(false);
        }

        protected virtual void OnValueChanged(string value) {
        }
    }
}