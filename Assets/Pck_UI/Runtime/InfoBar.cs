using DG.Tweening;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using BW.GameCode.Singleton;
namespace BW.GameCode.UI
{
    /// <summary>
    /// 游戏消息栏
    /// </summary>
    public class InfoBar : SimpleSingleton<InfoBar>
    {
        [SerializeField] InfoLine m_infoBoxPrefab;
        [SerializeField] int m_maxLines = 8;
        [SerializeField] Transform m_lineParent;

        [SerializeField] Color m_infoColor;
        [SerializeField] Color m_warningColor;

        [SerializeField] float m_startPosY = -300f;
        readonly Queue<InfoLine> caches = new Queue<InfoLine>();
        readonly List<InfoLine> actives = new List<InfoLine>();

        RectTransform rect;

        protected override void OnAwake() {
            base.OnAwake();
            rect = transform as RectTransform;
            Debug.Assert(rect != null);
        }

        InfoLine GetLine() {
            InfoLine result;
            if (caches.Count > 0) {
                result = caches.Dequeue();
            } else {
                result = Instantiate<InfoLine>(m_infoBoxPrefab, m_lineParent);
                result.Event_OnDisappear += () => RecycleLine(result);
            }
            result.transform.SetAsLastSibling();
            result.Rect.anchoredPosition = new Vector2(0, m_startPosY);// = Vector3.zero;
            //result.gameObject.SetActive(true);
            result.gameObject.name = "Actived";
            return result;
        }

        public void ShowInfo(string info) {
            ShowInfo(info, m_infoColor);
        }

        public void ShowWarning(string info) {
            ShowInfo(info, m_warningColor);
        }

        public void ShowInfo(string info, Color color) {
            if (actives.Count >= m_maxLines) {
                var item = actives[0];
                item.DisappearImmediately();
                //RecycleLine(item);
            }
            var line = GetLine();
            line.Show(info, color);
            actives.Add(line);
            //line.gameObject.name = actives.Count.ToString();

            if (co != null) {
                StopCoroutine(co);
            }
            co = StartCoroutine(UpdateLine());
        }

        Coroutine co;

        [ContextMenu("Update Lines")]
        IEnumerator UpdateLine() {
            if (actives.Count == 0) yield break;
            yield return null;
            float curHeight = m_startPosY;
            for (int i = actives.Count - 1; i >= 0; i--) {
                actives[i].Rect.DOKill();
                //actives[i].Rect.dop.(curHeight, 0.3f);
                //Debug.Log(curHeight);
                curHeight += actives[i].Height;
            }
        }

        void RecycleLine(InfoLine line) {
            actives.Remove(line);
            caches.Enqueue(line);
            line.gameObject.name = "[Cached]";
        }

        //IEnumerator DisplayInfo(string info) {
        //    var ui = GetInfoBox();
        //    ui.SetText(info);
        //    ui.Show();
        //    yield return new WaitForSeconds(1f);
        //    ui.Close();
        //}
    }
}