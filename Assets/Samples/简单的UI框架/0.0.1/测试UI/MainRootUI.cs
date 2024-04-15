namespace BW.GameCode.UI
{
    using DG.Tweening;

    using System;
    using System.Collections;
    using System.Linq;

    using UnityEngine;
    using UnityEngine.UI;
    /// <summary>
    /// 成就UI
    /// </summary>
    public class MainRootUI : BaseUIPage
    {
        [SerializeField] Button m_CloseButton;
        [SerializeField] Button m_CreateNewGameButton;
        [SerializeField] Button m_ShowAchGameButton;

        [SerializeField] Text myText;

        int helloTime = 0;
        protected override void BindUIEvent() {
            m_CloseButton.onClick.AddListener(Close);
            m_CreateNewGameButton.onClick.AddListener(CreateNewGame);
            m_ShowAchGameButton.onClick.AddListener(() => UIManager.I.Show<AchievementUI>());
        }

        private void CreateNewGame() {
            UIManager.I.Show<CreatePanel>();
        }
        protected override IEnumerator DoPlayShowAnimation() {
            yield return transform.DOScale(1, 2f).From(0).WaitForCompletion();
        }

        protected override IEnumerator DoPlayHideAnimation() {
            Debug.Log("Strt player HIde");
            yield return transform.DOScale(0, 2f).WaitForCompletion();
        }


    }

}

