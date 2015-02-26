using AiWanNet.Bitswarm;
using System;
namespace AiWanNet.Requests
{
	public interface IRequest
	{
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
		IMessage Message
		{
			get;
		}
		void Validate(AiWan sfs);
		void Execute(AiWan sfs);
	}
}
