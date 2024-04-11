using UnityEngine;
using UnityEngine.UI;

namespace BW.GameCode.UI
{

    /// <summary>
    /// 颜色变换
    /// </summary>
    public class ST_Image : ST_Color
    {
        [SerializeField] STValue_Color m_color;
        [SerializeField] Graphic m_image = default;
        protected override void SetColor(Color color,float time, bool instant) {
            if (m_image == null) return;
            m_image.CrossFadeColor(color, time, true, true);
        }

        private void OnValidate() {
            if (m_image == null) {
                m_image = GetComponent<Image>();
            }
            //OnStateChanged( AbstractToggle.AnimState.Normal);
        }
    }
}