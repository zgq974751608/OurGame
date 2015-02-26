using AiWanNet.Bitswarm;
using System;
namespace AiWanNet.Controllers
{
	public delegate void RequestDelegate(IMessage msg);
    public delegate void RequestProtoBufDelegate(ProtoBuf.IExtensible msg);

}
