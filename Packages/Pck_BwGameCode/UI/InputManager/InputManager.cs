using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace BW.GameCode.UI
{
    public struct InputBoxArgument
    {
        public string Title;
        public string Content;
        /// <summary>
        /// 输入框是否可以取消
        /// </summary>
        public bool cancelAble;
        public InputContentLimit ContentType;
        public int CharacterLimit;
        public Predicate<string> Checker;

        public InputBoxArgument(string title, string text, bool cancel, InputContentLimit conentLimit, int charLimit = 30, Predicate<string> inputCheck = default) {
            this.Title = title;
            this.Content = text;
            cancelAble = cancel;
            ContentType = conentLimit;
            CharacterLimit = charLimit;
            Checker = inputCheck;
        }
    }
    /// <summary>
    /// 输入框
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        [SerializeField] InputWindow m_dialogPrefab;
        [SerializeField] Transform m_dialogParent;

        [SerializeField] int m_cacheCount = 5;

        static Queue<InputWindow> mCaches = new Queue<InputWindow>();

        InputWindow CreateDialog() {
            InputWindow obj;
            if (mCaches.Count > 0) {
                obj = mCaches.Dequeue();
            } else {
                obj = Instantiate<InputWindow>(m_dialogPrefab, m_dialogParent);
                obj.Event_OnHide += () => RecycleDialog(obj);
            }
            obj.gameObject.name = "Actived";
            return obj;
        }

        private void RecycleDialog(InputWindow ui) {
            if (mCaches.Count > m_cacheCount) {
                Destroy(ui.gameObject);
            } else {
                ui.gameObject.name = "Cached";
                mCaches.Enqueue(ui);
            }
        }

        public InputWindow Show(InputBoxArgument args, Action<InputResult> callback) {
            var dialog = CreateDialog();
            dialog.Title = args.Title;
            dialog.Content = args.Content;
            dialog.Show( callback);
            switch (args.ContentType) {               
                case InputContentLimit.Interger:
                    dialog.ContentType = TMPro.TMP_InputField.ContentType.IntegerNumber;
                    break;
                case InputContentLimit.Common :
                    dialog.ContentType = TMPro.TMP_InputField.ContentType.Standard;
                    break;
                default:
                    dialog.ContentType = TMPro.TMP_InputField.ContentType.Standard;
                    break;
            }
           

            return dialog;
        }
    }
}