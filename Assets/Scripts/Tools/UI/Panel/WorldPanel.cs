using UnityEngine;
using System.Collections;
using AiWanNet;
using AiWanNet.Core;

public class WorldPanel : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Util.CloseDialog(DialogType.Main);
       // GameObject.Find("Camera/MainPanel").SetActive(false);
      //  AWConnection.awnetgate.AddEventListener(AWEvent.S2C_INTODUNGEON, OnIntoDungeon);
	}

    public void OnClick()
    {
        io.panelManager.CreatePanel(DialogType.Duplicate);
        //OnWorldButton();
    }

//     private void OnWorldButton()
//     {
//         clientmsg.c2s_intodungeon intodungeon = new clientmsg.c2s_intodungeon();
//         intodungeon.uaid = Global.uid;
//         AWConnection.instance.SendProtoBufMsg(intodungeon, AWConnection.awnetgate);
//     }
// 
//     private void OnIntoDungeon(BaseEvent evt)
//     {
//         clientmsg.s2c_intodungeon intodungeon = (clientmsg.s2c_intodungeon)evt.Params["protomsg"]; 
//        
//         if (intodungeon.result.ToString() == "intox_success")
//         {
//             
//             io.panelManager.CreatePanel(DialogType.Duplicate);
//         }
//         else
//         {
//             Debug.LogWarning("Dungeon ......   Failed");
//             return;
//         }
//     }

    public void OnClose()
    {
        io.panelManager.CreatePanel(DialogType.Main);
        Util.CloseDialog(DialogType.World);
    }

	// Update is called once per frame
	void Update () {
	
	}
}
