using TMPro;

using UnityEngine;
using System;
using UnityEngine.UI;

namespace BW.GameCode.UI
{
    public class SimplgeMessageDialog : MessageBoxDialog
    {
        [SerializeField] TextMeshProUGUI m_titleText;
        [SerializeField] TextMeshProUGUI m_contentText;
        [SerializeField] Button m_yesButton;
        [SerializeField] Button m_noButton;
        [SerializeField] Button m_cancelButton;
        protected override string Title { get => m_titleText.text; set => m_titleText.SetText(value); }
        protected override string Content { get => m_contentText.text; set => m_contentText.SetText(value); }

        private void Awake() {
            m_yesButton.onClick.AddListener(OnYesButtonClick);
            m_noButton.onClick.AddListener(OnNoButtonClick);
            m_cancelButton.onClick.AddListener(OnCancelButtonClick);
        }

        private void OnDestroy() {
            m_yesButton.onClick.RemoveAllListeners();
            m_noButton.onClick.RemoveAllListeners();
            m_cancelButton.onClick.RemoveAllListeners();
        }

        protected override void DisplayButtons(MessageBoxButton btnType) {
            m_yesButton.gameObject.SetActive(btnType.HasFlag(MessageBoxButton.Yes));
            m_noButton.gameObject.SetActive(btnType.HasFlag(MessageBoxButton.No));
            m_cancelButton.gameObject.SetActive(btnType.HasFlag(MessageBoxButton.Cancel));
        }
    }
}