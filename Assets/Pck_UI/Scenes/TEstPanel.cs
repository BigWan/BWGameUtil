using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BW.GameCode.UI;

using Cysharp.Threading.Tasks;

using UnityEngine;
using DG.Tweening;
public class TEstPanel : BaseUI
{
    [SerializeField] CanvasGroup m_fadeCanvs;
    protected override async UniTask PlayShowAnimation() {
        await m_fadeCanvs.DOFade(1, 0.25f);
    }
}
