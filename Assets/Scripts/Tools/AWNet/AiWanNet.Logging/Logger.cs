using AiWanNet.Core;
using System;
using System.Collections;
namespace AiWanNet.Logging
{
	public class Logger
	{
		private AiWan AiWanNet;
		private bool enableConsoleTrace = true;
		private bool enableEventDispatching = true;
		private LogLevel loggingLevel;
		public bool EnableConsoleTrace
		{
			get
			{
				return this.enableConsoleTrace;
			}
			set
			{
				this.enableConsoleTrace = value;
			}
		}
		public bool EnableEventDispatching
		{
			get
			{
				return this.enableEventDispatching;
			}
			set
			{
				this.enableEventDispatching = value;
			}
		}
		public LogLevel LoggingLevel
		{
			get
			{
				return this.loggingLevel;
			}
			set
			{
				this.loggingLevel = value;
			}
		}
		public Logger(AiWan AiWanNet)
		{
			this.AiWanNet = AiWanNet;
			this.loggingLevel = LogLevel.INFO;
		}
		public void Debug(params string[] messages)
		{
			this.Log(LogLevel.DEBUG, string.Join(" ", messages));
		}
		public void Info(params string[] messages)
		{
			this.Log(LogLevel.INFO, string.Join(" ", messages));
		}
		public void Warn(params string[] messages)
		{
			this.Log(LogLevel.WARN, string.Join(" ", messages));
		}
		public void Error(params string[] messages)
		{
			this.Log(LogLevel.ERROR, string.Join(" ", messages));
		}
		private void Log(LogLevel level, string message)
		{
			if (level >= this.loggingLevel)
			{
				if (this.enableConsoleTrace)
				{
					Console.WriteLine(string.Concat(new object[]
					{
						"[SFS - ",
						level,
						"] ",
						message
					}));
				}
				if (this.enableEventDispatching && this.AiWanNet != null)
				{
					Hashtable hashtable = new Hashtable();
					hashtable.Add("message", message);
					LoggerEvent evt = new LoggerEvent(this.loggingLevel, hashtable);
					this.AiWanNet.DispatchEvent(evt);
				}
			}
		}
		public void AddEventListener(LogLevel level, EventListenerDelegate listener)
		{
			if (this.AiWanNet != null)
			{
				this.AiWanNet.AddEventListener(LoggerEvent.LogEventType(level), listener);
			}
		}
	}
}
