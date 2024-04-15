using System.Collections;

using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
namespace BW.GameCode.UI
{
    public class AchievementUI : BaseUIPage
    {
        [SerializeField] Button m_closeButton;
        [SerializeField] Text myText;

        int helloTime = 0;
        protected override void BindUIEvent() {
            m_closeButton.onClick.AddListener(Close);
        }


        protected override IEnumerator DoPlayShowAnimation() {
           yield return transform.DOScale(1, 2f).From(0).WaitForCompletion();
        }

        protected override IEnumerator DoPlayHideAnimation() {
            yield return transform.DOScale(0, 2f).From(1).WaitForCompletion();
        }

    }

}
