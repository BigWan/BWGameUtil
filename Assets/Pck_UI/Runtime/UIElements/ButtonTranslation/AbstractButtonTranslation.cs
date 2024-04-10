using UnityEngine;

namespace BW.GameCode.UI
{
    public abstract class AbstractButtonTranslation : MonoBehaviour
    {
        public abstract void OnStateChanged(BWButton.ButtonState state);
    }
}