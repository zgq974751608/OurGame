using UnityEngine;
using System.Collections;
using AiWanNet;
using AiWanNet.Core;

public class LoginPanel : MonoBehaviour {

    public static bool isCreateChararcter = false;

    // Use this for initialization
    void Start()
    {

    }

    public void OnClick()
    {  
        string name = this.gameObject.name;
        switch (name)
        {
            case "LoginPanel":
                this.OnLogin();
                break;
        }
    }

    private void OnLogin()
    {
        GameObject gameObject = Util.Peer(base.gameObject, "LoginPanel/bottom/UserName");
        GameObject gameObject2 = Util.Peer(base.gameObject, "LoginPanel/bottom/PassWord");

        //Global.userInputName = gameObject.GetComponent<UIInput>().value;
       // Global.password = gameObject2.GetComponent<UIInput>().value;
        Global.userInputName = "test" + Util.Random(1, 500).ToString();

        Global.password = "123456";
        if (string.IsNullOrEmpty(Global.userInputName) || string.IsNullOrEmpty(Global.password))
        {
            return;
        }

        AWConnection.instance.Login_Server();

        Util.CloseDialog(DialogType.Login);

        if (LoginPanel.isCreateChararcter)
            io.panelManager.CreatePanel(DialogType.Character);
        else
            io.panelManager.CreatePanel(DialogType.Main);
    }
}
