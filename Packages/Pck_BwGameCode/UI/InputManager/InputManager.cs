using BW.GameCode.Foundation;

using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace BW.GameCode.UI
{
    /// <summary>
    /// 输入框
    /// </summary>
    public class InputManager : SimpleSingleton<InputManager>
    {
        
        [SerializeField] InputWindow m_dialogPrefab;
        [SerializeField] Transform m_dialogParent;

        [SerializeField] int m_cacheCount = 5;

        static Queue<InputWindow> mCaches = new Queue<InputWindow>();

        static InputWindow CreateDialog() {
            InputWindow obj;
            if (mCaches.Count > 0) {
                obj = mCaches.Dequeue();
            } else {
                obj = Instantiate<InputWindow>(I.m_dialogPrefab, I.m_dialogParent);
                obj.Event_OnHide += () => RecycleDialog(obj);
            }
            obj.gameObject.name = "Actived";
            return obj;
        }

        private static void RecycleDialog(InputWindow ui) {
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