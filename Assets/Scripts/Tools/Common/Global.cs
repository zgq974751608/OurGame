using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using clientmsg;

public class Global : MonoBehaviour {

    public static DialogType CurrDialogType = DialogType.None;
    public static SubDialogType CommDlgType = SubDialogType.None;

    public static bool Paused = false;
    public static string uiname = "login";
    public static string ipString = "192.168.1.17";
    public static int port = 30100;

    public static uint uid;
    //昵称
    public static string userName = "";//nickname
    public static string userInputName;//test1234
    public static string password = "123456"; //

}
