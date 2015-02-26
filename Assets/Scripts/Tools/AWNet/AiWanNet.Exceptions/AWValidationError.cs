using System;
using System.Collections.Generic;
namespace AiWanNet.Exceptions
{
	public class AWValidationError : Exception
	{
		private List<string> errors;
		public List<string> Errors
		{
			get
			{
				return this.errors;
			}
		}
		public AWValidationError(string message, ICollection<string> errors) : base(message)
		{
			this.errors = new List<string>(errors);
		}
	}
}
