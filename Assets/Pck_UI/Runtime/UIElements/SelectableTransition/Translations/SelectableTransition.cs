using UnityEngine;

using static BW.GameCode.UI.SelectableAnimationController;

namespace BW.GameCode.UI
{

    public abstract class SelectableTransition : MonoBehaviour
    {
        internal abstract void DoStateTransition(SelectableState state, bool instant);
    }
}