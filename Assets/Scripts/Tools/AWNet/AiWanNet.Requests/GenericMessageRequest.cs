using AiWanNet.Entities;
using AiWanNet.Entities.Data;
using AiWanNet.Exceptions;
using System;
using System.Collections.Generic;
namespace AiWanNet.Requests
{
	public class GenericMessageRequest : BaseRequest
	{
		public static readonly string KEY_ROOM_ID = "r";
		public static readonly string KEY_USER_ID = "u";
		public static readonly string KEY_MESSAGE = "m";
		public static readonly string KEY_MESSAGE_TYPE = "t";
		public static readonly string KEY_RECIPIENT = "rc";
		public static readonly string KEY_RECIPIENT_MODE = "rm";
		public static readonly string KEY_XTRA_PARAMS = "p";
		public static readonly string KEY_SENDER_DATA = "sd";
		protected int type = -1;

		protected string message;
		protected IAWObject parameters;
		protected object recipient;
		protected int sendMode;
		public GenericMessageRequest() : base(RequestType.GenericMessage)
		{
		}
		public override void Validate(AiWan sfs)
		{
			if (this.type < 0)
			{
				throw new AWValidationError("PublicMessage request error", new string[]
				{
					"Unsupported message type: " + this.type
				});
			}
			List<string> list = new List<string>();
			switch (this.type)
			{
			case 0:
				this.ValidatePublicMessage(sfs, list);
				goto IL_A9;
			case 1:
				this.ValidatePrivateMessage(sfs, list);
				goto IL_A9;
			case 4:
				this.ValidateObjectMessage(sfs, list);
				goto IL_A9;

			}

			IL_A9:
			if (list.Count > 0)
			{
				throw new AWValidationError("Request error - ", list);
			}
		}
		public override void Execute(AiWan sfs)
		{
			this.sfso.PutByte(GenericMessageRequest.KEY_MESSAGE_TYPE, Convert.ToByte(this.type));
			
			this.ExecuteSuperUserMessage(sfs);
		}
		private void ValidatePublicMessage(AiWan sfs, List<string> errors)
		{
			if (this.message == null || this.message.Length == 0)
			{
				errors.Add("Public message is empty!");
			}

		}
		private void ValidatePrivateMessage(AiWan sfs, List<string> errors)
		{
			if (this.message == null || this.message.Length == 0)
			{
				errors.Add("Private message is empty!");
			}
			if ((int)this.recipient < 0)
			{
				errors.Add("Invalid recipient id: " + this.recipient);
			}
		}
		private void ValidateObjectMessage(AiWan sfs, List<string> errors)
		{
			if (this.parameters == null)
			{
				errors.Add("Object message is null!");
			}
		}
		
		
		private void ExecutePrivateMessage(AiWan sfs)
		{
			this.sfso.PutInt(GenericMessageRequest.KEY_RECIPIENT, (int)this.recipient);
			this.sfso.PutUtfString(GenericMessageRequest.KEY_MESSAGE, this.message);
			if (this.parameters != null)
			{
				this.sfso.PutSFSObject(GenericMessageRequest.KEY_XTRA_PARAMS, this.parameters);
			}
		}
		private void ExecuteBuddyMessage(AiWan sfs)
		{
			this.sfso.PutInt(GenericMessageRequest.KEY_RECIPIENT, (int)this.recipient);
			this.sfso.PutUtfString(GenericMessageRequest.KEY_MESSAGE, this.message);
			if (this.parameters != null)
			{
				this.sfso.PutSFSObject(GenericMessageRequest.KEY_XTRA_PARAMS, this.parameters);
			}
		}
		private void ExecuteSuperUserMessage(AiWan sfs)
		{
			this.sfso.PutUtfString(GenericMessageRequest.KEY_MESSAGE, this.message);
			if (this.parameters != null)
			{
				this.sfso.PutSFSObject(GenericMessageRequest.KEY_XTRA_PARAMS, this.parameters);
			}
			this.sfso.PutInt(GenericMessageRequest.KEY_RECIPIENT_MODE, this.sendMode);
			
		}
		
	}
}
