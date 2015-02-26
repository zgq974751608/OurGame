using AiWanNet.Bitswarm;
using AiWanNet.Entities.Data;
using AiWanNet.Exceptions;
using AiWanNet.Logging;
using AiWanNet.Protocol;
using AiWanNet.Util;
using System;
namespace AiWanNet.Core
{
	public class AWProtocolCodec : IProtocolCodec
	{
        private static readonly string CONTROLLER_ID = "c";
        private static readonly string ACTION_ID = "a";
        private static readonly string PARAM_ID = "p";
        private static readonly string UDP_PACKET_ID = "i";
        private IoHandler ioHandler = null;
        private Logger log;
        private BitSwarmClient bitSwarm;
        public IoHandler IOHandler
        {
            get
            {
                return this.ioHandler;
            }
            set
            {
                if (this.ioHandler != null)
                {
                    throw new AWError("IOHandler is already defined for thir ProtocolHandler instance: " + this);
                }
                this.ioHandler = value;
            }
        }
        public AWProtocolCodec(IoHandler ioHandler, BitSwarmClient bitSwarm)
		{
			this.ioHandler = ioHandler;
			this.log = bitSwarm.Log;
			this.bitSwarm = bitSwarm;
		}
		public void OnPacketRead(ByteArray packet)
		{
			IAWObject requestObject = AWObject.NewFromBinaryData(packet);
			this.DispatchRequest(requestObject);
		}
		public void OnPacketRead(IAWObject packet)
		{
			this.DispatchRequest(packet);
		}
        public void OnPacketRead(ProtoBuf.IExtensible message)
        {
            this.DispatchRequest(message);

        }
		public void OnPacketWrite(IMessage message)
		{
			if (this.bitSwarm.Debug)
			{
				this.log.Debug(new string[]
				{
					"Writing message " + message.Content.GetHexDump()
				});
			}
			IAWObject content;

			content = this.PrepareTCPPacket(message);
		
			message.Content = content;
			this.ioHandler.OnDataWrite(message);
		}
        //-------------------------------------------
        // ProtoBuf Packet to tcp or udp 2014-1-14 zxb
        public void OnProtoBufPacketWrite(ProtoBuf.IExtensible message)
        {
            if (this.bitSwarm.Debug)
            {
                this.log.Debug(new string[]
				{
					"Writing message " 
				});
            }
            this.ioHandler.OnProtoBufDataWrite(message);
        }
		private IAWObject PrepareTCPPacket(IMessage message)
		{
			IAWObject iSFSObject = new AWObject();
			iSFSObject.PutByte(AWProtocolCodec.CONTROLLER_ID, Convert.ToByte(message.TargetController));
			iSFSObject.PutShort(AWProtocolCodec.ACTION_ID, Convert.ToInt16(message.Id));
			iSFSObject.PutSFSObject(AWProtocolCodec.PARAM_ID, message.Content);
			return iSFSObject;
		}
		
		private void DispatchRequest(IAWObject requestObject)
		{
			IMessage message = new Message();
			if (requestObject.IsNull(AWProtocolCodec.CONTROLLER_ID))
			{
				throw new AWCodecError("Request rejected: No Controller ID in request!");
			}
			if (requestObject.IsNull(AWProtocolCodec.ACTION_ID))
			{
				throw new AWCodecError("Request rejected: No Action ID in request!");
			}
			message.Id = Convert.ToInt32(requestObject.GetShort(AWProtocolCodec.ACTION_ID));
			message.Content = requestObject.GetSFSObject(AWProtocolCodec.PARAM_ID);
			message.IsUDP = requestObject.ContainsKey(AWProtocolCodec.UDP_PACKET_ID);
			if (message.IsUDP)
			{
				message.PacketId = requestObject.GetLong(AWProtocolCodec.UDP_PACKET_ID);
			}
			int @byte = (int)requestObject.GetByte(AWProtocolCodec.CONTROLLER_ID);
            UnityEngine.Debug.Log("@byte" + @byte);
			IController controller = this.bitSwarm.GetController(@byte);
			if (controller == null)
			{
				throw new AWError("Cannot handle server response. Unknown controller, id: " + @byte);
			}
			controller.HandleMessage(message);
		}
        private void DispatchRequest(ProtoBuf.IExtensible message)
        {
            IController controller = this.bitSwarm.GetController(2);
            if (controller == null)
            {
                throw new AWError("Cannot handle server response. Unknown controller, id: " + 2);
            }
            controller.HandleMessage(message);
        }
	}
}
