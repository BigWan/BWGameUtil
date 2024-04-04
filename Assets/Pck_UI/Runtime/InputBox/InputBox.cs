using BW.GameCode.Singleton;

using Cysharp.Threading.Tasks;

using System;
using System.Collections.Generic;

using UnityEngine;

namespace BW.GameCode.UI
{
    public struct InputData
    {
        public bool Cancel;
        public string InputValue;
    }

    /// <summary>
    /// 输入框
    /// </summary>
    public sealed class InputBox : SimpleSingleton<InputBox>
    {
        [SerializeField] SimpleInputDialog m_uiPrefab;
        [SerializeField] Transform m_dialogParent;

        [SerializeField] int m_cacheCount = 5;

        Queue<SimpleInputDialog> mCaches = new Queue<SimpleInputDialog>();

        SimpleInputDialog CreateDialog() {
            SimpleInputDialog obj;
            if (mCaches.Count > 0) {
                obj = mCaches.Dequeue();
            } else {
                obj = Instantiate<SimpleInputDialog>(m_uiPrefab, m_dialogParent);
            }
            obj.gameObject.name = "Actived";
            return obj;
        }

        private void RecycleDialog(SimpleInputDialog ui) {
            if (mCaches.Count > m_cacheCount) {
                Destroy(ui.gameObject);
            } else {
                ui.gameObject.name = "Cached";
                mCaches.Enqueue(ui);
            }
        }

        public static async UniTask<InputData> Show(string title, string content, string placeholder = "", int characterLimit = 0, bool cancleAble = true,
            Predicate<string> checkFunc = default) {
            var dialog = I.CreateDialog();

            dialog.ContentType = TMPro.TMP_InputField.ContentType.Standard;
            dialog.SetCancelButtonActive(cancleAble);
            var result = await dialog.Show(content, title, placeholder, characterLimit, checkFunc);
            I.RecycleDialog(dialog);
            return result;
        }

        //IEnumerator ProcessInput(SimpleInputDialog dialog, Action<bool, string> callback) {
        //    dialog.Show();
        //    while (dialog.Result == InputResult.None) {
        //        yield return null;
        //    }
        //    var result = dialog.InputValue;
        //    bool confirm = dialog.Result == InputResult.Confirm;
        //    dialog.Close();
        //    callback?.Invoke(confirm, result);
        //}
    }
}