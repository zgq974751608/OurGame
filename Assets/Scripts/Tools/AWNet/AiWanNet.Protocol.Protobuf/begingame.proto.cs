//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from: begingame.proto
// Note: requires additional types generated from: common.proto
using common;

namespace clientmsg
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"c2s_initchar")]
  public partial class c2s_initchar : global::ProtoBuf.IExtensible
  {
    public c2s_initchar() {}
    

    private uint _uaid = default(uint);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"uaid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint uaid
    {
      get { return _uaid; }
      set { _uaid = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"s2c_initchar")]
  public partial class s2c_initchar : global::ProtoBuf.IExtensible
  {
    public s2c_initchar() {}
    

    private uint _uaid = default(uint);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"uaid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint uaid
    {
      get { return _uaid; }
      set { _uaid = value; }
    }

    private bool _newchar = default(bool);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"newchar", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(default(bool))]
    public bool newchar
    {
      get { return _newchar; }
      set { _newchar = value; }
    }

    private msgcharinfo _charinfo = null;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"charinfo", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public msgcharinfo charinfo
    {
      get { return _charinfo; }
      set { _charinfo = value; }
    }
    private readonly global::System.Collections.Generic.List<CharHeroSaveData> _heros = new global::System.Collections.Generic.List<CharHeroSaveData>();
    [global::ProtoBuf.ProtoMember(4, Name=@"heros", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<CharHeroSaveData> heros
    {
      get { return _heros; }
    }
  
    private readonly global::System.Collections.Generic.List<uint> _openmaps = new global::System.Collections.Generic.List<uint>();
    [global::ProtoBuf.ProtoMember(5, Name=@"openmaps", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public global::System.Collections.Generic.List<uint> openmaps
    {
      get { return _openmaps; }
    }
  
    private readonly global::System.Collections.Generic.List<CharSaveItem> _items = new global::System.Collections.Generic.List<CharSaveItem>();
    [global::ProtoBuf.ProtoMember(6, Name=@"items", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<CharSaveItem> items
    {
      get { return _items; }
    }
  

    private CharQuestData _quests = null;
    [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name=@"quests", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public CharQuestData quests
    {
      get { return _quests; }
      set { _quests = value; }
    }
    private readonly global::System.Collections.Generic.List<CharSaveRune> _runes = new global::System.Collections.Generic.List<CharSaveRune>();
    [global::ProtoBuf.ProtoMember(8, Name=@"runes", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<CharSaveRune> runes
    {
      get { return _runes; }
    }
  
    private readonly global::System.Collections.Generic.List<uint> _summonids = new global::System.Collections.Generic.List<uint>();
    [global::ProtoBuf.ProtoMember(9, Name=@"summonids", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public global::System.Collections.Generic.List<uint> summonids
    {
      get { return _summonids; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"c2s_createchar")]
  public partial class c2s_createchar : global::ProtoBuf.IExtensible
  {
    public c2s_createchar() {}
    

    private uint _uaid = default(uint);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"uaid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint uaid
    {
      get { return _uaid; }
      set { _uaid = value; }
    }

    private msgnewchar _newchar = null;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"newchar", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public msgnewchar newchar
    {
      get { return _newchar; }
      set { _newchar = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"s2c_createchar")]
  public partial class s2c_createchar : global::ProtoBuf.IExtensible
  {
    public s2c_createchar() {}
    

    private uint _uaid = default(uint);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"uaid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint uaid
    {
      get { return _uaid; }
      set { _uaid = value; }
    }

    private enum_createchar _result = enum_createchar.createchar_charexist;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"result", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(enum_createchar.createchar_charexist)]
    public enum_createchar result
    {
      get { return _result; }
      set { _result = value; }
    }

    private msgcharinfo _charinfo = null;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"charinfo", DataFormat = global::ProtoBuf.DataFormat.Default)]
    [global::System.ComponentModel.DefaultValue(null)]
    public msgcharinfo charinfo
    {
      get { return _charinfo; }
      set { _charinfo = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"c2s_intomainworld")]
  public partial class c2s_intomainworld : global::ProtoBuf.IExtensible
  {
    public c2s_intomainworld() {}
    

    private uint _uaid = default(uint);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"uaid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint uaid
    {
      get { return _uaid; }
      set { _uaid = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"s2c_intomainworld")]
  public partial class s2c_intomainworld : global::ProtoBuf.IExtensible
  {
    public s2c_intomainworld() {}
    

    private uint _uaid = default(uint);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"uaid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint uaid
    {
      get { return _uaid; }
      set { _uaid = value; }
    }

    private enum_userintox _result = enum_userintox.intox_fail;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"result", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(enum_userintox.intox_fail)]
    public enum_userintox result
    {
      get { return _result; }
      set { _result = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"c2s_intodungeon")]
  public partial class c2s_intodungeon : global::ProtoBuf.IExtensible
  {
    public c2s_intodungeon() {}
    

    private uint _uaid = default(uint);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"uaid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint uaid
    {
      get { return _uaid; }
      set { _uaid = value; }
    }

    private uint _dungeonid = default(uint);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"dungeonid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint dungeonid
    {
      get { return _dungeonid; }
      set { _dungeonid = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"s2c_intodungeon")]
  public partial class s2c_intodungeon : global::ProtoBuf.IExtensible
  {
    public s2c_intodungeon() {}
    

    private uint _uaid = default(uint);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"uaid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint uaid
    {
      get { return _uaid; }
      set { _uaid = value; }
    }

    private uint _dungeonid = default(uint);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"dungeonid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint dungeonid
    {
      get { return _dungeonid; }
      set { _dungeonid = value; }
    }

    private enum_userintox _result = enum_userintox.intox_fail;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"result", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(enum_userintox.intox_fail)]
    public enum_userintox result
    {
      get { return _result; }
      set { _result = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"c2s_intolevels")]
  public partial class c2s_intolevels : global::ProtoBuf.IExtensible
  {
    public c2s_intolevels() {}
    

    private uint _uaid = default(uint);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"uaid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint uaid
    {
      get { return _uaid; }
      set { _uaid = value; }
    }

    private uint _levelsid = default(uint);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"levelsid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint levelsid
    {
      get { return _levelsid; }
      set { _levelsid = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"s2c_intolevels")]
  public partial class s2c_intolevels : global::ProtoBuf.IExtensible
  {
    public s2c_intolevels() {}
    

    private uint _uaid = default(uint);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"uaid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint uaid
    {
      get { return _uaid; }
      set { _uaid = value; }
    }

    private uint _levelsid = default(uint);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"levelsid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint levelsid
    {
      get { return _levelsid; }
      set { _levelsid = value; }
    }

    private enum_userintox _result = enum_userintox.intox_fail;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"result", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(enum_userintox.intox_fail)]
    public enum_userintox result
    {
      get { return _result; }
      set { _result = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"c2s_intoembattle")]
  public partial class c2s_intoembattle : global::ProtoBuf.IExtensible
  {
    public c2s_intoembattle() {}
    

    private uint _uaid = default(uint);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"uaid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint uaid
    {
      get { return _uaid; }
      set { _uaid = value; }
    }

    private uint _levelsid = default(uint);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"levelsid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint levelsid
    {
      get { return _levelsid; }
      set { _levelsid = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"s2c_intoembattle")]
  public partial class s2c_intoembattle : global::ProtoBuf.IExtensible
  {
    public s2c_intoembattle() {}
    

    private uint _uaid = default(uint);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"uaid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint uaid
    {
      get { return _uaid; }
      set { _uaid = value; }
    }

    private uint _levelsid = default(uint);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"levelsid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint levelsid
    {
      get { return _levelsid; }
      set { _levelsid = value; }
    }

    private enum_userintox _result = enum_userintox.intox_fail;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"result", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(enum_userintox.intox_fail)]
    public enum_userintox result
    {
      get { return _result; }
      set { _result = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"c2s_summon")]
  public partial class c2s_summon : global::ProtoBuf.IExtensible
  {
    public c2s_summon() {}
    

    private uint _uaid = default(uint);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"uaid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint uaid
    {
      get { return _uaid; }
      set { _uaid = value; }
    }

    private uint _summonid = default(uint);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"summonid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint summonid
    {
      get { return _summonid; }
      set { _summonid = value; }
    }

    private enum_summon _summontype = enum_summon.summon_first;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"summontype", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(enum_summon.summon_first)]
    public enum_summon summontype
    {
      get { return _summontype; }
      set { _summontype = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"s2c_summon")]
  public partial class s2c_summon : global::ProtoBuf.IExtensible
  {
    public s2c_summon() {}
    

    private uint _uaid = default(uint);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"uaid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint uaid
    {
      get { return _uaid; }
      set { _uaid = value; }
    }

    private uint _summonid = default(uint);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"summonid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint summonid
    {
      get { return _summonid; }
      set { _summonid = value; }
    }

    private enum_summon _summontype = enum_summon.summon_first;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"summontype", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(enum_summon.summon_first)]
    public enum_summon summontype
    {
      get { return _summontype; }
      set { _summontype = value; }
    }
    private readonly global::System.Collections.Generic.List<CharHeroSaveData> _heros = new global::System.Collections.Generic.List<CharHeroSaveData>();
    [global::ProtoBuf.ProtoMember(4, Name=@"heros", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<CharHeroSaveData> heros
    {
      get { return _heros; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"c2s_myhero")]
  public partial class c2s_myhero : global::ProtoBuf.IExtensible
  {
    public c2s_myhero() {}
    

    private uint _uaid = default(uint);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"uaid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint uaid
    {
      get { return _uaid; }
      set { _uaid = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"s2c_myhero")]
  public partial class s2c_myhero : global::ProtoBuf.IExtensible
  {
    public s2c_myhero() {}
    

    private uint _uaid = default(uint);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"uaid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint uaid
    {
      get { return _uaid; }
      set { _uaid = value; }
    }

    private uint _heronum = default(uint);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"heronum", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint heronum
    {
      get { return _heronum; }
      set { _heronum = value; }
    }
    private readonly global::System.Collections.Generic.List<msghero> _herolist = new global::System.Collections.Generic.List<msghero>();
    [global::ProtoBuf.ProtoMember(3, Name=@"herolist", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<msghero> herolist
    {
      get { return _herolist; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"s2c_mynewhero")]
  public partial class s2c_mynewhero : global::ProtoBuf.IExtensible
  {
    public s2c_mynewhero() {}
    

    private uint _uaid = default(uint);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"uaid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint uaid
    {
      get { return _uaid; }
      set { _uaid = value; }
    }

    private uint _heronum = default(uint);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"heronum", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint heronum
    {
      get { return _heronum; }
      set { _heronum = value; }
    }
    private readonly global::System.Collections.Generic.List<msghero> _herolist = new global::System.Collections.Generic.List<msghero>();
    [global::ProtoBuf.ProtoMember(3, Name=@"herolist", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<msghero> herolist
    {
      get { return _herolist; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"s2c_createhero")]
  public partial class s2c_createhero : global::ProtoBuf.IExtensible
  {
    public s2c_createhero() {}
    

    private uint _uaid = default(uint);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"uaid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint uaid
    {
      get { return _uaid; }
      set { _uaid = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"c2s_fightbegin")]
  public partial class c2s_fightbegin : global::ProtoBuf.IExtensible
  {
    public c2s_fightbegin() {}
    

    private uint _uaid = default(uint);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"uaid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint uaid
    {
      get { return _uaid; }
      set { _uaid = value; }
    }

    private uint _enemyid = default(uint);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"enemyid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint enemyid
    {
      get { return _enemyid; }
      set { _enemyid = value; }
    }
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"s2c_fightbegin")]
  public partial class s2c_fightbegin : global::ProtoBuf.IExtensible
  {
    public s2c_fightbegin() {}
    

    private uint _uaid = default(uint);
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"uaid", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint uaid
    {
      get { return _uaid; }
      set { _uaid = value; }
    }

    private uint _enemygruop = default(uint);
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"enemygruop", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    [global::System.ComponentModel.DefaultValue(default(uint))]
    public uint enemygruop
    {
      get { return _enemygruop; }
      set { _enemygruop = value; }
    }
    private readonly global::System.Collections.Generic.List<string> _enemylist = new global::System.Collections.Generic.List<string>();
    [global::ProtoBuf.ProtoMember(3, Name=@"enemylist", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<string> enemylist
    {
      get { return _enemylist; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}