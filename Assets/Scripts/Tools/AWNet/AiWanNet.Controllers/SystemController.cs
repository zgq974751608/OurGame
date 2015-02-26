using AiWanNet.Bitswarm;
using AiWanNet.Core;
using AiWanNet.Entities;
using AiWanNet.Entities.Data;



using AiWanNet.Requests;


using AiWanNet.Util;
using System;
using System.Collections;
using System.Collections.Generic;
namespace AiWanNet.Controllers
{
    public class SystemController : BaseController
    {
        private Dictionary<int, RequestDelegate> requestHandlers;
        public SystemController(BitSwarmClient bitSwarm)
            : base(bitSwarm)
        {
            this.requestHandlers = new Dictionary<int, RequestDelegate>();
            this.InitRequestHandlers();
        }
        private void InitRequestHandlers()
        {
            this.requestHandlers[1005] = new RequestDelegate(this.FnClientDisconnection);
            this.requestHandlers[1006] = new RequestDelegate(this.FnReconnectionFailure);

        }

        public override void HandleMessage(IMessage message)
        {
            if (this.sfs.Debug)
            {
                this.log.Info(new string[]
				{
					string.Concat(new object[]
					{
						"Message: ",
						(RequestType)message.Id,
						" ",
						message
					})
				});
            }
            if (!this.requestHandlers.ContainsKey(message.Id))
            {
                this.log.Warn(new string[]
				{
					"Unknown message id: " + message.Id
				});
            }
            else
            {
                RequestDelegate requestDelegate = this.requestHandlers[message.Id];
                requestDelegate(message);
            }
        }

        private void FnClientDisconnection(IMessage msg)
        {
            IAWObject content = msg.Content;
            int @byte = (int)content.GetByte("dr");
            this.sfs.HandleClientDisconnection(ClientDisconnectionReason.GetReason(@byte));
        }

        private void FnReconnectionFailure(IMessage msg)
        {
            this.sfs.HandleReconnectionFailure();
        }

    }
}
