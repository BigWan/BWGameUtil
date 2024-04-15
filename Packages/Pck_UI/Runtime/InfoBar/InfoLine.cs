using System;
using System.Collections;

using TMPro;

using UnityEngine;

namespace BW.GameCode.UI
{

    public class InfoLine : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI m_content;
        [SerializeField] CanvasGroup m_cg;
        [SerializeField] float m_infoDuration = 2f;

        public bool IsBusy { get; private set; }
        public RectTransform Rect { get; private set; }
        public string Text { get => m_content.text; set => m_content.text = value; }
        public Color Color { get => m_content.color; set => m_content.color = value; }

        public float Height => Rect.sizeDelta.y;

        public event Action Event_OnDisappear;

        WaitForSeconds infoDurationWaiter;

        private void Awake() {
            Rect = transform as RectTransform;
            infoDurationWaiter = new WaitForSeconds(m_infoDuration);
        }

        public void Show(string text) {
            if(IsBusy) {
                throw new System.Exception("该弹出被占用,无法再次启动");
            }
            Show(text, Color.white);
        }

        public void Show(string text, Color color) {
            Text = text;
            Color = color;
            StartCoroutine(ShowCoroutine());
        }

        public void DisappearImmediately() {
            m_cg.alpha = 0;
            IsBusy = false;
            Event_OnDisappear?.Invoke();
        }

        IEnumerator ShowCoroutine() {
            IsBusy = true;
            m_cg.alpha = 0;
            float alphaStep = 1f;
            while (m_cg.alpha<=1) {
                m_cg.alpha += alphaStep * Time.deltaTime; ;
            }
            m_cg.alpha = 1;
            yield return infoDurationWaiter;
            while (m_cg.alpha>=0) {
                m_cg.alpha -= alphaStep * Time.deltaTime;
            }

            DisappearImmediately();
        }
    }
}