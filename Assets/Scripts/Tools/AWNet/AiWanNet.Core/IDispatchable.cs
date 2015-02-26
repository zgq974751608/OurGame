using System;
namespace AiWanNet.Core
{
	public interface IDispatchable
	{
		EventDispatcher Dispatcher
		{
			get;
		}
		void AddEventListener(string eventType, EventListenerDelegate listener);
	}
}
