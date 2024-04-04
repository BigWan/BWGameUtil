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
        if (Input.GetKeyDown(KeyCode.A)) {
            ShowAMessagebox(MessageBoxButton.Yes);
        }
        if (Input.GetKeyDown(KeyCode.B)) {
            ShowAMessagebox(MessageBoxButton.No);
        }
        if (Input.GetKeyDown(KeyCode.C)) {
            ShowAMessagebox(MessageBoxButton.Yes | MessageBoxButton.No);
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            ShowInputbox();
        }
    }


    async void ShowAMessagebox(MessageBoxButton type) {

        var msg = await MessageBox.I.Show("请选择一个选项", "adfadfasdfasdfasdf", type);
        Debug.Log(msg.ToString());

    }


    async void ShowInputbox() {
        var result = await InputBox.Show("cesi", "ceshi", "input your name", 30, true);
        Debug.Log(result.InputValue);
    }
}
