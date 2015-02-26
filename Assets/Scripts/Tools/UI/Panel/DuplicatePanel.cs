using UnityEngine;
using System.Collections;
using AiWanNet;
using AiWanNet.Core;

public class DuplicatePanel : MonoBehaviour
{

	// Use this for initialization
	void Start () 
    {
        Util.CloseDialog(DialogType.World);
       // GameObject.Find("Camera/WorldPanel").SetActive(false);
       // AWConnection.awnetgate.AddEventListener(AWEvent.S2C_INTOLEVELS, OnIntoLevels);
	}

    public void OnClick()
    {
        io.panelManager.CreatePanel(DialogType.Arrangement);
        //OnDuplicateButton();
    }

    private void OnDuplicateButton()
    {
        clientmsg.c2s_intolevels intolevels = new clientmsg.c2s_intolevels();
        intolevels.uaid = Global.uid;
        AWConnection.instance.SendProtoBufMsg(intolevels, AWConnection.awnetgate);
    }

    public void OnClose()
    {
        io.panelManager.CreatePanel(DialogType.World);
        Util.CloseDialog(DialogType.Duplicate);
        Util.SetBackground("MainUI/MainGround");
    }

//     private void OnIntoLevels(BaseEvent evt)
//     {
//         clientmsg.s2c_intolevels intolevels = (clientmsg.s2c_intolevels)evt.Params["protomsg"];
// 
//         Debug.LogWarning("IntoLevels.....   " + intolevels.result);
// 
//         if(intolevels.result.ToString() == "intox_success")
//         {
//             io.panelManager.CreatePanel(DialogType.Arrangement);
//             //io.panelManager.CreatePanel(DialogType.Fight);
//            // Application.LoadLevelAsync("Fight");
//            // LoadScene.LoadDungeon(10101);
//         }
//         else
//         {
//             Debug.LogWarning("IntoLevels......   Failed");
//             return;
//         }
//     }
}
