using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BW.Core.UI {

    /// <summary>
    /// 页面类(一个窗口管理多个页面,负责页面之间的回退和交互)
    /// </summary>
    public abstract class Page : MonoBehaviour , IPage{

        protected Canvas canvas;

        protected virtual void Awake() {
            canvas = GetComponent<Canvas>();
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
        }

        public virtual void Close() {
            Destroy(gameObject);
        }

    }

}
