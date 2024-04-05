using DG.Tweening;

using System;

using TMPro;

using UnityEngine;

using static TMPro.TMP_InputField;

namespace BW.GameCode.UI
{
    public delegate int CheckInputValueDelegate(string value);

    public class InputDialog : BaseUI
    {
        [Header("Input Filed")]
        [SerializeField] TMP_InputField m_input = default;




        [Header("Text Cmpts")]
        [SerializeField] TextMeshProUGUI m_placeHolder = default;   // 输入区域提示文本
        [SerializeField] TextMeshProUGUI m_captionText;             // 标题文本
        [SerializeField] TextMeshProUGUI m_contentText;             // 输入内容
        [SerializeField] TextMeshProUGUI m_warningText = default;      // 检查警告文本

        public void SetCancelButtonActive(bool active) {
            //m_cancelButton.gameObject.SetActive(active);
        }

        public InputResult Result { get; private set; } = InputResult.None;
        public string InputValue => m_input.text;
        public CheckInputValueDelegate CheckFunc { get; set; }
        public string PlaceHolder { get => m_placeHolder.text; set => m_placeHolder.text = value; }
        public int CharacterLimit { get => m_input.characterLimit; set => m_input.characterLimit = value; }
        public string Caption { get => m_captionText.text; set => m_captionText.text = value; }
        public string Content { get => m_contentText.text; set => m_contentText.text = value; }
        public ContentType ContentType { get => m_input.contentType; set => m_input.contentType = value; }

        public void SetContent(string title, string content, string placeHolder, ContentType contentType = ContentType.Standard, int CharLimit = 30) {
            Content = content;
            Caption = title;
            PlaceHolder = placeHolder;
        }

        protected override void OnAwake() {

            //m_cancelButton.Event_OnClick.AddListener(OnCancelClick);
            //m_confirmButton.Event_OnClick.AddListener(OnConfirmClick);
            m_input.onValueChanged.AddListener(OnValueChanged);
            //m_tipCloseButton.Event_OnClick.AddListener(() => m_tipBubber.DOFade(0, 0.25f));
            //m_tipBubber.alpha = 0;
        }

        private void OnValueChanged(string inputValue) {
        }

        protected override void OnActive() {
            base.OnActive();
            Result = InputResult.None;
        }

        private void OnCancelClick() {
            //Event_OnCancelClicked?.Invoke();
            Result = InputResult.Cancel;
        }

        private void OnConfirmClick() {
            //Event_OnConfirmClicked?.Invoke();
            if (CheckFunc == null || CheckFunc(InputValue) == 0) {
                Result = InputResult.Confirm;
                return;
            }

            m_warningText.text = "内容有误!";
            //m_tipBubber.DOFade(1, 0.25f).From(0);
            //if (m_tipSound != null) {
            //    m_tipSound.Play(m_audioSource);
            //}

        }

        protected override void OnShow() {
            base.OnShow();
            if (m_input != null) {
                m_input.ActivateInputField();
            }
        }
    }
}