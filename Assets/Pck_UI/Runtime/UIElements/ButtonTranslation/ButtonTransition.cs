using UnityEngine;

namespace BW.GameCode.UI
{
    using static BW.GameCode.UI.BWButton;

    public abstract class ButtonTransition : MonoBehaviour
    {
        internal abstract void DoStateTransition(ButtonState state, bool instant);
    }
}