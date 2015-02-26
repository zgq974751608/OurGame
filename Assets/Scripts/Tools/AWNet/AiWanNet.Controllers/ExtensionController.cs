using AiWanNet.Bitswarm;
using AiWanNet.Core;
using AiWanNet.Entities.Data;
using System;
using System.Collections;
namespace AiWanNet.Controllers
{
	public class ExtensionController : BaseController
	{
		public static readonly string KEY_CMD = "c";
		public static readonly string KEY_PARAMS = "p";
		public static readonly string KEY_ROOM = "r";
		public ExtensionController(BitSwarmClient bitSwarm) : base(bitSwarm)
		{
		}
		public override void HandleMessage(IMessage message)
		{
			
		}
        
	}
}
