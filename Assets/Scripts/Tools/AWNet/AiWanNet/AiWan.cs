﻿using AiWanNet.Bitswarm;
using AiWanNet.Core;
using AiWanNet.Entities;
using AiWanNet.Entities.Data;

using AiWanNet.Exceptions;
using AiWanNet.Logging;
using AiWanNet.Requests;
using AiWanNet.Util;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Timers;
namespace AiWanNet
{
	public class AiWan : IDispatchable
	{
		private const int DEFAULT_HTTP_PORT = 8080;
		private const int MAX_BB_CONNECT_ATTEMPTS = 3;
		private const char CLIENT_TYPE_SEPARATOR = ':';
		private int majVersion = 1;
		private int minVersion = 5;
		private int subVersion = 0;
		private BitSwarmClient bitSwarm;
		private string clientDetails = "Unity";
		private LagMonitor lagMonitor;
		private bool useBlueBox = true;
		private bool isJoining = false;
		
		private string sessionToken;
		
		private Logger log;
		private bool inited = false;
		private bool debug = false;
		private bool threadSafeMode = true;
		private bool isConnecting = false;
	
		
		private ConfigData config;
		private string currentZone;
		private bool autoConnectOnConfig = false;
		private string lastIpAddress;
		private EventDispatcher dispatcher;
		private object eventsLocker = new object();
		private Queue<BaseEvent> eventsQueue = new Queue<BaseEvent>();
		private int bbConnectionAttempt = 0;
		private System.Timers.Timer disconnectTimer = null;
        private bool isLoginServer = false;
        private bool isGateServer = false;

        public bool IsLoginServer
        {
            get
            {
                return this.isLoginServer;
            }
            set
            {
                this.isLoginServer = value;
            }
        }
        public bool IsGateServer
        {
            get
            {
                return this.isGateServer;
            }
            set
            {
                this.isGateServer = value;
            }
        }
		public BitSwarmClient BitSwarm
		{
			get
			{
				return this.bitSwarm;
			}
		}
		public Logger Log
		{
			get
			{
				return this.log;
			}
		}
		public bool IsConnecting
		{
			get
			{
				return this.isConnecting;
			}
		}
		public LagMonitor LagMonitor
		{
			get
			{
				return this.lagMonitor;
			}
		}
		public bool IsConnected
		{
			get
			{
				bool result = false;
				if (this.bitSwarm != null)
				{
					result = this.bitSwarm.Connected;
				}
				return result;
			}
		}
		public string Version
		{
			get
			{
				return string.Concat(new object[]
				{
					"",
					this.majVersion,
					".",
					this.minVersion,
					".",
					this.subVersion
				});
			}
		}
		public ConfigData Config
		{
			get
			{
				return this.config;
			}
		}
		public bool UseBlueBox
		{
			get
			{
				return this.useBlueBox;
			}
			set
			{
				this.useBlueBox = value;
			}
		}
        
		public string ConnectionMode
		{
			get
			{
				return this.bitSwarm.ConnectionMode;
			}
		}
		public int CompressionThreshold
		{
			get
			{
				return this.bitSwarm.CompressionThreshold;
			}
		}
		public int MaxMessageSize
		{
			get
			{
				return this.bitSwarm.MaxMessageSize;
			}
		}
		public bool Debug
		{
			get
			{
				return this.debug;
			}
			set
			{
				this.debug = value;
			}
		}
		public string CurrentIp
		{
			get
			{
				return this.bitSwarm.ConnectionIp;
			}
		}
		public int CurrentPort
		{
			get
			{
				return this.bitSwarm.ConnectionPort;
			}
		}
		public string CurrentZone
		{
			get
			{
				return this.currentZone;
			}
		}
		
		public Logger Logger
		{
			get
			{
				return this.log;
			}
		}
		
