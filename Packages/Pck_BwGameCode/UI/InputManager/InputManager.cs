using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace BW.GameCode.UI
{
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

        public void Show(InputBoxArgument args, Action<InputResult> callback) {
            var dialog = CreateDialog();
            dialog.Show(args, callback);
        }
    }
}