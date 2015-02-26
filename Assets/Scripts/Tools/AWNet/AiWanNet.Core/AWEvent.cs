using System;
using System.Collections;
namespace AiWanNet.Core
{
	public class AWEvent : BaseEvent
	{

		public static readonly string CONNECTION = "connection";
		public static readonly string SOCKET_ERROR = "socketError";
		public static readonly string CONNECTION_LOST = "connectionLost";
		public static readonly string CONNECTION_RETRY = "connectionRetry";
		public static readonly string CONNECTION_RESUME = "connectionResume";
		public static readonly string CONNECTION_ATTEMPT_HTTP = "connectionAttemptHttp";
		public static readonly string CONFIG_LOAD_SUCCESS = "configLoadSuccess";
		public static readonly string CONFIG_LOAD_FAILURE = "configLoadFailure";
		public static readonly string LOGIN = "login";
		public static readonly string LOGIN_ERROR = "loginError";

        public static readonly string S2C_INITCHAR = "initchar";
        public static readonly string RESPONSE_MAILCONTENT = "mailcontent";
        public static readonly string RESPONSE_PROOFTIME = "prooftime";
        public static readonly string S2C_BEGINGAME = "begingame";
        public static readonly string S2C_CREATECHAR = "createchar";
        public static readonly string S2C_INTOMAINWORLD = "intomainworld";
        public static readonly string S2C_INTODUNGEON = "intodungeon";
        public static readonly string S2C_INTOLEVELS = "intolevels";
        public static readonly string S2C_INTOEMBATTLE = "intoembattle";
        public static readonly string S2C_MYHERO = "myhero";
        public static readonly string S2C_FIGHTBEGIN = "fightbegin";
		public AWEvent(string type, Hashtable data) : base(type, data)
		{
		}
		public AWEvent(string type) : base(type, null)
		{
		}
	}
}
