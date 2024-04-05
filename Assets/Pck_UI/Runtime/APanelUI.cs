using System.Collections.Generic;

using UnityEngine;

namespace BW.GameCode.UI

{
    /// <summary>
    /// 全屏UI
    /// UI 生命周期
    /// Awake => Active => Show => Close=> Deactive
    /// </summary>
    [DisallowMultipleComponent]
    public abstract class APanelUI : BaseUI
    {
        [Header("是否独立UI")]
        [Tooltip("是否独立UI,独立UI的开关不会影响到其他UI,非独立UI只能同时存在一个,在打开的同时会关闭其他非独立UI")]
        [SerializeField] bool m_isStandalone;

        public bool IsMainUI => m_isStandalone;

        IUIManager mUIManager;

        public void RegUI(IUIManager uimanager) {
            Debug.Log($"UIManager Reg UI <{this.name}>");
            mUIManager = uimanager;
        }

        // 通知管理器处理独立UI
        protected override void OnActive() {
            if (mUIManager != null) {
                mUIManager.OnUIActive(this);
            }
        }

        protected override void OnDeactive() {
            if (mUIManager != null) {
                mUIManager.OnUIDeactive(this);
            }
        }
    }
}