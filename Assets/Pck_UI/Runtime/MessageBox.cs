using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using BW.GameCode.Singleton;
using Cysharp.Threading.Tasks;

namespace BW.GameCode.UI
{

    public class MessageBox : SimpleSingleton<MessageBox>
    {
        [SerializeField] BaseMessageBoxWindow m_msgBoxDialog;
        [SerializeField] Transform m_dialogGroup;

        [SerializeField] int m_cacheCount = 5;

        Queue<BaseMessageBoxWindow> mCaches = new Queue<BaseMessageBoxWindow>();



        BaseMessageBoxWindow CreateDialog() {
            BaseMessageBoxWindow obj;
            if (mCaches.Count > 0) {
                obj = mCaches.Dequeue();
            } else {
                obj = Instantiate<BaseMessageBoxWindow>(m_msgBoxDialog, m_dialogGroup);
                //obj.Event_OnClose += () => OnDialogDeactive(obj);
                //obj.Callback_OnDeactive.AddListener(()=>OnDialogDeactive(obj));rtff
            }
            obj.gameObject.name = "Actived";
            return obj;
        }

        private void RecycleDialog(BaseMessageBoxWindow ui) {
            if (mCaches.Count > m_cacheCount) {
                Destroy(ui.gameObject);
            } else {
                ui.gameObject.name = "Cached";
                mCaches.Enqueue(ui);
            }
        }

        public async UniTask<MessageBoxButton> Show(string title, string content, MessageBoxButtonStyle btnType = MessageBoxButtonStyle.Yes) {
            var dialog = CreateDialog();
            var result = await dialog.Show(content, title, btnType);            
            RecycleDialog(dialog);
            return result;
        }
    }
}