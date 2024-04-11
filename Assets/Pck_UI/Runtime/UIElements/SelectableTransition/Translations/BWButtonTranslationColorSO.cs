using UnityEngine;

namespace BW.GameCode.UI
{
    /// <summary>
    /// 按钮的颜色过度配置文件
    /// </summary>
    [CreateAssetMenu(fileName ="Selectable Translation Color Config")]
    public class BWButtonTranslationColorSO : ScriptableObject
    {
        [SerializeField] Color m_commonColor = Color.white;
        [SerializeField] Color m_hoverColor = Color.white;
        [SerializeField] Color m_pressedColor = Color.white;
        [SerializeField] Color m_disableColor = Color.white;


        public Color CommonColor => m_commonColor;
        public Color HoverColor => m_hoverColor;
        public Color PressColor => m_pressedColor;
        public Color DisableColor => m_disableColor;

    }
}