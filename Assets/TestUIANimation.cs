using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BW.GameCode.UI;
public class TestUIANimation : MonoBehaviour
{
    [SerializeField] UIAnimationController c;
    // Start is called before the first frame update
    void Start()
    {
        c
            .InitAnimations();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) {
            c.Play(0);
        }
    }
}
