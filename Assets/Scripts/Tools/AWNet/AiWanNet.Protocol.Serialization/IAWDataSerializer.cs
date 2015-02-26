using AiWanNet.Entities.Data;
using AiWanNet.Util;
using System;
namespace AiWanNet.Protocol.Serialization
{
	public interface IAWDataSerializer
	{
		ByteArray Object2Binary(IAWObject obj);
		ByteArray Array2Binary(IAWArray array);
		IAWObject Binary2Object(ByteArray data);
		IAWArray Binary2Array(ByteArray data);
	}
}
