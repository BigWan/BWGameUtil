using TMPro;

using UnityEngine;

namespace BW.GameCode.UI
{
    public enum EInputContentType
    {
        Name,
        Number,
    }

    //public struct InputDialogResult
    //{
    //    public InputDialogResult(MessageBoxButton result, string value) {
    //        this.button = result;
    //        this.Value = value;
    //    }

    //    public string Value { get; }
    //    public MessageBoxButton button { get; }
    //}

    //public class InputDialog : CommonDialog
    //{
    //    [Header("Input Filed")]
    //    [SerializeField] TMP_InputField m_input = default;
    //    [SerializeField] TextMeshProUGUI m_placeHolder = default;

    //    public string InputValue => m_input.text;

    //    public void SetPlaceHolder(string placeHolder) => m_placeHolder.SetText(placeHolder);

    //    public void SetInputType(EInputContentType type) {
    //    }

    //    public void SetCharacterLimit(int limit) {
    //        m_input.characterLimit = limit;
    //    }

    //    protected override void OnActive() {
    //        base.OnActive();
    //        GUIHelper.FocusInput(m_input);
    //    }
    //}
}