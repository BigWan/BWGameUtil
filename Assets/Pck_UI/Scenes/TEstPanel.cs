using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BW.GameCode.UI;

using Cysharp.Threading.Tasks;

using UnityEngine;
using DG.Tweening;
using System.Threading;

public class TEstPanel : BaseUI
{
    [SerializeField] CanvasGroup m_fadeCanvs;
    protected override async UniTask PlayShowAnimation(CancellationToken ct) {
        await m_fadeCanvs.DOFade(1, 20f).ToUniTask( TweenCancelBehaviour.Kill,ct);
    }
    protected override async UniTask PlayHideAnimation(CancellationToken ct) {
        await m_fadeCanvs.DOFade(0, 20f).ToUniTask(TweenCancelBehaviour.Kill, ct);
    }
}
