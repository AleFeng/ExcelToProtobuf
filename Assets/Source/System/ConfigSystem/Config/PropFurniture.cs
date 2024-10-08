// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Prop_Furniture.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Deploy {

  /// <summary>Holder for reflection information generated from Prop_Furniture.proto</summary>
  public static partial class PropFurnitureReflection {

    #region Descriptor
    /// <summary>File descriptor for Prop_Furniture.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static PropFurnitureReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChRQcm9wX0Z1cm5pdHVyZS5wcm90bxIGZGVwbG95IpcBCg5Qcm9wX0Z1cm5p",
            "dHVyZRIKCgJJZBgBIAEoBRIMCgRUeXBlGAIgASgFEg8KB1NldFR5cGUYAyAB",
            "KAUSFQoNR3JpZEl0ZW1TaXplWBgEIAEoBRIVCg1HcmlkSXRlbVNpemVZGAUg",
            "ASgFEhUKDUdyaWRJdGVtU2l6ZVoYBiABKAUSFQoNRmFjaWxpdHlWYWx1ZRgH",
            "IAEoBSKQAQoSUHJvcF9GdXJuaXR1cmVfTWFwEjQKBUl0ZW1zGAEgAygLMiUu",
            "ZGVwbG95LlByb3BfRnVybml0dXJlX01hcC5JdGVtc0VudHJ5GkQKCkl0ZW1z",
            "RW50cnkSCwoDa2V5GAEgASgFEiUKBXZhbHVlGAIgASgLMhYuZGVwbG95LlBy",
            "b3BfRnVybml0dXJlOgI4AWIGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Deploy.Prop_Furniture), global::Deploy.Prop_Furniture.Parser, new[]{ "Id", "Type", "SetType", "GridItemSizeX", "GridItemSizeY", "GridItemSizeZ", "FacilityValue" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Deploy.Prop_Furniture_Map), global::Deploy.Prop_Furniture_Map.Parser, new[]{ "Items" }, null, null, null, new pbr::GeneratedClrTypeInfo[] { null, })
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class Prop_Furniture : pb::IMessage<Prop_Furniture> {
    private static readonly pb::MessageParser<Prop_Furniture> _parser = new pb::MessageParser<Prop_Furniture>(() => new Prop_Furniture());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Prop_Furniture> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Deploy.PropFurnitureReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Prop_Furniture() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Prop_Furniture(Prop_Furniture other) : this() {
      id_ = other.id_;
      type_ = other.type_;
      setType_ = other.setType_;
      gridItemSizeX_ = other.gridItemSizeX_;
      gridItemSizeY_ = other.gridItemSizeY_;
      gridItemSizeZ_ = other.gridItemSizeZ_;
      facilityValue_ = other.facilityValue_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Prop_Furniture Clone() {
      return new Prop_Furniture(this);
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

    /// <summary>Field number for the "Type" field.</summary>
    public const int TypeFieldNumber = 2;
    private int type_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Type {
      get { return type_; }
      set {
        type_ = value;
      }
    }

    /// <summary>Field number for the "SetType" field.</summary>
    public const int SetTypeFieldNumber = 3;
    private int setType_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int SetType {
      get { return setType_; }
      set {
        setType_ = value;
      }
    }

    /// <summary>Field number for the "GridItemSizeX" field.</summary>
    public const int GridItemSizeXFieldNumber = 4;
    private int gridItemSizeX_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int GridItemSizeX {
      get { return gridItemSizeX_; }
      set {
        gridItemSizeX_ = value;
      }
    }

    /// <summary>Field number for the "GridItemSizeY" field.</summary>
    public const int GridItemSizeYFieldNumber = 5;
    private int gridItemSizeY_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int GridItemSizeY {
      get { return gridItemSizeY_; }
      set {
        gridItemSizeY_ = value;
      }
    }

    /// <summary>Field number for the "GridItemSizeZ" field.</summary>
    public const int GridItemSizeZFieldNumber = 6;
    private int gridItemSizeZ_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int GridItemSizeZ {
      get { return gridItemSizeZ_; }
      set {
        gridItemSizeZ_ = value;
      }
    }

    /// <summary>Field number for the "FacilityValue" field.</summary>
    public const int FacilityValueFieldNumber = 7;
    private int facilityValue_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int FacilityValue {
      get { return facilityValue_; }
      set {
        facilityValue_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Prop_Furniture);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Prop_Furniture other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Id != other.Id) return false;
      if (Type != other.Type) return false;
      if (SetType != other.SetType) return false;
      if (GridItemSizeX != other.GridItemSizeX) return false;
      if (GridItemSizeY != other.GridItemSizeY) return false;
      if (GridItemSizeZ != other.GridItemSizeZ) return false;
      if (FacilityValue != other.FacilityValue) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Id != 0) hash ^= Id.GetHashCode();
      if (Type != 0) hash ^= Type.GetHashCode();
      if (SetType != 0) hash ^= SetType.GetHashCode();
      if (GridItemSizeX != 0) hash ^= GridItemSizeX.GetHashCode();
      if (GridItemSizeY != 0) hash ^= GridItemSizeY.GetHashCode();
      if (GridItemSizeZ != 0) hash ^= GridItemSizeZ.GetHashCode();
      if (FacilityValue != 0) hash ^= FacilityValue.GetHashCode();
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
      if (Type != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(Type);
      }
      if (SetType != 0) {
        output.WriteRawTag(24);
        output.WriteInt32(SetType);
      }
      if (GridItemSizeX != 0) {
        output.WriteRawTag(32);
        output.WriteInt32(GridItemSizeX);
      }
      if (GridItemSizeY != 0) {
        output.WriteRawTag(40);
        output.WriteInt32(GridItemSizeY);
      }
      if (GridItemSizeZ != 0) {
        output.WriteRawTag(48);
        output.WriteInt32(GridItemSizeZ);
      }
      if (FacilityValue != 0) {
        output.WriteRawTag(56);
        output.WriteInt32(FacilityValue);
      }
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
      if (Type != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Type);
      }
      if (SetType != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(SetType);
      }
      if (GridItemSizeX != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(GridItemSizeX);
      }
      if (GridItemSizeY != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(GridItemSizeY);
      }
      if (GridItemSizeZ != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(GridItemSizeZ);
      }
      if (FacilityValue != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(FacilityValue);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Prop_Furniture other) {
      if (other == null) {
        return;
      }
      if (other.Id != 0) {
        Id = other.Id;
      }
      if (other.Type != 0) {
        Type = other.Type;
      }
      if (other.SetType != 0) {
        SetType = other.SetType;
      }
      if (other.GridItemSizeX != 0) {
        GridItemSizeX = other.GridItemSizeX;
      }
      if (other.GridItemSizeY != 0) {
        GridItemSizeY = other.GridItemSizeY;
      }
      if (other.GridItemSizeZ != 0) {
        GridItemSizeZ = other.GridItemSizeZ;
      }
      if (other.FacilityValue != 0) {
        FacilityValue = other.FacilityValue;
      }
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
            Type = input.ReadInt32();
            break;
          }
          case 24: {
            SetType = input.ReadInt32();
            break;
          }
          case 32: {
            GridItemSizeX = input.ReadInt32();
            break;
          }
          case 40: {
            GridItemSizeY = input.ReadInt32();
            break;
          }
          case 48: {
            GridItemSizeZ = input.ReadInt32();
            break;
          }
          case 56: {
            FacilityValue = input.ReadInt32();
            break;
          }
        }
      }
    }

  }

  public sealed partial class Prop_Furniture_Map : pb::IMessage<Prop_Furniture_Map> {
    private static readonly pb::MessageParser<Prop_Furniture_Map> _parser = new pb::MessageParser<Prop_Furniture_Map>(() => new Prop_Furniture_Map());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Prop_Furniture_Map> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Deploy.PropFurnitureReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Prop_Furniture_Map() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Prop_Furniture_Map(Prop_Furniture_Map other) : this() {
      items_ = other.items_.Clone();
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Prop_Furniture_Map Clone() {
      return new Prop_Furniture_Map(this);
    }

    /// <summary>Field number for the "Items" field.</summary>
    public const int ItemsFieldNumber = 1;
    private static readonly pbc::MapField<int, global::Deploy.Prop_Furniture>.Codec _map_items_codec
        = new pbc::MapField<int, global::Deploy.Prop_Furniture>.Codec(pb::FieldCodec.ForInt32(8, 0), pb::FieldCodec.ForMessage(18, global::Deploy.Prop_Furniture.Parser), 10);
    private readonly pbc::MapField<int, global::Deploy.Prop_Furniture> items_ = new pbc::MapField<int, global::Deploy.Prop_Furniture>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::MapField<int, global::Deploy.Prop_Furniture> Items {
      get { return items_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Prop_Furniture_Map);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Prop_Furniture_Map other) {
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
    public void MergeFrom(Prop_Furniture_Map other) {
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
