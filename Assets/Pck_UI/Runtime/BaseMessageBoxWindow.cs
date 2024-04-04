using Cysharp.Threading.Tasks;

using DG.Tweening;

using UnityEngine;
namespace BW.GameCode.UI
{
    public enum MessageBoxButtonStyle
    {
        Yes,// 是
        YesNo,// 是+否
        YesNoCancel,// 是+否+取消
    }

    public enum MessageBoxButton
    {
        Invalid,
        No,
        Yes,
        Cancel,
    }

    public abstract class BaseMessageBoxWindow : MonoBehaviour
    {
        [SerializeField] CanvasGroup m_body;
        protected abstract string Title { get; set; }
        protected abstract string Content { get; set; }

        UniTaskCompletionSource<MessageBoxButton> clickResult;

        public async UniTask<MessageBoxButton> Show(string content, string title, MessageBoxButtonStyle btnType = MessageBoxButtonStyle.Yes) {
            Title = title;
            Content = content;
            clickResult = new UniTaskCompletionSource<MessageBoxButton>();
            SetupButton(btnType);
            SetBodyVisible(true);
            await PlayShowAnimation();
            SetBodyInteractable(true);
            var result = await clickResult.Task;
            SetBodyInteractable(false);
            await PlayHideAnimation();
            SetBodyVisible(false);
            return result;
        }

        protected async UniTask PlayShowAnimation() {
            await m_body.DOFade(1f, 0.25f);
        }

        protected async UniTask PlayHideAnimation() {
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

        protected void OnYesButtonClick() {
            clickResult.TrySetResult(MessageBoxButton.Yes);
        }

        protected void OnNoButtonClick() {
            clickResult.TrySetResult(MessageBoxButton.No);
        }

        protected void OnCancelButtonClick() {
            clickResult.TrySetResult(MessageBoxButton.Cancel);
        }

        public abstract void SetupButton(MessageBoxButtonStyle btnType);
    }
}