		public bool IsJoining
		{
			get
			{
				return this.isJoining;
			}
			set
			{
				this.isJoining = value;
			}
		}
		public string SessionToken
		{
			get
			{
				return this.sessionToken;
			}
		}
		public EventDispatcher Dispatcher
		{
			get
			{
				return this.dispatcher;
			}
		}
		public bool ThreadSafeMode
		{
			get
			{
				return this.threadSafeMode;
			}
			set
			{
				this.threadSafeMode = value;
			}
		}
		public AiWan()
		{
			this.log = new Logger(this);
			this.debug = false;
			this.Initialize();
		}
        public AiWan(bool debug)
		{
			this.log = new Logger(this);
			this.log.EnableEventDispatching = true;
			if (debug)
			{
				this.log.LoggingLevel = LogLevel.DEBUG;
			}
			this.debug = debug;
			this.Initialize();
		}
		private void Initialize()
		{
			if (!this.inited)
			{
				if (this.dispatcher == null)
				{
					this.dispatcher = new EventDispatcher(this);
				}
				this.bitSwarm = new BitSwarmClient(this);
				this.bitSwarm.IoHandler = new AWIOHandler(this.bitSwarm);
				this.bitSwarm.Init();
				this.bitSwarm.Dispatcher.AddEventListener(BitSwarmEvent.CONNECT, new EventListenerDelegate(this.OnSocketConnect));
				this.bitSwarm.Dispatcher.AddEventListener(BitSwarmEvent.DISCONNECT, new EventListenerDelegate(this.OnSocketClose));
				this.bitSwarm.Dispatcher.AddEventListener(BitSwarmEvent.RECONNECTION_TRY, new EventListenerDelegate(this.OnSocketReconnectionTry));
				this.bitSwarm.Dispatcher.AddEventListener(BitSwarmEvent.IO_ERROR, new EventListenerDelegate(this.OnSocketIOError));
				this.bitSwarm.Dispatcher.AddEventListener(BitSwarmEvent.SECURITY_ERROR, new EventListenerDelegate(this.OnSocketSecurityError));
				this.bitSwarm.Dispatcher.AddEventListener(BitSwarmEvent.DATA_ERROR, new EventListenerDelegate(this.OnSocketDataError));
				this.inited = true;
				this.Reset();
			}
		}
		private void Reset()
		{
			
		}
		public void SetClientDetails(string platformId, string version)
		{
			if (this.IsConnected)
			{
				this.log.Warn(new string[]
				{
					"SetClientDetails must be called before the connection is started"
				});
			}
			else
			{
				this.clientDetails = ((platformId == null) ? "" : platformId.Replace(':', ' '));
				this.clientDetails += ':';
				this.clientDetails += ((version == null) ? "" : version.Replace(':', ' '));
			}
		}
		public void EnableLagMonitor(bool enabled, int interval, int queueSize)
		{
			
			{
				if (enabled)
				{
					this.lagMonitor = new LagMonitor(this, interval, queueSize);
					this.lagMonitor.Start();
				}
				else
				{
					this.lagMonitor.Stop();
				}
			}
		}
		public void EnableLagMonitor(bool enabled)
		{
			this.EnableLagMonitor(enabled, 4, 10);
		}
		public void EnableLagMonitor(bool enabled, int interval)
		{
			this.EnableLagMonitor(enabled, interval, 10);
		}
		public BitSwarmClient GetSocketEngine()
		{
			return this.bitSwarm;
		}
	
		public void KillConnection()
		{
			this.bitSwarm.KillConnection();
		}
		public void Connect(string host, int port)
    	{
			if (this.IsConnected)
			{
				this.log.Warn(new string[]
				{
					"Already connected"
				});
			}
			else
			{
				if (this.isConnecting)
				{
					this.log.Warn(new string[]
					{
						"A connection attempt is already in progress"
					});
				}
				else
				{
					if (this.config != null)
					{
						if (host == null)
						{
							host = this.config.Host;
						}
						if (port == -1)
						{
							port = this.config.Port;
						}
					}
					if (host == null || host.Length == 0)
					{
						throw new ArgumentException("Invalid connection host/address");
					}
					if (port < 0 || port > 65535)
					{
						throw new ArgumentException("Invalid connection port");
					}
					try
					{
						IPAddress.Parse(host);
					}
					catch (FormatException)
					{
						try
						{
							host = Dns.GetHostEntry(host).AddressList[0].ToString(); 
						}
						catch (Exception ex)
						{
							string text = "Failed to lookup hostname " + host + ". Connection failed. Reason " + ex.Message;
							this.log.Error(new string[]
							{
								text
							});
							Hashtable hashtable = new Hashtable();
							hashtable["success"] = false;
							hashtable["errorMessage"] = text;
							this.DispatchEvent(new AWEvent(AWEvent.CONNECTION, hashtable));
							return;
						}
					}
					this.lastIpAddress = host;
					this.isConnecting = true;
					this.bitSwarm.Connect(host, port);
				}
			}
		}
		public void Connect()
		{
			this.Connect(null, -1);
		}
		public void Connect(string host)
		{
			this.Connect(host, -1);
		}
		public void Connect(ConfigData cfg)
		{
			this.ValidateConfig(cfg);
			this.Connect(cfg.Host, cfg.Port);
		}
		public void Disconnect()
		{
			if (this.IsConnected)
			{
				if (this.bitSwarm.ReconnectionSeconds > 0)
				{
					this.Send(new ManualDisconnectionRequest());
				}
				this.HandleClientDisconnection(ClientDisconnectionReason.MANUAL);
			}
			else
			{
				this.log.Info(new string[]
				{
					"You are not connected"
				});
			}
		}
		private void DisconnectConnection(int timeout)
		{
			if (this.disconnectTimer == null)
			{
				this.disconnectTimer = new System.Timers.Timer();
			}
			this.disconnectTimer.AutoReset = false;
			this.disconnectTimer.Elapsed += new ElapsedEventHandler(this.OnDisconnectConnectionEvent);
			this.disconnectTimer.Enabled = true;
		}
		private void OnDisconnectConnectionEvent(object source, ElapsedEventArgs e)
		{
			this.disconnectTimer.Enabled = false;
			this.bitSwarm.Disconnect(ClientDisconnectionReason.MANUAL);
		}

