using System;

using UnityEngine;

namespace BW.GameCode.UI
{
    /// <summary>
    /// 按钮的驱动器
    /// 
    /// </summary>
    public abstract class BWButtonEventDrive : MonoBehaviour
    {
        public event Action<bool> Event_PointInSide;
        public event Action<bool> Event_PointDown;
        public event Action Event_OnClick;

        protected void RaisePointInSideEvent(bool inSide) {
            Event_PointInSide?.Invoke(inSide);
        }

        protected void RaisePointDownEvent(bool isDown) {
            Event_PointDown?.Invoke(isDown);
        }

        protected void RaisClickEvent() {
            Event_OnClick?.Invoke();
        }
    }
}