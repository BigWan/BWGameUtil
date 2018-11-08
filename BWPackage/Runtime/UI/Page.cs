using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace BW.Core.UI {

    /// <summary>
    /// 页面类(一个窗口管理多个页面,负责页面之间的回退和交互)
    /// </summary>
    public abstract class BasePage : MonoBehaviour , IPage{

        protected Canvas canvas;

        public UnityEvent showEvent;

        protected virtual void Awake() {
            canvas = GetComponent<Canvas>();
            showEvent = new UnityEvent();
            showEvent.AddListener(OnShow);
        }

        public virtual void Hide() {
            if (canvas != null)
                canvas.enabled = false;
            else
                gameObject.SetActive(false);
        }

        public virtual void Show() {
            if (canvas != null) {
                canvas.enabled = true;
            } else {
                gameObject.SetActive(true);
            }
            showEvent.Invoke();
        }

        public virtual void Close() {
            Destroy(gameObject);
        }

        public abstract void OnShow();

    }

}
