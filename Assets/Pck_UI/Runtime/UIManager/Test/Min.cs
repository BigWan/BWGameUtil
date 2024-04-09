using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BW.GameCode.UI
{
    /// <summary>
    /// 场景主UI
    /// </summary>
    public class MainUI:BaseUI
    {



    }


    /// <summary>
    /// 成就UI
    /// </summary>
    public class AchPanel : BaseUI
    {

    }

    public class CreatePanel : BaseUI
    {

    }


    public class UIUsage : UnityEngine.MonoBehaviour
    {
        private void Start() {
            UIManager.I.Show<AchPanel>();
        }
    }

}
