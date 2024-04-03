using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using BW.GameCode.Singleton;
using Cysharp.Threading.Tasks;

namespace BW.GameCode.UI
{
    /// <summary>
    /// 需要玩家操作的,强烈的警告意味
    /// </summary>
    public class MessageBox : SimpleSingleton<MessageBox>
    {
        [SerializeField] BaseMessageBoxDialog m_dialogPrefab;
        [SerializeField] Transform m_dialogParent;

        [SerializeField] int m_cacheCount = 5;

        Queue<BaseMessageBoxDialog> mCaches = new Queue<BaseMessageBoxDialog>();

        protected override void OnAwake() {
            base.OnAwake();
        }

        BaseMessageBoxDialog CreateDialog() {
            BaseMessageBoxDialog obj;
            if (mCaches.Count > 0) {
                obj = mCaches.Dequeue();
            } else {
                obj = Instantiate<BaseMessageBoxDialog>(m_dialogPrefab, m_dialogParent);
                obj.Event_OnClose += () => OnDialogDeactive(obj);
                //obj.Callback_OnDeactive.AddListener(()=>OnDialogDeactive(obj));rtff
            }
            obj.gameObject.name = "Actived";
            return obj;
        }

        private void OnDialogDeactive(BaseMessageBoxDialog ui) {
            if (mCaches.Count > m_cacheCount) {
                Destroy(ui.gameObject);
            } else {
                ui.gameObject.name = "Cached";
                mCaches.Enqueue(ui);
            }
        }

        public async UniTask<MessageBoxButton> Show(string title, string content, MessageBoxButtonType btnType = MessageBoxButtonType.Yes) {
            var dialog = CreateDialog();
            var result = await dialog.Show(content, title, btnType);
            dialog.Close();
            return result;
        }
    }
}