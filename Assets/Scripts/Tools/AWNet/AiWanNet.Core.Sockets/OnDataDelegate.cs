﻿using System;
namespace AiWanNet.Core.Sockets
{
	public delegate void OnDataDelegate(byte[] msg);
    public delegate void OnDataProtoBufDelegate(byte[] msg , int msgTypeLen);
}
