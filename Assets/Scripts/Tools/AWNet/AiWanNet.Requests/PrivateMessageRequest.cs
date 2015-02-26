using AiWanNet.Entities.Data;
using System;
namespace AiWanNet.Requests
{
	public class PrivateMessageRequest : GenericMessageRequest
	{
		public PrivateMessageRequest(string message, int recipientId, IAWObject parameters)
		{
			this.type = 1;
			this.message = message;
			this.recipient = recipientId;
			this.parameters = parameters;
		}
		public PrivateMessageRequest(string message, int recipientId) : this(message, recipientId, null)
		{
		}
	}
}
