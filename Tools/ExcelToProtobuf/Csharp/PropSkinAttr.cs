// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Prop_SkinAttr.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Deploy {

  /// <summary>Holder for reflection information generated from Prop_SkinAttr.proto</summary>
  public static partial class PropSkinAttrReflection {

    #region Descriptor
    /// <summary>File descriptor for Prop_SkinAttr.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static PropSkinAttrReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChNQcm9wX1NraW5BdHRyLnByb3RvEgZkZXBsb3kilgEKDVByb3BfU2tpbkF0",
            "dHISCgoCSWQYASABKAUSFAoMU2tpblBhcnRFbnVtGAIgASgFEjMKB0F0dHJB",
            "ZGQYAyADKAsyIi5kZXBsb3kuUHJvcF9Ta2luQXR0ci5BdHRyQWRkRW50cnka",
            "LgoMQXR0ckFkZEVudHJ5EgsKA2tleRgBIAEoBRINCgV2YWx1ZRgCIAEoBToC",
            "OAEijQEKEVByb3BfU2tpbkF0dHJfTWFwEjMKBUl0ZW1zGAEgAygLMiQuZGVw",
            "bG95LlByb3BfU2tpbkF0dHJfTWFwLkl0ZW1zRW50cnkaQwoKSXRlbXNFbnRy",
            "eRILCgNrZXkYASABKAUSJAoFdmFsdWUYAiABKAsyFS5kZXBsb3kuUHJvcF9T",
            "a2luQXR0cjoCOAFiBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Deploy.Prop_SkinAttr), global::Deploy.Prop_SkinAttr.Parser, new[]{ "Id", "SkinPartEnum", "AttrAdd" }, null, null, null, new pbr::GeneratedClrTypeInfo[] { null, }),
            new pbr::GeneratedClrTypeInfo(typeof(global::Deploy.Prop_SkinAttr_Map), global::Deploy.Prop_SkinAttr_Map.Parser, new[]{ "Items" }, null, null, null, new pbr::GeneratedClrTypeInfo[] { null, })
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class Prop_SkinAttr : pb::IMessage<Prop_SkinAttr> {
    private static readonly pb::MessageParser<Prop_SkinAttr> _parser = new pb::MessageParser<Prop_SkinAttr>(() => new Prop_SkinAttr());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Prop_SkinAttr> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Deploy.PropSkinAttrReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Prop_SkinAttr() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Prop_SkinAttr(Prop_SkinAttr other) : this() {
      id_ = other.id_;
      skinPartEnum_ = other.skinPartEnum_;
      attrAdd_ = other.attrAdd_.Clone();
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Prop_SkinAttr Clone() {
      return new Prop_SkinAttr(this);
    }

    /// <summary>Field number for the "Id" field.</summary>
    public const int IdFieldNumber = 1;
    private int id_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Id {
      get { return id_; }
      set {
        id_ = value;
      }
    }

    /// <summary>Field number for the "SkinPartEnum" field.</summary>
    public const int SkinPartEnumFieldNumber = 2;
    private int skinPartEnum_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int SkinPartEnum {
      get { return skinPartEnum_; }
      set {
        skinPartEnum_ = value;
      }
    }

    /// <summary>Field number for the "AttrAdd" field.</summary>
    public const int AttrAddFieldNumber = 3;
    private static readonly pbc::MapField<int, int>.Codec _map_attrAdd_codec
        = new pbc::MapField<int, int>.Codec(pb::FieldCodec.ForInt32(8, 0), pb::FieldCodec.ForInt32(16, 0), 26);
    private readonly pbc::MapField<int, int> attrAdd_ = new pbc::MapField<int, int>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::MapField<int, int> AttrAdd {
      get { return attrAdd_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Prop_SkinAttr);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Prop_SkinAttr other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Id != other.Id) return false;
      if (SkinPartEnum != other.SkinPartEnum) return false;
      if (!AttrAdd.Equals(other.AttrAdd)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Id != 0) hash ^= Id.GetHashCode();
      if (SkinPartEnum != 0) hash ^= SkinPartEnum.GetHashCode();
      hash ^= AttrAdd.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Id != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(Id);
      }
      if (SkinPartEnum != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(SkinPartEnum);
      }
      attrAdd_.WriteTo(output, _map_attrAdd_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Id != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Id);
      }
      if (SkinPartEnum != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(SkinPartEnum);
      }
      size += attrAdd_.CalculateSize(_map_attrAdd_codec);
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Prop_SkinAttr other) {
      if (other == null) {
        return;
      }
      if (other.Id != 0) {
        Id = other.Id;
      }
      if (other.SkinPartEnum != 0) {
        SkinPartEnum = other.SkinPartEnum;
      }
      attrAdd_.Add(other.attrAdd_);
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            Id = input.ReadInt32();
            break;
          }
          case 16: {
            SkinPartEnum = input.ReadInt32();
            break;
          }
          case 26: {
            attrAdd_.AddEntriesFrom(input, _map_attrAdd_codec);
            break;
          }
        }
      }
    }

  }

  public sealed partial class Prop_SkinAttr_Map : pb::IMessage<Prop_SkinAttr_Map> {
    private static readonly pb::MessageParser<Prop_SkinAttr_Map> _parser = new pb::MessageParser<Prop_SkinAttr_Map>(() => new Prop_SkinAttr_Map());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Prop_SkinAttr_Map> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Deploy.PropSkinAttrReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Prop_SkinAttr_Map() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Prop_SkinAttr_Map(Prop_SkinAttr_Map other) : this() {
      items_ = other.items_.Clone();
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Prop_SkinAttr_Map Clone() {
      return new Prop_SkinAttr_Map(this);
    }

    /// <summary>Field number for the "Items" field.</summary>
    public const int ItemsFieldNumber = 1;
    private static readonly pbc::MapField<int, global::Deploy.Prop_SkinAttr>.Codec _map_items_codec
        = new pbc::MapField<int, global::Deploy.Prop_SkinAttr>.Codec(pb::FieldCodec.ForInt32(8, 0), pb::FieldCodec.ForMessage(18, global::Deploy.Prop_SkinAttr.Parser), 10);
    private readonly pbc::MapField<int, global::Deploy.Prop_SkinAttr> items_ = new pbc::MapField<int, global::Deploy.Prop_SkinAttr>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::MapField<int, global::Deploy.Prop_SkinAttr> Items {
      get { return items_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Prop_SkinAttr_Map);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Prop_SkinAttr_Map other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (!Items.Equals(other.Items)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      hash ^= Items.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      items_.WriteTo(output, _map_items_codec);
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      size += items_.CalculateSize(_map_items_codec);
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Prop_SkinAttr_Map other) {
      if (other == null) {
        return;
      }
      items_.Add(other.items_);
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            items_.AddEntriesFrom(input, _map_items_codec);
            break;
          }
        }
      }
    }

  }

  #endregion

}

#endregion Designer generated code
