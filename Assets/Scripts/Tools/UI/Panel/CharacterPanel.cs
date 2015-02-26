using UnityEngine;
using System.Collections;
using AiWanNet;
using AiWanNet.Core;

public class CharacterPanel : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        //AWConnection.awnetgate.AddEventListener(AWEvent.S2C_CREATECHAR, OnCreateChar);
    }
	

    public void OnClick()
    {
        //OnCharacterButton();
        io.panelManager.CreatePanel(DialogType.Main);
    }

    private void OnCharacterButton()
    {
//         clientmsg.c2s_createchar createchar = new clientmsg.c2s_createchar();
//         createchar.uaid = Global.uid;
// //        Debug.LogError("createcharuid...... " + Global.uid);
//         createchar.newchar = new common.msgnewchar();
//         createchar.newchar.uaid = Global.uid;
//         createchar.newchar.headimage = "aiwan";
//         createchar.newchar.name = "pic";
// 
//         AWConnection.instance.SendProtoBufMsg(createchar, AWConnection.awnetgate);
    }

//     private void OnCreateChar(BaseEvent evt)
//     {
//         clientmsg.s2c_createchar createchar = (clientmsg.s2c_createchar)evt.Params["protomsg"];
//         Debug.LogWarning("OnCreateChar   " + createchar.result);
//         if (createchar.result.ToString() == "createchar_success")
//         {
//             io.panelManager.CreatePanel(DialogType.Main);
//         }
//         else
//         {
//             Debug.LogWarning("CreateChar ......   Failed");
//             return;
//         }
//     }

}
