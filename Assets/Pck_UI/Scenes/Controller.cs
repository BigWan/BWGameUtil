using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BW.GameCode.UI;
public class Controller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a")) {
            ShowAMessagebox();
        }
        
    }


    void ShowAMessagebox() {

        var msg = MessageBox.I.Show("��ѡ��һ��ѡ��", "adfadfasdfasdfasdf", MessageBoxButtonStyle.Yes);
        Debug.Log(msg);

    }
}
