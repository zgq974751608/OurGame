using AiWanNet.Util;
using System;
namespace AiWanNet.Core
{
	public delegate void WriteBinaryDataDelegate(PacketHeader header, ByteArray binData, bool udp);
    public delegate void WriteProtoBufBinaryDataDelegate(ProtoBufPackageHeader header, ByteArray binData);
}
