using BW.GameCode.Singleton;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace BW.GameCode.UI
{
    /// <summary>
    /// 输入框
    /// </summary>
    public class InputBox : SimpleSingleton<InputBox>
    {
        [SerializeField] InputDialog m_uiPrefab;
        [SerializeField] Transform m_dialogParent;

        [SerializeField] int m_cacheCount = 5;

        Queue<InputDialog> mCaches = new Queue<InputDialog>();

        InputDialog CreateDialog() {
            InputDialog obj;
            if (mCaches.Count > 0) {
                obj = mCaches.Dequeue();
            } else {
                obj = Instantiate<InputDialog>(m_uiPrefab, m_dialogParent);
                obj.Event_OnHide += () => RecycleDialog(obj);
                //obj.Callback_OnDeactive.AddListener(()=>OnDialogDeactive(obj));
            }
            obj.gameObject.name = "Actived";
            return obj;
        }

        private void RecycleDialog(InputDialog ui) {
            if (mCaches.Count > m_cacheCount) {
                Destroy(ui.gameObject);
            } else {
                ui.gameObject.name = "Cached";
                mCaches.Enqueue(ui);
            }
        }

        public static void Show(string title, string content, string placeholder = "", int characterLimit = 0, bool showCancelButton = true,
            Action<bool, string> callback = default,// bool =true 表示confirm
            CheckInputValueDelegate checkFunc = default) {
            I.ShowInternal(title, content, placeholder, characterLimit, showCancelButton, callback, checkFunc);
        }

        void ShowInternal(string title, string content, string placeholder = "", int characterLimit = 0, bool showCancelButton = true,
           Action<bool, string> callback = default,// bool =true 表示confirm
           CheckInputValueDelegate checkFunc = default) {
            var dialog = CreateDialog();
            dialog.Caption = title;
            dialog.Content = content;
            dialog.PlaceHolder = placeholder;
            dialog.CharacterLimit = characterLimit;
            dialog.ContentType = TMPro.TMP_InputField.ContentType.Standard;
            dialog.checkFunc = checkFunc;
            dialog.SetCancelButtonActive(showCancelButton);
            StartCoroutine(ProcessInput(dialog, callback));
        }

        IEnumerator ProcessInput(InputDialog dialog, Action<bool, string> callback) {
            dialog.Show();
            while (dialog.Result == InputResult.None) {
                yield return null;
            }
            var result = dialog.InputValue;
            bool confirm = dialog.Result == InputResult.Confirm;
            dialog.Close();
            callback?.Invoke(confirm, result);
        }
    }
}