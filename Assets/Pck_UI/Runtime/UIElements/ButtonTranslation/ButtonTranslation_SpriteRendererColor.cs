using DG.Tweening;

using UnityEngine;

namespace BW.GameCode.UI
{
    /// <summary>
    /// 颜色变换
    /// </summary>
    public class ButtonTranslation_SpriteRendererColor : ButtonTranslation_Color
    {
        [SerializeField] SpriteRenderer m_renderer = default;

        protected override void SetColor(Color color, float time, bool instant) {
            Debug.Assert(m_renderer != null, this.transform);
            m_renderer.DOKill();
            m_renderer.DOColor(color, time);
        }

        private void OnValidate() {
            if (m_renderer == null) {
                m_renderer = GetComponent<SpriteRenderer>();
            }
        }
    }
}