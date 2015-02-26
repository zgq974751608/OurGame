using System;
namespace AiWanNet.Entities.Data
{
	public class AWDataWrapper
	{
		private int type;
		private object data;
		public int Type
		{
			get
			{
				return this.type;
			}
		}
		public object Data
		{
			get
			{
				return this.data;
			}
		}
		public AWDataWrapper(int type, object data)
		{
			this.type = type;
			this.data = data;
		}
		public AWDataWrapper(AWDataType tp, object data)
		{
			this.type = (int)tp;
			this.data = data;
		}
	}
}
