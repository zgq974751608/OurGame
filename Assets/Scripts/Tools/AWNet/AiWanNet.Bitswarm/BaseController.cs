using AiWanNet.Exceptions;
using AiWanNet.Logging;
using System;
namespace AiWanNet.Bitswarm
{
	public abstract class BaseController : IController
	{
		protected int id = -1;
		protected AiWan sfs;
		protected BitSwarmClient bitSwarm;
		protected Logger log;
		public int Id
		{
			get
			{
				return this.id;
			}
			set
			{
				if (this.id == -1)
				{
					this.id = value;
					return;
				}
				throw new AWError("Controller ID is already set: " + this.id + ". Can't be changed at runtime!");
			}
		}
		public BaseController(BitSwarmClient bitSwarm)
		{
			this.bitSwarm = bitSwarm;
			if (bitSwarm != null)
			{
				this.log = bitSwarm.Log;
				this.sfs = bitSwarm.Sfs;
			}
		}
        public virtual void HandleMessage(IMessage message) { }
        public virtual void HandleMessage(ProtoBuf.IExtensible message) { }
	}
}
