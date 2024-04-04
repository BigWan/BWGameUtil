using Cysharp.Threading.Tasks;

using DG.Tweening;

using UnityEngine;
namespace BW.GameCode.UI
{
    [System.Flags]
    public enum MessageBoxButton
    {
        Invalid = 0,
        No = 1,
        Yes = 2,
        Cancel = 4,
    }

    [DisallowMultipleComponent]
    public abstract class MessageBoxWindow : MonoBehaviour
    {
        [SerializeField] CanvasGroup m_body;
        protected abstract string Title { get; set; }
        protected abstract string Content { get; set; }

        UniTaskCompletionSource<MessageBoxButton> clickResult;

        public async UniTask<MessageBoxButton> Show(string content, string title, MessageBoxButton btnType = MessageBoxButton.Yes) {
            Title = title;
            Content = content;
            clickResult = new UniTaskCompletionSource<MessageBoxButton>();
            DisplayButtons(btnType);
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

        protected abstract void DisplayButtons(MessageBoxButton btnType);
    }
}