﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: forwardmsg.proto
namespace servermsg
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"UserForwardGame")]
  public partial class UserForwardGame : global::ProtoBuf.IExtensible
  {
    public UserForwardGame() {}
    

    private uint _oid = default(uint);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"oid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint oid
    {
      get { return _oid; }
      set { _oid = value; }
    }

    private uint _charid = default(uint);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"charid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint charid
    {
      get { return _charid; }
      set { _charid = value; }
    }

    private string _msgname = "";
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"msgname", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string msgname
    {
      get { return _msgname; }
      set { _msgname = value; }
    }

    private byte[] _msg = null;
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"msg", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public byte[] msg
    {
      get { return _msg; }
      set { _msg = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"UserForwardCenter")]
  public partial class UserForwardCenter : global::ProtoBuf.IExtensible
  {
    public UserForwardCenter() {}
    

    private uint _oid = default(uint);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"oid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint oid
    {
      get { return _oid; }
      set { _oid = value; }
    }

    private uint _charid = default(uint);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"charid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint charid
    {
      get { return _charid; }
      set { _charid = value; }
    }

    private string _msgname = "";
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"msgname", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string msgname
    {
      get { return _msgname; }
      set { _msgname = value; }
    }

    private byte[] _msg = null;
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"msg", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public byte[] msg
    {
      get { return _msg; }
      set { _msg = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ServerForwardUser")]
  public partial class ServerForwardUser : global::ProtoBuf.IExtensible
  {
    public ServerForwardUser() {}
    

    private uint _gateid = default(uint);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"gateid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint gateid
    {
      get { return _gateid; }
      set { _gateid = value; }
    }

    private uint _charid = default(uint);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"charid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint charid
    {
      get { return _charid; }
      set { _charid = value; }
    }

    private string _msgname = "";
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"msgname", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string msgname
    {
      get { return _msgname; }
      set { _msgname = value; }
    }

    private byte[] _msg = null;
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"msg", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public byte[] msg
    {
      get { return _msg; }
      set { _msg = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ForwardCmdToNine")]
  public partial class ForwardCmdToNine : global::ProtoBuf.IExtensible
  {
    public ForwardCmdToNine() {}
    

    private uint _sceneid = default(uint);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"sceneid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint sceneid
    {
      get { return _sceneid; }
      set { _sceneid = value; }
    }

    private uint _posi = default(uint);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"posi", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint posi
    {
      get { return _posi; }
      set { _posi = value; }
    }

    private string _msgname = "";
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"msgname", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string msgname
    {
      get { return _msgname; }
      set { _msgname = value; }
    }

    private byte[] _msg = null;
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"msg", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public byte[] msg
    {
      get { return _msg; }
      set { _msg = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ForwardCmdToNineExceptMe")]
  public partial class ForwardCmdToNineExceptMe : global::ProtoBuf.IExtensible
  {
    public ForwardCmdToNineExceptMe() {}
    

    private uint _sceneid = default(uint);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"sceneid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint sceneid
    {
      get { return _sceneid; }
      set { _sceneid = value; }
    }

    private uint _posi = default(uint);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"posi", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint posi
    {
      get { return _posi; }
      set { _posi = value; }
    }

    private uint _charid = default(uint);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"charid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint charid
    {
      get { return _charid; }
      set { _charid = value; }
    }

    private string _msgname = "";
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"msgname", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string msgname
    {
      get { return _msgname; }
      set { _msgname = value; }
    }

    private byte[] _msg = null;
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"msg", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public byte[] msg
    {
      get { return _msg; }
      set { _msg = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"SceneTransmitMessage")]
  public partial class SceneTransmitMessage : global::ProtoBuf.IExtensible
  {
    public SceneTransmitMessage() {}
    

    private uint _id = default(uint);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint id
    {
      get { return _id; }
      set { _id = value; }
    }

    private uint _tyoe = default(uint);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"tyoe", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint tyoe
    {
      get { return _tyoe; }
      set { _tyoe = value; }
    }

    private string _msgname = "";
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"msgname", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string msgname
    {
      get { return _msgname; }
      set { _msgname = value; }
    }

    private byte[] _msg = null;
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"msg", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public byte[] msg
    {
      get { return _msg; }
      set { _msg = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ForwardCmdToDirect")]
  public partial class ForwardCmdToDirect : global::ProtoBuf.IExtensible
  {
    public ForwardCmdToDirect() {}
    

    private uint _sceneid = default(uint);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"sceneid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint sceneid
    {
      get { return _sceneid; }
      set { _sceneid = value; }
    }

    private uint _posi = default(uint);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"posi", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint posi
    {
      get { return _posi; }
      set { _posi = value; }
    }

    private uint _dir = default(uint);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"dir", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint dir
    {
      get { return _dir; }
      set { _dir = value; }
    }

    private string _msgname = "";
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"msgname", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string msgname
    {
      get { return _msgname; }
      set { _msgname = value; }
    }

    private byte[] _msg = null;
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"msg", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public byte[] msg
    {
      get { return _msg; }
      set { _msg = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"ForwardCmdToReverseDirect")]
  public partial class ForwardCmdToReverseDirect : global::ProtoBuf.IExtensible
  {
    public ForwardCmdToReverseDirect() {}
    

    private uint _sceneid = default(uint);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"sceneid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint sceneid
    {
      get { return _sceneid; }
      set { _sceneid = value; }
    }

    private uint _posi = default(uint);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"posi", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint posi
    {
      get { return _posi; }
      set { _posi = value; }
    }

    private uint _dir = default(uint);
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"dir", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint dir
    {
      get { return _dir; }
      set { _dir = value; }
    }

    private string _msgname = "";
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"msgname", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue("")]
    public string msgname
    {
      get { return _msgname; }
      set { _msgname = value; }
    }

    private byte[] _msg = null;
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"msg", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public byte[] msg
    {
      get { return _msg; }
      set { _msg = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}