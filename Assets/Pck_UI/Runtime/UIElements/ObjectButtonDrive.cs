namespace BW.GameCode.UI
{

    public sealed class ObjectButtonDrive : BWButtonEventDrive
    {
        private void OnMouseUpAsButton() {
            if (GUIHelper.IsPointOnUI()) return;
            RaisClickEvent();
            //Click();
        }

        private void OnMouseEnter() {
            if (GUIHelper.IsPointOnUI()) return;
            RaisePointInSideEvent(true);
            //IsPointInside = true;
        }

        private void OnMouseExit() {
            RaisePointInSideEvent(false);
            //IsPointInside = false;
        }

        private void OnMouseDown() {
            if (GUIHelper.IsPointOnUI()) return;
            RaisePointDownEvent(true);
            //IsPointDown = true;
        }

        private void OnMouseUp() {
            RaisePointDownEvent(false);
            //IsPointDown = false;
        }
    }
}