using UnityEngine;

namespace BW.GameCode.UI
{
    public abstract class UICanvasLayerManager : MonoBehaviour
    {
        public abstract Transform GetUILayer(int uiLayer);
        public abstract bool IsPanelLayer(int uiLayer);
    }
}