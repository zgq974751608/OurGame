using System;
namespace AiWanNet.Bitswarm
{
	public enum PacketReadTransition
	{
		HeaderReceived,
		SizeReceived,
		IncompleteSize,
		WholeSizeReceived,
		PacketFinished,
		InvalidData,
		InvalidDataFinished
	}
}
