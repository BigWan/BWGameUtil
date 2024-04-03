using TMPro;

using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;

namespace BW.GameCode.UI
{
    public enum MessageBoxButtonType
    {
        Yes,
        YesNo,
        YesNoCancel,
    }

    public enum MessageBoxButton
    {
        None,
        No,
        Yes,
        Cancel,
    }

    public abstract class BaseMessageBoxDialog : BaseUI
    {
        protected abstract string Title { get; set; }
        protected abstract string Content { get; set; }

        UniTaskCompletionSource<MessageBoxButton> clickResult;

        public async UniTask<MessageBoxButton> Show(string content, string title, MessageBoxButtonType btnType = MessageBoxButtonType.Yes) {
            Title = title;
            Content = content;
            clickResult = new UniTaskCompletionSource<MessageBoxButton>();
            DisplayButton(btnType);
            Show();
            return await clickResult.Task;
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

        public abstract void DisplayButton(MessageBoxButtonType btnType);
    }

    public class MessageBoxDialog2 : BaseUI
    {
        [Space(20)]
        [Header("Dialog Controllers")]
        [SerializeField] TextMeshProUGUI m_titleText = default;
        [SerializeField] TextMeshProUGUI m_contentText = default;

        [SerializeField] Button m_yesButton = default;
        [SerializeField] Button m_noButton = default;
        [SerializeField] Button m_cancelButton = default;

        MessageBoxButton mResult = MessageBoxButton.None;

        public MessageBoxButton Result {
            get => mResult;
            set {
                if (mResult != MessageBoxButton.None) return;
                mResult = value;
            }
        }

        public void SetContent(string caption, string content, MessageBoxButtonType btnType) {
            mResult = MessageBoxButton.None;
            m_titleText.SetText(caption);
            m_contentText.SetText(content);
            m_noButton.gameObject.SetActive(btnType == MessageBoxButtonType.YesNo);
            m_cancelButton.gameObject.SetActive(btnType == MessageBoxButtonType.YesNoCancel);
        }

        protected override void OnAwake() {
            m_yesButton.onClick.AddListener(() => Result = MessageBoxButton.Yes);
            m_cancelButton.onClick.AddListener(() => Result = MessageBoxButton.Cancel);
            m_noButton.onClick.AddListener(() => Result = MessageBoxButton.No);
        }

        //protected override void OnActive() {
        //    base.OnActive();

        //}

        //protected override void OnDeactive() {
        //    Result = MessageBoxResult.None;
        //    base.OnDeactive();
        //}
    }
}