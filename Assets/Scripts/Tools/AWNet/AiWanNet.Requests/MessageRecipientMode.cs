﻿using System;
namespace AiWanNet.Requests
{
	public class MessageRecipientMode
	{
		private object target;
		private int mode;
		public object Target
		{
			get
			{
				return this.target;
			}
		}
		public int Mode
		{
			get
			{
				return this.mode;
			}
		}
		public MessageRecipientMode(int mode, object target)
		{
			if (mode < 0 || mode > 3)
			{
				throw new ArgumentException("Illegal recipient mode: " + mode);
			}
			this.mode = mode;
			this.target = target;
		}
	}
}
