using DG.Tweening;

using System.Collections;

using UnityEngine;
using UnityEngine.UI;

namespace BW.GameCode.UI
{
    public class CreatePanel : BaseUIPage
    {
        [SerializeField] Button myButton;
        [SerializeField] Text myText;

        int helloTime = 0;
        protected override void BindUIEvent() {
            myButton.onClick.AddListener(Close);
        }
        protected override IEnumerator DoPlayShowAnimation() {
            yield return transform.DOScale(1, 2f).From(0).WaitForCompletion();
        }

        protected override IEnumerator DoPlayHideAnimation() {
            yield return transform.DOScale(0, 2f).From(1).WaitForCompletion();
        }

    }

}
