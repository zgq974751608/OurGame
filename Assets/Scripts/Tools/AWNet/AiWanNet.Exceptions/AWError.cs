using System;
namespace AiWanNet.Exceptions
{
	public class AWError : Exception
	{
		public AWError(string message) : base(message)
		{
		}
	}
}
