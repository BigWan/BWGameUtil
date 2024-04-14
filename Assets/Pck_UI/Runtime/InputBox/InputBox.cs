using BW.GameCode.Core;

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
        [SerializeField] InputDialog m_dialogPrefab;
        [SerializeField] Transform m_dialogParent;

        [SerializeField] int m_cacheCount = 5;

        static Queue<InputDialog> mCaches = new Queue<InputDialog>();

        static InputDialog CreateDialog() {
            InputDialog obj;
            if (mCaches.Count > 0) {
                obj = mCaches.Dequeue();
            } else {
                obj = Instantiate<InputDialog>(I.m_dialogPrefab, I.m_dialogParent);
                obj.Event_OnHide += () => RecycleDialog(obj);
            }
            obj.gameObject.name = "Actived";
            return obj;
        }

        private static void RecycleDialog(InputDialog ui) {
            if (mCaches.Count > I.m_cacheCount) {
                Destroy(ui.gameObject);
            } else {
                ui.gameObject.name = "Cached";
                mCaches.Enqueue(ui);
            }
        }

        public static void Show(InputBoxArgument args, Action<InputResult> callback) {
            var dialog = CreateDialog();
            dialog.Show(args, callback);
        }
    }
}