using UnityEngine;

using static BW.GameCode.UI.SelectableAnimationController;

namespace BW.GameCode.UI
{
    /// <summary>
    /// 基本UI元素的动画
    /// </summary>
    public abstract class SelectableTransition : MonoBehaviour
    {
        internal abstract void DoStateTransition(SelectableState state, bool instant);
    }
}