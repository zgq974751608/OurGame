using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.IO;
//这边定义消息
using clientmsg;

public interface IMessageHandler
{
    void Process(ProtoBuf.IExtensible msg);
}

public class DefaultMessageHandler : IMessageHandler
{
    public void Process(ProtoBuf.IExtensible msg)
    {
        //默认处理函数, 相当于switch case 操作
        if (msg is clientmsg.s2c_begingame)
        {
        }
    }
}

public static class MessageDispatcher
{
    private static readonly IMessageHandler s_DefaultHandler = new DefaultMessageHandler();
    private static readonly Dictionary<Type, IMessageHandler> s_Handlers = new Dictionary<Type, IMessageHandler>();
    private static readonly Dictionary<string, Type> typelist = new Dictionary<string, Type>();

    static MessageDispatcher()
    {
        typelist.Add(typeof(clientmsg.s2c_login).FullName, typeof(clientmsg.s2c_login));

        typelist.Add(typeof(clientmsg.s2c_initchar).FullName, typeof(clientmsg.s2c_initchar));

        typelist.Add(typeof(clientmsg.SendMailContent).FullName, typeof(clientmsg.SendMailContent));
      
        typelist.Add(typeof(clientmsg.S2CProofTime).FullName, typeof(clientmsg.S2CProofTime));

        typelist.Add(typeof(clientmsg.s2c_begingame).FullName, typeof(clientmsg.s2c_begingame));
        typelist.Add(typeof(clientmsg.s2c_createchar).FullName, typeof(clientmsg.s2c_createchar));
        typelist.Add(typeof(clientmsg.s2c_intomainworld).FullName, typeof(clientmsg.s2c_intomainworld));
        typelist.Add(typeof(clientmsg.s2c_intodungeon).FullName, typeof(clientmsg.s2c_intodungeon));
        typelist.Add(typeof(clientmsg.s2c_intolevels).FullName, typeof(clientmsg.s2c_intolevels));
        typelist.Add(typeof(clientmsg.s2c_intoembattle).FullName, typeof(clientmsg.s2c_intoembattle));
        typelist.Add(typeof(clientmsg.s2c_myhero).FullName, typeof(clientmsg.s2c_myhero));
        typelist.Add(typeof(clientmsg.s2c_fightbegin).FullName, typeof(clientmsg.s2c_fightbegin));
    }

    public static void Dispatch(ProtoBuf.IExtensible msg)
    {
        Type key = msg.GetType();
        if (s_Handlers.ContainsKey(key))
        {
            // We found a specific handler! :)
            s_Handlers[key].Process(msg);
        }
        else
        {
            // We will have to resort to the default handler. :(
            s_DefaultHandler.Process(msg);
        }
    }
    public static Type getTypeByStr(string name)
    {
        return typelist[name];
    }
}