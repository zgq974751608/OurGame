using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Permissions;
using System.Text;
using AiWanNet;
using AiWanNet.Core;

using AiWanNet.Requests;
using AiWanNet.Logging;

using clientmsg;
using LitJson;


public class AWConnection : MonoBehaviour
{
    
    //----------------------------------------------------------
    // Setup variablesF:\SmartClient\mmo\Assets\\
    //----------------------------------------------------------
    public string serverName = "192.168.1.152";
    public int serverPort = 30100;
    
    public LogLevel logLevel = LogLevel.DEBUG;

    public static AiWan awnet;
    public static AiWan awnetgate;

    public static AWConnection instance;
   
    //----------------------------------------------------------
    // Called when program starts
    //----------------------------------------------------------
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(this);

        // 
        awnet = new AiWan(true);
        awnetgate = new AiWan(true);
        Debug.LogWarning("API Version: " + awnet.Version);

        // Register callback delegate

        awnet.AddEventListener(AWEvent.CONNECTION, OnConnectionLoginServer);
        awnet.AddEventListener(AWEvent.CONNECTION_LOST, OnConnectionLost);
        awnet.AddEventListener(AWEvent.LOGIN, OnLoginLoginServer);
        awnet.AddEventListener(AWEvent.LOGIN_ERROR, OnLoginError);
        awnet.AddLogListener(logLevel, OnDebugMessage);

        awnetgate.AddEventListener(AWEvent.CONNECTION, OnConnectionGateServer);
        
        awnetgate.AddEventListener(AWEvent.RESPONSE_MAILCONTENT, OnS2C_ResponseMailContent);
        awnetgate.AddEventListener(AWEvent.RESPONSE_PROOFTIME, OnS2C_ResponseProofTime);
        awnetgate.AddEventListener(AWEvent.S2C_INITCHAR, OnS2C_InitChar);
        AWConnection.awnetgate.AddEventListener(AWEvent.S2C_FIGHTBEGIN, OnS2C_ResponseFightBegin);

        Application.LoadLevelAsync("Login");
    }

    void OnDestroy()
    {
        awnetgate.Disconnect();
    }
    //----------------------------------------------------------
    // As Unity is not thread safe, we process the queued up callbacks every physics tick
    //----------------------------------------------------------
    void FixedUpdate()
    {
        if (awnet != null)
        {
            awnet.ProcessEvents();
        }
        if (awnetgate != null)
        {
            awnetgate.ProcessEvents();
        }

    }


    //----------------------------------------------------------
    // Handle connection response from server
    //----------------------------------------------------------
    public void OnConnectionLoginServer(BaseEvent evt)
    {
        bool success = (bool)evt.Params["success"];
        if (success)
        {
            LoginRequestMsg();
        }
        else
        {
            Debug.LogWarning("Can't connect to server!");
        }
    }
    public void OnConnectionGateServer(BaseEvent evt)
    {
        UnityEngine.Debug.Log("----call-OnConnectionGateServer-----");
        bool success = (bool)evt.Params["success"];
        //string error = (string)evt.Params["errorMessage"];
        if (success)
        {
            ConnectionGateMsg();
        }
        else
        {
            Debug.LogWarning("Can't connect to server!");
        }

    }


    public void OnConnectionLost(BaseEvent evt)
    {
        // Reset all internal states so we kick back to login screen
        Debug.Log("OnConnectionLost");
        
        Debug.LogWarning("Connection was lost, Reason: " + (string)evt.Params["reason"]);
    }

    public void OnLoginLoginServer(BaseEvent evt)
    {
        //awnet
        clientmsg.s2c_login loginresponse = (clientmsg.s2c_login)evt.Params["protomsg"];
        string gateServerIP = loginresponse.gsip;
        uint gateServerPort = loginresponse.gsport;
        Global.uid = loginresponse.uaid;
        //NGUIDebug.Log("Recv GateInfo IP: " + gateServerIP + "    Recv GateInfo Port: " + gateServerPort);

        switch (loginresponse.loginresult.ToString())
        {
            case "login_success":
                awnet.Disconnect();
                awnetgate.Connect(gateServerIP, (int)gateServerPort);
                break;
            case "login_accounterror":
                Debug.LogError("user and password     error");
                break;
            case "login_fail":
                Debug.LogError("login   fail");
                awnet.Disconnect();
                Login_Server();
                awnetgate.Connect(gateServerIP, (int)gateServerPort);
                break;
        }
        if (loginresponse.loginresult.ToString() == "login_success")
        {
            awnet.Disconnect();
            awnetgate.Connect(gateServerIP, (int)gateServerPort);
        }
        else
        {
            Debug.LogError("username...... error");
        }
    }

    public void OnLoginGate(BaseEvent evtt)
    {
        ConnectionGateMsg();
    }

    public void OnLoginError(BaseEvent evt)
    {
        Debug.Log("Login error: " + (string)evt.Params["errorMessage"]);
    }

    public void OnRoomJoin(BaseEvent evt)
    {
        Debug.Log("Joined room successfully");

        // Room was joined - lets load the game and remove all the listeners from this component
        awnet.RemoveAllEventListeners();
        Application.LoadLevel("Game");

    }

    void OnLogout(BaseEvent evt)
    {
        Debug.Log("OnLogout");
    }

    public void OnDebugMessage(BaseEvent evt)
    {
        string message = (string)evt.Params["message"];
        Debug.Log("[SFS DEBUG] " + message);
    }


    public void SendProtoBufMsg(ProtoBuf.IExtensible msg, AiWan fox)
    {
        fox.SendProtobufMsg(msg);
    }

    void LoginRequestMsg()
    {
        int messageContentLen = 0;
        clientmsg.c2s_login msg = new clientmsg.c2s_login();
       // NGUIDebug.Log("name.....  " + Global.userInputName);

        msg.name = Global.userInputName;
        msg.pwd = Global.password;
        messageContentLen += msg.name.Length;
        messageContentLen += msg.pwd.Length;
        SendProtoBufMsg(msg, awnet);
    }

    void ConnectionGateMsg()
    {
        if (awnetgate.IsConnected)
        {
            clientmsg.c2s_begingame msg = new clientmsg.c2s_begingame();
            msg.uaid = Global.uid;
            SendProtoBufMsg(msg, awnetgate);
        }

    }
    public void OutChangeLevel(clientmsg.ChangeLevel level)
    {
        SendProtoBufMsg(level, awnetgate);
    }

    public void Login_Server()
    {
        //NGUIDebug.Log("    serverName" + serverName + "    serverPort" + serverPort);
        awnet.Connect(serverName, serverPort);
    }

    public void btn_LoginOut()
    {
        awnetgate.Disconnect();
    }

    //测试
    void OnGUI()
    {
//         if (GUI.Button(new Rect(10, 10, 100, 100), "RecNetMsg"))
//          {
//              RecNetMsgData();
//          }
// 
//          if (GUI.Button(new Rect(10, 110, 100, 100), "RequestMsg"))
//          {
//              //OutChangeLevel();
//              RequestNetMsgData();
//          }

    }

