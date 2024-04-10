
using UnityEngine;
using UnityEngine.UI;

namespace BW.GameCode.UI
{
    public sealed class ToggleTranslation_SpriteSwap : AbstractToggleTranslation
    {
        [SerializeField] Image m_image;
        [SerializeField] Sprite m_onImage;
        [SerializeField] Sprite m_offImage;

        public override void OnToggleChanged(bool isOn) {
            m_image.sprite = isOn ? m_onImage : m_offImage;
        }
    }

   
}