using UnityEngine;

namespace BW.GameCode.UI
{
    public abstract class UICanvasLayerManager : MonoBehaviour
    {
        public abstract Transform GetUILayer(string uiLayer);
        public abstract bool IsPanelLayer(string uiLayer);
    }
}