//测试网络消息
#region

    void RecNetMsgData()
    {
        clientmsg.UserSendMailByid msg = new clientmsg.UserSendMailByid();
        msg.remoteid = 3;
        string content = "爱玩网络科技有限公司";
        byte[] encodeBytes = System.Text.Encoding.UTF8.GetBytes(content);

        string inputString = System.Text.Encoding.UTF8.GetString(encodeBytes);
        msg.content = inputString;

        msg.type = clientmsg.enumMailType.enumMailType_Sys;
        SendProtoBufMsg(msg, awnetgate);   
    }

    void RequestNetMsgData()
    {
         clientmsg.UserOpenMail msg = new clientmsg.UserOpenMail();
         SendProtoBufMsg(msg, awnetgate);

//         clientmsg.ClientSelectChar msg = new clientmsg.ClientSelectChar();
//         msg.uaid = uid;
//         msg.charid = (uint)1;
// 
//         SendProtoBufMsg(msg, awnetgate);

    }

    void OnS2C_ResponseMailContent(BaseEvent evt)
    {
        Debug.Log("OnS2C_ResponseMailContent");

        clientmsg.SendMailContent responsemailcontent = (clientmsg.SendMailContent)evt.Params["protomsg"];
        int count = responsemailcontent.mail_contents.Count;

        if (count > 0)
        {
            for (int i = 0; i < count; i++ )
            {
           //     NGUIDebug.Log("maild....  " +  responsemailcontent.mail_contents[i].mailid.ToString());
            }
        }

    }

    void OnS2C_ResponseProofTime(BaseEvent evt)
    {
        clientmsg.S2CProofTime prooftime = (clientmsg.S2CProofTime)evt.Params["protomsg"];
        NGUIDebug.Log("prooftime id  " + prooftime.charid + "   prooftime timestamp     " + prooftime.time_stamp);
    }

#endregion


    //接受数据
    void OnS2C_ResponseChangeLevel(BaseEvent evt)
    {
//        clientmsg.ResponseChangeLevel responsechangelevel = (clientmsg.ResponseChangeLevel)evt.Params["protomsg"];
// 
//         JsonData jdpos = JsonMapper.ToObject(responsechangelevel.monsterpos);
//         JsonData jdgroup = JsonMapper.ToObject(responsechangelevel.monstergroup);
// 
//         Debug.Log("jdpos" + jdpos[0]["originx"] + " ..... " + jdpos[0]["originy"]);
        //int x = int.Parse(jdpos[0]["originx"].ToString());
        //int y = int.Parse(jdpos[0]["originy"].ToString());
        //int count = int.Parse(jdpos[0]["quantity"].ToString());

        //for (int i = 0; i < count; i++ )
        //{
        //    int posx = int.Parse(jdgroup[i]["posX"].ToString());
        //    int posy = int.Parse(jdgroup[i]["posY"].ToString());
        //    string name = jdgroup[i]["type"].ToString();
        //} 
    }

    void OnS2C_InitChar(BaseEvent evt)
    {
        clientmsg.s2c_initchar initchar = (clientmsg.s2c_initchar)evt.Params["protomsg"];
        LoginPanel.isCreateChararcter = initchar.newchar;

        int count = initchar.heros.Count;
        for (int i = 0; i < count; i++ )
        {
            ArrangementPanel.spritename.Add(int.Parse(initchar.heros[i].id.ToString()), initchar.heros[i].icon);
        }
    }

    void OnS2C_ResponseFightBegin(BaseEvent evt)
    {
        DungeonManager.enemyWave.Clear();
        clientmsg.s2c_fightbegin fightbegin = (clientmsg.s2c_fightbegin)evt.Params["protomsg"];
        ArrangementPanel.data = fightbegin.enemylist;
        if (ArrangementPanel.data.Count > 0)
        {
            CEventDispatcher.GetInstance().DispatchEvent(new CBaseEvent(CEventType.GAME_DATA, null));
        }
    }

    void OnApplicationQuit()
    {
//         if (awnetgate.IsConnected)
//             awnetgate.Disconnect();
    }

}