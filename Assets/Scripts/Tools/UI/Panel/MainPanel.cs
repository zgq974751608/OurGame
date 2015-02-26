using UnityEngine;
using System.Collections;
using AiWanNet;
using AiWanNet.Core;

public class MainPanel : BaseButton
{
    private static int isFirst = 0;

	// Use this for initialization
	void Start () 
    {
        Util.CloseDialog(DialogType.Character);
//         AWConnection.awnetgate.AddEventListener(AWEvent.S2C_INTOMAINWORLD, OnIntoMainWorld);
//         AWConnection.awnetgate.AddEventListener(AWEvent.S2C_MYHERO, OnMyHero);
	}

    public void OnClick()
    {
        io.panelManager.CreatePanel(DialogType.World);
       // OnMainButton();
    }

//     void OnMyHero(BaseEvent evt)
//     {
//         ArrangementPanel.spritename.Clear();
//         clientmsg.s2c_myhero myhero = (clientmsg.s2c_myhero)evt.Params["protomsg"];
//         for (int i = 0; i < myhero.herolist.Count; i++)
//         {
//             ArrangementPanel.spritename.Add(int.Parse(myhero.herolist[i].id.ToString()), myhero.herolist[i].icon);
//         }
//     }

//     private void OnMainButton()
//     {
//         clientmsg.c2s_intomainworld intomainworld = new clientmsg.c2s_intomainworld();
//         intomainworld.uaid = Global.uid;
//         AWConnection.instance.SendProtoBufMsg(intomainworld, AWConnection.awnetgate);
//     }

//     private void OnIntoMainWorld(BaseEvent evt)
//     {
//         clientmsg.s2c_intomainworld intomainworld = (clientmsg.s2c_intomainworld)evt.Params["protomsg"]; ;
//         isFirst = isFirst + 1;
//         Debug.LogWarning("OnIntoMainWorld  .. " + intomainworld.result);
// 
//         if (intomainworld.result.ToString() == "intox_success" && isFirst == 1)
//         {
//             io.panelManager.CreatePanel(DialogType.World);
//             
//         }
//         else
//         {
//             Debug.LogWarning("Main ......   Failed");
//             return;
//         }
//     }
	// Update is called once per frame
	void Update () {
	
	}

    public void OnSummonClick()
    {
        io.panelManager.CreatePanel(DialogType.Summon);
    } 
}
