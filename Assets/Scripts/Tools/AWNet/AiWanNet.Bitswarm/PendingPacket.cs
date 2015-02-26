using AiWanNet.Core;
using AiWanNet.Util;
using System;
namespace AiWanNet.Bitswarm
{
	public class PendingPacket
	{
		private PacketHeader header;
		private ByteArray buffer;
		public PacketHeader Header
		{
			get
			{
				return this.header;
			}
		}
		public ByteArray Buffer
		{
			get
			{
				return this.buffer;
			}
			set
			{
				this.buffer = value;
			}
		}
		public PendingPacket(PacketHeader header)
		{
			this.header = header;
			this.buffer = new ByteArray();
			this.buffer.Compressed = header.Compressed;
		}
	}
}
