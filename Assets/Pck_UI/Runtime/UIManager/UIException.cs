using System;

namespace BW.GameCode.UI
{
    public class UIException : Exception
    {
        public UIException(string msg) : base(msg) {
        }
    }
}