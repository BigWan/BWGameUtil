using BW.GameCode.Animation;

using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.UI;

public class TestUIANimation : MonoBehaviour
{
    [SerializeField] List<AnimPartController> c;

    [SerializeField] Button m_playOnceButton;
    [SerializeField] Button m_playRestartButton;
    [SerializeField] Button m_playLoopButton;

    // Start is called before the first frame update
    private void Awake() {
        c = GetComponentsInChildren<AnimPartController>().ToList();
        m_playOnceButton.onClick.AddListener(PlayOnce);
        m_playRestartButton.onClick.AddListener(PlayRestart);
        m_playLoopButton.onClick.AddListener(PlayLoop);
    }

    void PlayOnce() {
        foreach (var x in c) {
            x.InitAnimations();
            x.LoopType = AnimtionLoopType.None;
            x.Play();
        }
    }

    void PlayRestart() {
        foreach (var x in c) {
            x.InitAnimations();
            x.LoopType = AnimtionLoopType.Restart;

            x.Play();
        }
    }

    void PlayLoop() {
        foreach (var x in c) {
            x.InitAnimations();
            x.LoopType = AnimtionLoopType.Yoyo;
            x.Play();
        }
    }

    // Update is called once per frame
}