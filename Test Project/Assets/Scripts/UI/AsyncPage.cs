using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

namespace BW.Core.UI {

    /// <summary>
    /// 异步窗口
    /// </summary>
    public abstract class AsyncPage : MonoBehaviour, IPage {

        public UnityEvent onShow;
        public UnityEvent onHide;

        protected Canvas canvas;

        protected virtual void Awake() {
            canvas = GetComponent<Canvas>();
        }

        public virtual void Hide() {
            StartCoroutine(HideCoroutine());
            onHide?.Invoke();
        }

        public virtual void Show() {
            StartCoroutine(ShowCoroutine());
            onShow?.Invoke();   
        }
        
        public virtual void Close() {
            onShow.RemoveAllListeners();
            onHide.RemoveAllListeners();
            Destroy(gameObject);
        }

        protected virtual IEnumerator HideCoroutine() {
            yield return null;
            if (canvas != null) {
                canvas.enabled = true;
            } else {
                gameObject.SetActive(true);
            }
        }
        protected virtual IEnumerator ShowCoroutine() {
            yield return null;
            if (canvas != null) {
                canvas.enabled = true;
            } else {
                gameObject.SetActive(true);
            }
        }



    }
}