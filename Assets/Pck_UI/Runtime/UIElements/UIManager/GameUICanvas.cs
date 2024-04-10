
using UnityEngine;

namespace BW.GameCode.UI
{
    public class GameUICanvas : MonoBehaviour
    {
        [SerializeField] Transform m_sceneLayer;
        [SerializeField] Transform m_panelLayer;
        [SerializeField] Transform m_popupLayer;
        [SerializeField] Transform m_topLayer;

        public Transform GetLayer(UIType uiType) {
            switch (uiType) {
                case UIType.SceneUI: return m_sceneLayer;
                case UIType.Panel:return m_panelLayer;
                case UIType.Popup: return m_popupLayer;
                case UIType.Top: return m_topLayer;
                default:
                    return this.transform;
            }
        }
    }
}