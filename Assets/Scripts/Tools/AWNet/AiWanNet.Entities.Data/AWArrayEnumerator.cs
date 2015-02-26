using System;
using System.Collections;
using System.Collections.Generic;
namespace AiWanNet.Entities.Data
{
	public class AWArrayEnumerator : IEnumerator
	{
		private List<AWDataWrapper> data;
		private int cursorIndex;
		object IEnumerator.Current
		{
			get
			{
				if (this.cursorIndex < 0 || this.cursorIndex == this.data.Count)
				{
					throw new InvalidOperationException();
				}
				return this.data[this.cursorIndex].Data;
			}
		}
		void IEnumerator.Reset()
		{
			this.cursorIndex = -1;
		}
		bool IEnumerator.MoveNext()
		{
			if (this.cursorIndex < this.data.Count)
			{
				this.cursorIndex++;
			}
			return this.cursorIndex != this.data.Count;
		}
		public AWArrayEnumerator(List<AWDataWrapper> data)
		{
			this.data = data;
			this.cursorIndex = -1;
		}
	}
}
