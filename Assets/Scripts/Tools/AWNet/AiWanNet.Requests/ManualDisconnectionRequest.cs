using System;
namespace AiWanNet.Requests
{
	public class ManualDisconnectionRequest : BaseRequest
	{
		public ManualDisconnectionRequest() : base(RequestType.ManualDisconnection)
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
