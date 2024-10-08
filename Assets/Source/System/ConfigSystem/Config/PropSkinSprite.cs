// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Prop_SkinSprite.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Deploy {

  /// <summary>Holder for reflection information generated from Prop_SkinSprite.proto</summary>
  public static partial class PropSkinSpriteReflection {

    #region Descriptor
    /// <summary>File descriptor for Prop_SkinSprite.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static PropSkinSpriteReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChVQcm9wX1NraW5TcHJpdGUucHJvdG8SBmRlcGxveSIyCg9Qcm9wX1NraW5T",
            "cHJpdGUSCgoCSWQYASABKAUSEwoLU2xvdFNwcml0ZXMYAiADKAkikwEKE1By",
            "b3BfU2tpblNwcml0ZV9NYXASNQoFSXRlbXMYASADKAsyJi5kZXBsb3kuUHJv",
            "cF9Ta2luU3ByaXRlX01hcC5JdGVtc0VudHJ5GkUKCkl0ZW1zRW50cnkSCwoD",
            "a2V5GAEgASgFEiYKBXZhbHVlGAIgASgLMhcuZGVwbG95LlByb3BfU2tpblNw",
            "cml0ZToCOAFiBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Deploy.Prop_SkinSprite), global::Deploy.Prop_SkinSprite.Parser, new[]{ "Id", "SlotSprites" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Deploy.Prop_SkinSprite_Map), global::Deploy.Prop_SkinSprite_Map.Parser, new[]{ "Items" }, null, null, null, new pbr::GeneratedClrTypeInfo[] { null, })
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class Prop_SkinSprite : pb::IMessage<Prop_SkinSprite> {
    private static readonly pb::MessageParser<Prop_SkinSprite> _parser = new pb::MessageParser<Prop_SkinSprite>(() => new Prop_SkinSprite());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Prop_SkinSprite> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Deploy.PropSkinSpriteReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Prop_SkinSprite() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Prop_SkinSprite(Prop_SkinSprite other) : this() {
      id_ = other.id_;
      slotSprites_ = other.slotSprites_.Clone();
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Prop_SkinSprite Clone() {
      return new Prop_SkinSprite(this);
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

    /// <summary>Field number for the "SlotSprites" field.</summary>
    public const int SlotSpritesFieldNumber = 2;
    private static readonly pb::FieldCodec<string> _repeated_slotSprites_codec
        = pb::FieldCodec.ForString(18);
    private readonly pbc::RepeatedField<string> slotSprites_ = new pbc::RepeatedField<string>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<string> SlotSprites {
      get { return slotSprites_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Prop_SkinSprite);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Prop_SkinSprite other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Id != other.Id) return false;
      if(!slotSprites_.Equals(other.slotSprites_)) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Id != 0) hash ^= Id.GetHashCode();
      hash ^= slotSprites_.GetHashCode();
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
      slotSprites_.WriteTo(output, _repeated_slotSprites_codec);
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
      size += slotSprites_.CalculateSize(_repeated_slotSprites_codec);
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Prop_SkinSprite other) {
      if (other == null) {
        return;
      }
      if (other.Id != 0) {
        Id = other.Id;
      }
      slotSprites_.Add(other.slotSprites_);
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
          case 18: {
            slotSprites_.AddEntriesFrom(input, _repeated_slotSprites_codec);
            break;
          }
        }
      }
    }

  }

  public sealed partial class Prop_SkinSprite_Map : pb::IMessage<Prop_SkinSprite_Map> {
    private static readonly pb::MessageParser<Prop_SkinSprite_Map> _parser = new pb::MessageParser<Prop_SkinSprite_Map>(() => new Prop_SkinSprite_Map());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Prop_SkinSprite_Map> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Deploy.PropSkinSpriteReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Prop_SkinSprite_Map() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Prop_SkinSprite_Map(Prop_SkinSprite_Map other) : this() {
      items_ = other.items_.Clone();
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Prop_SkinSprite_Map Clone() {
      return new Prop_SkinSprite_Map(this);
    }

    /// <summary>Field number for the "Items" field.</summary>
    public const int ItemsFieldNumber = 1;
    private static readonly pbc::MapField<int, global::Deploy.Prop_SkinSprite>.Codec _map_items_codec
        = new pbc::MapField<int, global::Deploy.Prop_SkinSprite>.Codec(pb::FieldCodec.ForInt32(8, 0), pb::FieldCodec.ForMessage(18, global::Deploy.Prop_SkinSprite.Parser), 10);
    private readonly pbc::MapField<int, global::Deploy.Prop_SkinSprite> items_ = new pbc::MapField<int, global::Deploy.Prop_SkinSprite>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::MapField<int, global::Deploy.Prop_SkinSprite> Items {
      get { return items_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Prop_SkinSprite_Map);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Prop_SkinSprite_Map other) {
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
    public void MergeFrom(Prop_SkinSprite_Map other) {
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
