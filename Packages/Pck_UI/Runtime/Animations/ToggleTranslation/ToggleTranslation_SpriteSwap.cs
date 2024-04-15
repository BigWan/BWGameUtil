using UnityEngine;
using UnityEngine.UI;

namespace BW.GameCode.UI
{
    

    public sealed class ToggleTranslation_SpriteSwap : ToggleTranslation
    {
        [SerializeField] Image m_image;
        [SerializeField] ToggleData_Sprite m_spriteGroup;

        protected override void DOTranslation(bool isOn) {
            if (m_image != null) {
                m_image.sprite = m_spriteGroup.GetValue(isOn);
            }
        }
    }
}