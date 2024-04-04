
using TMPro;

using UnityEngine;
using UnityEngine.UI;
namespace BW.GameCode.UI
{
    [DisallowMultipleComponent]
    public class SimpleMessageBoxDialog : MessageBoxWindow
    {
        [Space(20)]
        [Header("Dialog Controllers")]
        [SerializeField] TextMeshProUGUI m_titleText = default;
        [SerializeField] TextMeshProUGUI m_contentText = default;

        [SerializeField] Button m_yesButton = default;
        [SerializeField] Button m_noButton = default;
        [SerializeField] Button m_cancelButton = default;

        protected override string Title { get => m_titleText.text; set => m_titleText.SetText(value); }
        protected override string Content { get => m_contentText.text; set => m_contentText.SetText(value); }

        protected override void DisplayButtons(MessageBoxButton btnType) {
            m_yesButton.gameObject.SetActive(btnType.HasFlag(MessageBoxButton.Yes));
            m_noButton.gameObject.SetActive(btnType.HasFlag(MessageBoxButton.No));
            m_cancelButton.gameObject.SetActive(btnType.HasFlag(MessageBoxButton.Cancel));
        }

        protected void Awake() {
            m_yesButton.onClick.AddListener(OnYesButtonClick);
            m_cancelButton.onClick.AddListener(OnCancelButtonClick);
            m_noButton.onClick.AddListener(OnNoButtonClick);
        }
    }
}