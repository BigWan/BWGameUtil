using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BW.Core;

public class TestPool : MonoBehaviour {

    public Poolable abc;
    Poolable a;
    private void OnGUI() {
        if(GUI.Button(new Rect(30, 30, 100, 100), "adfadf")) {
           a =  PoolManagerS.instance.GetPoolable(abc);
            a.transform.SetParent(transform);
            
        }
        if (GUI.Button(new Rect(220, 220, 100, 100), "adfadf2")) {
            a.TryDestroy();
        }
    }

}
