using BW.GameCode.UI;

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageBoxTest : MonoBehaviour
{
    [SerializeField] MessageBox msgBox;


    private void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            msgBox.Show("BIAO TI ", "NEI RONG WEN BEN ", MessageBoxButton.Yes, oNmESSAGEBOXclICKED);
        }
        if (Input.GetKeyDown(KeyCode.B)) {
            msgBox.Show("BIAO TI ", "NEI RONG WEN BEN ", MessageBoxButton.Yes| MessageBoxButton.No, oNmESSAGEBOXclICKED);
        }
    }

    private void oNmESSAGEBOXclICKED(MessageBoxButton clickButton) {
        Debug.Log(clickButton.ToString());
    }
}
