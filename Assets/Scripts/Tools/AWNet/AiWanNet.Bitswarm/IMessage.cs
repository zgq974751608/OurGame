using AiWanNet.Entities.Data;
using System;
namespace AiWanNet.Bitswarm
{
	public interface IMessage
	{
		int Id
		{
			get;
			set;
		}
		IAWObject Content
		{
			get;
			set;
		}
		int TargetController
		{
			get;
			set;
		}
		bool IsEncrypted
		{
			get;
			set;
		}
		bool IsUDP
		{
			get;
			set;
		}
		long PacketId
		{
			get;
			set;
		}
	}
}
