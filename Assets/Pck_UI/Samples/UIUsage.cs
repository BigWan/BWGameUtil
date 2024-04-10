
namespace BW.GameCode.UI
{
    using UnityEngine;

    public class UIUsage : MonoBehaviour
    {
        private void Update() {
            if (Input.GetKeyDown(KeyCode.A)) {
                UIManager.I.Show<MainRootUI>();
            }
        }
    }
}