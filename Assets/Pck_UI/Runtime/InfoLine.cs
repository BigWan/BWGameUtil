namespace BW.GameCode.UI
{
    using UnityEngine;
    using TMPro;
    using DG.Tweening;
    using System;
    using System.Collections.Generic;
    using System.Collections;

    public class InfoLine : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI m_content;
        [SerializeField] CanvasGroup m_cg;
        [SerializeField] float m_infoDuration = 2f;
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
            Show(text, Color.white);
        }

        public void Show(string text, Color color) {
            Text = text;
            Color = color;
            StartCoroutine(ShowCoroutine());
        }

        public void DisappearImmediately() {
            m_cg.DOKill();
            m_cg.alpha = 0;
            StopAllCoroutines();
            //gameObject.SetActive(false);
            Event_OnDisappear?.Invoke();
        }

        IEnumerator ShowCoroutine() {
            m_cg.DOKill();
            yield return m_cg.DOFade(1, 1f).From(0).WaitForCompletion();
            yield return infoDurationWaiter;
            yield return m_cg.DOFade(0, 1f).WaitForCompletion();
            DisappearImmediately();
        }
    }
}