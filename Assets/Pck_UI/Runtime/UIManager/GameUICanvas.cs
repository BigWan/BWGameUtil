
using UnityEngine;
using UnityEngine.UI;

namespace BW.GameCode.UI
{
    /// <summary>
    /// UI 的Canvas设置
    /// </summary>
    public class GameUICanvas : MonoBehaviour
    {
        [SerializeField] Canvas m_canvas;
        [SerializeField] CanvasScaler m_scaler;
        [SerializeField] Transform m_backLayer;
        [SerializeField] Transform m_panelPageLayer;
        [SerializeField] Transform m_popupLayer;
        [SerializeField] Transform m_topLayer;



        public Transform GetLayer(GameUILayer layer) {
            switch (layer) {
                case GameUILayer.Back: return m_backLayer;
                case GameUILayer.PanelPage: return m_panelPageLayer;
                case GameUILayer.Popup: return m_popupLayer;
                case GameUILayer.Top: return m_topLayer;
                default:
                    return transform;
            }
            
        }
    }
}