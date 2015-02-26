using AiWanNet.Bitswarm;
using AiWanNet.Entities.Data;
using AiWanNet.Util;
using System;
namespace AiWanNet.Protocol
{
	public interface IProtocolCodec
	{
		IoHandler IOHandler
		{
			get;
			set;
		}
		void OnPacketRead(IAWObject packet);
		void OnPacketRead(ByteArray packet);
        void OnPacketRead(ProtoBuf.IExtensible message);
		void OnPacketWrite(IMessage message);
        void OnProtoBufPacketWrite(ProtoBuf.IExtensible message);
	}
}
