using System;
namespace AiWanNet.Requests
{
	public class PingPongRequest : BaseRequest
	{
		public PingPongRequest() : base(RequestType.PingPong)
		{
		}
		public override void Validate(AiWan sfs)
		{
		}
		public override void Execute(AiWan sfs)
		{
		}
	}
}
