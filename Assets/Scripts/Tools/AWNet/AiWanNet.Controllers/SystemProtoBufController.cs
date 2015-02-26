using AiWanNet.Bitswarm;
using AiWanNet.Core;
using AiWanNet.Entities;
using AiWanNet.Entities.Data;
using AiWanNet.Requests;
using AiWanNet.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using clientmsg;
namespace AiWanNet.Controllers
{
    public class SystemProtoBufController : BaseController
    {
        private Dictionary<string, RequestProtoBufDelegate> requestHandlers;
        public SystemProtoBufController(BitSwarmClient bitSwarm)
            : base(bitSwarm)
        {
            this.requestHandlers = new Dictionary<string, RequestProtoBufDelegate>();
            this.InitRequestHandlers();
        }
        private void InitRequestHandlers()
        {
            this.requestHandlers["clientmsg.s2c_login"] = new RequestProtoBufDelegate(this.OnS2C_Login);
            this.requestHandlers["clientmsg.s2c_initchar"] = new RequestProtoBufDelegate(this.OnS2C_ResponseInitChar);
            this.requestHandlers["clientmsg.SendMailContent"] = new RequestProtoBufDelegate(this.OnS2C_ResponseMailContent);
            this.requestHandlers["clientmsg.S2CProofTime"] = new RequestProtoBufDelegate(this.OnS2C_ResponseProofTime);
            this.requestHandlers["clientmsg.s2c_begingame"] = new RequestProtoBufDelegate(this.OnS2C_BeginGame);
            this.requestHandlers["clientmsg.s2c_createchar"] = new RequestProtoBufDelegate(this.OnS2C_CreateChar);
            this.requestHandlers["clientmsg.s2c_intomainworld"] = new RequestProtoBufDelegate(this.OnS2C_IntoMainWorld);
            this.requestHandlers["clientmsg.s2c_intodungeon"] = new RequestProtoBufDelegate(this.OnS2C_IntoDungeon);
            this.requestHandlers["clientmsg.s2c_intolevels"] = new RequestProtoBufDelegate(this.OnS2C_IntoLevels);
            this.requestHandlers["clientmsg.s2c_intoembattle"] = new RequestProtoBufDelegate(this.OnS2C_IntoemBattle);
            this.requestHandlers["clientmsg.s2c_myhero"] = new RequestProtoBufDelegate(this.OnS2C_MyHero);
            this.requestHandlers["clientmsg.s2c_fightbegin"] = new RequestProtoBufDelegate(this.OnS2C_FightBegin);
        }

       
        /// <summary>
        /// 重载消息分发器
        /// </summary>
        /// <param name="message"></param>
        public override void HandleMessage(ProtoBuf.IExtensible message)
        {
            if (this.sfs.Debug)
            {
                this.log.Info(new string[]
				{
					string.Concat(new object[]
					{
						"Message: ",
						message.GetType().FullName,
						" ",
						message
					})
				});
            }
            if (!this.requestHandlers.ContainsKey(message.GetType().FullName))
            {
                this.log.Warn(new string[]
				{
					"Unknown message name: " + message.GetType().FullName
				});
            }
            else
            {
                RequestProtoBufDelegate requestDelegate = this.requestHandlers[message.GetType().FullName];
                requestDelegate(message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        private void OnS2C_Login(ProtoBuf.IExtensible msg)
        {
            UnityEngine.Debug.Log("SystemProtoBufController------>OnS2C_LoginResponse()");
            clientmsg.s2c_login loginResponse = msg as clientmsg.s2c_login;

            Hashtable hashtable = new Hashtable();

            hashtable["protomsg"] = loginResponse;
            AWEvent evt = new AWEvent(AWEvent.LOGIN, hashtable);
            this.sfs.DispatchEvent(evt);
        }

        private void OnS2C_ResponseInitChar(ProtoBuf.IExtensible msg)
        {
            UnityEngine.Debug.Log("SystemProtoBufController------>OnS2C_ResponseInitChar");
            clientmsg.s2c_initchar clientinit = msg as clientmsg.s2c_initchar;
            Hashtable hashtable = new Hashtable();
           
            //if (clientinit.result == common.enumGetCharResult.Response_Success)
            {
                hashtable["protomsg"] = clientinit;
                AWEvent evt = new AWEvent(AWEvent.S2C_INITCHAR, hashtable);
                this.sfs.DispatchEvent(evt);
            }

        }

        private void OnS2C_ResponseMailContent(ProtoBuf.IExtensible msg)
        {
            UnityEngine.Debug.Log("SystemProtoBufController------>OnS2C_ResponseMailContent");
            clientmsg.SendMailContent mailcontent = msg as clientmsg.SendMailContent;
            Hashtable hashtable = new Hashtable();
            hashtable["protomsg"] = mailcontent;
            
            AWEvent evt = new AWEvent(AWEvent.RESPONSE_MAILCONTENT, hashtable);
            this.sfs.DispatchEvent(evt);
        }

        private void OnS2C_ResponseProofTime(ProtoBuf.IExtensible msg)
        {
            UnityEngine.Debug.Log("SystemProtoBufController------>OnS2C_ResponseProofTime");
            clientmsg.S2CProofTime prooftime = msg as clientmsg.S2CProofTime;
            Hashtable hashtable = new Hashtable();
            hashtable["protomsg"] = prooftime;
            AWEvent evt = new AWEvent(AWEvent.RESPONSE_PROOFTIME, hashtable);
            this.sfs.DispatchEvent(evt);
        }

        private void OnS2C_BeginGame(ProtoBuf.IExtensible msg)
        {
            UnityEngine.Debug.Log("SystemProtoBufController-------->OnS2C_ResponseLoginGame");
            clientmsg.s2c_begingame logingame = msg as clientmsg.s2c_begingame;
            Hashtable hashtable = new Hashtable();
            hashtable["protomsg"] = logingame;
            AWEvent evt = new AWEvent(AWEvent.S2C_BEGINGAME, hashtable);
            this.sfs.DispatchEvent(evt);
        }

        private void OnS2C_CreateChar(ProtoBuf.IExtensible msg)
        {
            UnityEngine.Debug.Log("SystemProtoBufController--------->OnS2C_CreateChar");
            clientmsg.s2c_createchar createchar = msg as clientmsg.s2c_createchar;
            Hashtable hashtable = new Hashtable();
            hashtable["protomsg"] = createchar;
            AWEvent evt = new AWEvent(AWEvent.S2C_CREATECHAR, hashtable);
            this.sfs.DispatchEvent(evt);
        }

        private void OnS2C_IntoMainWorld(ProtoBuf.IExtensible msg)
        {
            clientmsg.s2c_intomainworld intomainworld = msg as clientmsg.s2c_intomainworld;
            Hashtable hashtable = new Hashtable();
            hashtable["protomsg"] = intomainworld;
            AWEvent evt = new AWEvent(AWEvent.S2C_INTOMAINWORLD, hashtable);
            this.sfs.DispatchEvent(evt);
        }

        private void OnS2C_IntoDungeon(ProtoBuf.IExtensible msg)
        {
            clientmsg.s2c_intodungeon intodungeon = msg as clientmsg.s2c_intodungeon;
            Hashtable hashtable = new Hashtable();
            hashtable["protomsg"] = intodungeon;
            AWEvent evt = new AWEvent(AWEvent.S2C_INTODUNGEON, hashtable);
            this.sfs.DispatchEvent(evt);
        }

        private void OnS2C_IntoLevels(ProtoBuf.IExtensible msg)
        {
            clientmsg.s2c_intolevels intolevels = msg as clientmsg.s2c_intolevels;
            Hashtable hashtable = new Hashtable();
            hashtable["protomsg"] = intolevels;
            AWEvent evt = new AWEvent(AWEvent.S2C_INTOLEVELS, hashtable);
            this.sfs.DispatchEvent(evt);
        }

        private void OnS2C_IntoemBattle(ProtoBuf.IExtensible msg)
        {
            clientmsg.s2c_intoembattle intoembattle = msg as clientmsg.s2c_intoembattle;
            Hashtable hashtable = new Hashtable();
            hashtable["protomsg"] = intoembattle;
            AWEvent evt = new AWEvent(AWEvent.S2C_INTOEMBATTLE, hashtable);
            this.sfs.DispatchEvent(evt);
        }

        private void OnS2C_MyHero(ProtoBuf.IExtensible msg)
        {
            clientmsg.s2c_myhero myhero = msg as clientmsg.s2c_myhero;
            Hashtable hashtable = new Hashtable();
            hashtable["protomsg"] = myhero;
            AWEvent evt = new AWEvent(AWEvent.S2C_MYHERO, hashtable);
            this.sfs.DispatchEvent(evt);
        }

        private void OnS2C_FightBegin(ProtoBuf.IExtensible msg)
        {
            clientmsg.s2c_fightbegin fightbegin = msg as clientmsg.s2c_fightbegin;
            Hashtable hashtable = new Hashtable();
            hashtable["protomsg"] = fightbegin;
            AWEvent evt = new AWEvent(AWEvent.S2C_FIGHTBEGIN, hashtable);
            this.sfs.DispatchEvent(evt);
        }
    }
}