		public int GetReconnectionSeconds()
		{
			return this.bitSwarm.ReconnectionSeconds;
		}
		public void SetReconnectionSeconds(int seconds)
		{
			this.bitSwarm.ReconnectionSeconds = seconds;
		}

        //------------------------------------------------
        //
        public void SendProtobufMsg(ProtoBuf.IExtensible request)
        {
            
            if (!this.IsConnected)
            {
                this.log.Warn(new string[]
				{
					"You are not connected. Request cannot be sent: " + request
				});
            }
            else
            {
                try
                {
                    this.bitSwarm.SendProtobufMsg(request);
                }
                catch (AWValidationError sFSValidationError)
                {
                    string text = sFSValidationError.Message;
                    foreach (string current in sFSValidationError.Errors)
                    {
                        text = text + "\t" + current + "\n";
                    }
                    this.log.Warn(new string[]
					{
						text
					});
                }
                catch (AWCodecError awCodeError)
                {
                    this.log.Warn(new string[]
					{
						awCodeError.Message
					});
                }
            }
        }
		public void Send(IRequest request)
		{
			if (!this.IsConnected)
			{
				this.log.Warn(new string[]
				{
					"You are not connected. Request cannot be sent: " + request
				});
			}

		}
		public void LoadConfig(string filePath, bool connectOnSuccess)
		{
			ConfigLoader configLoader = new ConfigLoader(this);
			configLoader.Dispatcher.AddEventListener(AWEvent.CONFIG_LOAD_SUCCESS, new EventListenerDelegate(this.OnConfigLoadSuccess));
			configLoader.Dispatcher.AddEventListener(AWEvent.CONFIG_LOAD_FAILURE, new EventListenerDelegate(this.OnConfigLoadFailure));
			this.autoConnectOnConfig = connectOnSuccess;
			configLoader.LoadConfig(filePath);
		}
		public void LoadConfig(string filePath)
		{
			this.LoadConfig(filePath, true);
		}
		public void LoadConfig(bool connectOnSuccess)
		{
			
		}
		public void LoadConfig()
		{
			
		}
		public void AddLogListener(LogLevel logLevel, EventListenerDelegate eventListener)
		{
			this.AddEventListener(LoggerEvent.LogEventType(logLevel), eventListener);
			this.log.EnableEventDispatching = true;
		}
	
	
		private void OnSocketConnect(BaseEvent e)
		{
			BitSwarmEvent bitSwarmEvent = e as BitSwarmEvent;
			if ((bool)bitSwarmEvent.Params["success"])
			{
				//this.SendHandshakeRequest((bool)bitSwarmEvent.Params["isReconnection"]);
                UnityEngine.Debug.Log("call   OnSocketConnect ");
                Hashtable hashtable = new Hashtable();
                hashtable["success"] = true;
                hashtable["errorMessage"] = bitSwarmEvent.Params["message"];
                this.DispatchEvent(new AWEvent(AWEvent.CONNECTION, hashtable));
			}
			else
			{
				this.log.Warn(new string[]
				{
					"Connection attempt failed"
				});
				this.HandleConnectionProblem(bitSwarmEvent);
			}
		}
		private void OnSocketClose(BaseEvent e)
		{
			BitSwarmEvent bitSwarmEvent = e as BitSwarmEvent;
			this.Reset();
			Hashtable hashtable = new Hashtable();
			hashtable["reason"] = bitSwarmEvent.Params["reason"];
			this.DispatchEvent(new AWEvent(AWEvent.CONNECTION_LOST, hashtable));
		}
		private void OnSocketReconnectionTry(BaseEvent e)
		{
			this.DispatchEvent(new AWEvent(AWEvent.CONNECTION_RETRY));
		}
		private void OnSocketDataError(BaseEvent e)
		{
			Hashtable hashtable = new Hashtable();
			hashtable["errorMessage"] = e.Params["message"];
			this.DispatchEvent(new AWEvent(AWEvent.SOCKET_ERROR, hashtable));
		}
		private void OnSocketIOError(BaseEvent e)
		{
			BitSwarmEvent e2 = e as BitSwarmEvent;
			if (this.isConnecting)
			{
				this.HandleConnectionProblem(e2);
			}
		}
		private void OnSocketSecurityError(BaseEvent e)
		{
			BitSwarmEvent e2 = e as BitSwarmEvent;
			if (this.isConnecting)
			{
				this.HandleConnectionProblem(e2);
			}
		}
		private void OnConfigLoadSuccess(BaseEvent e)
		{
			AWEvent sFSEvent = e as AWEvent;
			ConfigLoader configLoader = sFSEvent.Target as ConfigLoader;
			ConfigData configData = sFSEvent.Params["cfg"] as ConfigData;
			configLoader.Dispatcher.RemoveEventListener(AWEvent.CONFIG_LOAD_SUCCESS, new EventListenerDelegate(this.OnConfigLoadSuccess));
			configLoader.Dispatcher.RemoveEventListener(AWEvent.CONFIG_LOAD_FAILURE, new EventListenerDelegate(this.OnConfigLoadFailure));
			this.ValidateConfig(configData);
			Hashtable hashtable = new Hashtable();
			hashtable["config"] = configData;
			BaseEvent evt = new AWEvent(AWEvent.CONFIG_LOAD_SUCCESS, hashtable);
			this.DispatchEvent(evt);
			if (this.autoConnectOnConfig)
			{
				this.Connect(this.config.Host, this.config.Port);
			}
		}
		private void OnConfigLoadFailure(BaseEvent e)
		{
			AWEvent sFSEvent = e as AWEvent;
			this.log.Error(new string[]
			{
				"Failed to load config: " + (string)sFSEvent.Params["message"]
			});
			ConfigLoader configLoader = sFSEvent.Target as ConfigLoader;
			configLoader.Dispatcher.RemoveEventListener(AWEvent.CONFIG_LOAD_SUCCESS, new EventListenerDelegate(this.OnConfigLoadSuccess));
			configLoader.Dispatcher.RemoveEventListener(AWEvent.CONFIG_LOAD_FAILURE, new EventListenerDelegate(this.OnConfigLoadFailure));
			BaseEvent evt = new AWEvent(AWEvent.CONFIG_LOAD_FAILURE);
			this.DispatchEvent(evt);
		}
		private void ValidateConfig(ConfigData cfgData)
		{
			if (cfgData.Host == null || cfgData.Host.Length == 0)
			{
				throw new ArgumentException("Invalid Host/IpAddress in external config file");
			}
			if (cfgData.Port < 0 || cfgData.Port > 65535)
			{
				throw new ArgumentException("Invalid TCP port in external config file");
			}
			if (cfgData.Zone == null || cfgData.Zone.Length == 0)
			{
				throw new ArgumentException("Invalid Zone name in external config file");
			}
			this.config = cfgData;
			this.debug = cfgData.Debug;
			this.useBlueBox = cfgData.UseBlueBox;
		}
		public void HandleHandShake(BaseEvent evt)
		{
            IAWObject iAWObject = evt.Params["message"] as IAWObject;
            if (iAWObject.IsNull(BaseRequest.KEY_ERROR_CODE))
			{
                this.sessionToken = iAWObject.GetUtfString(HandshakeRequest.KEY_SESSION_TOKEN);
                this.bitSwarm.CompressionThreshold = iAWObject.GetInt(HandshakeRequest.KEY_COMPRESSION_THRESHOLD);
                this.bitSwarm.MaxMessageSize = iAWObject.GetInt(HandshakeRequest.KEY_MAX_MESSAGE_SIZE);
				if (this.debug)
				{
					this.log.Debug(new string[]
					{
						string.Format("Handshake response: tk => {0}, ct => {1}", this.sessionToken, this.bitSwarm.CompressionThreshold)
					});
				}
				if (this.bitSwarm.IsReconnecting)
				{
					this.bitSwarm.IsReconnecting = false;
					this.DispatchEvent(new AWEvent(AWEvent.CONNECTION_RESUME));
				}
				else
				{
					this.isConnecting = false;
					Hashtable hashtable = new Hashtable();
					hashtable["success"] = true;
					this.DispatchEvent(new AWEvent(AWEvent.CONNECTION, hashtable));
				}
			}
			else
			{
                short @short = iAWObject.GetShort(BaseRequest.KEY_ERROR_CODE);
                string errorMessage = AWErrorCodes.GetErrorMessage((int)@short, this.log, iAWObject.GetUtfStringArray(BaseRequest.KEY_ERROR_PARAMS));
				Hashtable hashtable2 = new Hashtable();
				hashtable2["success"] = false;
				hashtable2["errorMessage"] = errorMessage;
				hashtable2["errorCode"] = @short;
				this.DispatchEvent(new AWEvent(AWEvent.CONNECTION, hashtable2));
			}
		}
		public void HandleLogin(BaseEvent evt)
		{
			this.currentZone = (evt.Params["zone"] as string);
		}
		public void HandleClientDisconnection(string reason)
		{
			this.bitSwarm.ReconnectionSeconds = 0;
			this.bitSwarm.Disconnect(reason);
			this.Reset();
			if (reason != null)
			{
				Hashtable hashtable = new Hashtable();
				hashtable.Add("reason", reason);
				this.DispatchEvent(new AWEvent(AWEvent.CONNECTION_LOST, hashtable));
			}
		}
		public void HandleLogout()
		{

		}
		private void HandleConnectionProblem(BaseEvent e)
		{
			if (this.IsConnecting && this.useBlueBox && this.bbConnectionAttempt < 3)
			{
				this.bbConnectionAttempt++;
				this.bitSwarm.ForceBlueBox(true);
				int port = (this.config == null) ? 8080 : this.config.HttpPort;
				this.bitSwarm.Connect(this.lastIpAddress, port);
				this.DispatchEvent(new AWEvent(AWEvent.CONNECTION_ATTEMPT_HTTP, new Hashtable()));
			}
			else
			{
				this.bitSwarm.ForceBlueBox(false);
				this.bbConnectionAttempt = 0;
				BitSwarmEvent bitSwarmEvent = e as BitSwarmEvent;
				Hashtable hashtable = new Hashtable();
				hashtable["success"] = false;
				hashtable["errorMessage"] = bitSwarmEvent.Params["message"];
				this.DispatchEvent(new AWEvent(AWEvent.CONNECTION, hashtable));
				this.isConnecting = false;
				this.bitSwarm.Destroy();
			}
		}
		public void HandleReconnectionFailure()
		{
			this.SetReconnectionSeconds(0);
			this.bitSwarm.StopReconnection();
		}
		private void SendHandshakeRequest(bool isReconnection)
		{
			IRequest request = new HandshakeRequest(this.Version, (!isReconnection) ? null : this.sessionToken, this.clientDetails);
			this.Send(request);
		}
		internal void DispatchEvent(BaseEvent evt)
		{
			if (!this.threadSafeMode)
			{
				this.Dispatcher.DispatchEvent(evt);
			}
			else
			{
				this.EnqueueEvent(evt);
			}
		}
		private void EnqueueEvent(BaseEvent evt)
		{
			object obj = this.eventsLocker;
			Monitor.Enter(obj);
			try
			{
				this.eventsQueue.Enqueue(evt);
			}
			finally
			{
				Monitor.Exit(obj);
			}
		}
		public void ProcessEvents()
		{
			if (this.threadSafeMode)
			{
				object obj = this.eventsLocker;
				Monitor.Enter(obj);
				BaseEvent[] array;
				try
				{
					array = this.eventsQueue.ToArray();
					this.eventsQueue.Clear();
				}
				finally
				{
					Monitor.Exit(obj);
				}
				BaseEvent[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					BaseEvent evt = array2[i];
					this.Dispatcher.DispatchEvent(evt);
				}
			}
		}
		public void AddEventListener(string eventType, EventListenerDelegate listener)
		{
			this.dispatcher.AddEventListener(eventType, listener);
		}
		public void RemoveEventListener(string eventType, EventListenerDelegate listener)
		{
			this.dispatcher.RemoveEventListener(eventType, listener);
		}
		public void RemoveAllEventListeners()
		{
			this.dispatcher.RemoveAll();
		}
	}
}
