
using TMPro;

using UnityEngine;
using UnityEngine.UI;

using static TMPro.TMP_InputField;

namespace BW.GameCode.UI
{
    public class SimpleInputDialog : InputWindow
    {
        [Header("Input Filed")]
        [SerializeField] TMP_InputField m_input = default;

        [Header("Buttons")]
        [SerializeField] Button m_cancelButton = default;
        [SerializeField] Button m_confirmButton = default;

        [Header("Text Cmpts")]
        [SerializeField] TextMeshProUGUI m_placeHolder = default;   // 输入区域提示文本
        [SerializeField] TextMeshProUGUI m_captionText;             // 标题文本
        [SerializeField] TextMeshProUGUI m_contentText;             // 输入内容
        [SerializeField] TextMeshProUGUI m_warningText = default;      // 检查警告文本

        public void SetCancelButtonActive(bool active) {
            m_cancelButton.gameObject.SetActive(active);
        }

        public override string PlaceHolder { get => m_placeHolder.text; set => m_placeHolder.text = value; }
        public override int CharacterLimit { get => m_input.characterLimit; set => m_input.characterLimit = value; }
        public override string Title { get => m_captionText.text; set => m_captionText.text = value; }
        public override string Content { get => m_contentText.text; set => m_contentText.text = value; }
        public ContentType ContentType { get => m_input.contentType; set => m_input.contentType = value; }

        protected override string Value => m_input.text;

        protected void Awake() {
            m_cancelButton.onClick.AddListener(OnCancelButtonClick);
            m_confirmButton.onClick.AddListener(OnConfirmButtonClick);
            m_input.onValueChanged.AddListener(OnValueChanged);
            //m_tipCloseButton.Event_OnClick.AddListener(() => m_tipBubber.DOFade(0, 0.25f));
            //m_tipBubber.alpha = 0;
        }
    }
}