using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace BW.GameCode.UI
{
    [System.Flags]
    public enum MessageBoxButton
    {
        Invalid = 0,
        No = 1,
        Yes = 2,
        Cancel = 4,
    }

    /// <summary>
    /// 需要玩家操作的,强烈的警告意味
    /// </summary>
    public class MessageBox : MonoBehaviour
    {
        static string msgboxWindowPrefab = "";
        static string msgboxCanvasObject = "";

        public static MessageBox Instance { get; private set; }

        [SerializeField] MessageBoxDialog m_dialogPrefab;
        [SerializeField] Transform m_dialogParent;

        [SerializeField] int m_cacheCount = 5;

        Queue<MessageBoxDialog> mCaches = new Queue<MessageBoxDialog>();

        MessageBoxDialog CreateDialog() {
            MessageBoxDialog obj;
            if (mCaches.Count > 0) {
                obj = mCaches.Dequeue();
            } else {
                obj = Instantiate<MessageBoxDialog>(m_dialogPrefab, m_dialogParent);
                obj.Event_OnHide += () => RecycleDialog(obj);
            }
            obj.gameObject.name = "Actived";
            return obj;
        }

        private void RecycleDialog(MessageBoxDialog msgbox) {
            if (mCaches.Count > m_cacheCount) {
                Destroy(msgbox.gameObject);
            } else {
                msgbox.gameObject.name = "Cached";
                mCaches.Enqueue(msgbox);
            }
        }

   

        public void Show(string title, string content, MessageBoxButton btnType = MessageBoxButton.Yes,
            MessageBoxClickDelegate callback = default) {
            var dialog = CreateDialog();
            dialog.Show(content, title, btnType, callback);
            
        }

    }
}