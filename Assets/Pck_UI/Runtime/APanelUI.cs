using System.Collections.Generic;

using UnityEngine;

namespace BW.GameCode.UI
{

    /// <summary>
    /// 弹窗UI
    /// </summary>
    public abstract class WindowUI : BaseUI
    {

    }

    /// <summary>
    /// 全屏UI
    /// UI 生命周期
    /// Awake => Active => Show => Close=> Deactive
    /// </summary>
    [DisallowMultipleComponent]
    public abstract class APanelUI : BaseUI
    {


    }
}