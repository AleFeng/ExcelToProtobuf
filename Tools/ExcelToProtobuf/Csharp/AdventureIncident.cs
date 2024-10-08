// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Adventure_Incident.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Deploy {

  /// <summary>Holder for reflection information generated from Adventure_Incident.proto</summary>
  public static partial class AdventureIncidentReflection {

    #region Descriptor
    /// <summary>File descriptor for Adventure_Incident.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static AdventureIncidentReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChhBZHZlbnR1cmVfSW5jaWRlbnQucHJvdG8SBmRlcGxveSKZAgoSQWR2ZW50",
            "dXJlX0luY2lkZW50EgoKAklkGAEgASgFEgwKBE5hbWUYAiABKAkSEAoIRGVz",
            "Y3JpYmUYAyABKAkSFQoNTGlua0luY2lkZW50cxgEIAMoCRIjChtMaW5rSW5j",
            "aWRlbnRTY29yZUNvbmRpdGlvbnMYBSADKAkSJAocTGlua0luY2lkZW50Q2hv",
            "b3NlQ29uZGl0aW9ucxgGIAMoCRINCgVJdGVtcxgHIAMoCRITCgtQcm9iYWJp",
            "bGl0eRgIIAEoBRIQCghQcmlvcml0eRgJIAEoBRIOCgZXZWlnaHQYCiABKAUS",
            "FgoOQ29uZGl0aW9uQXJyYXkYCyADKAkSFwoPTG9naWNFeHByZXNzaW9uGAwg",
            "ASgJIpwBChZBZHZlbnR1cmVfSW5jaWRlbnRfTWFwEjgKBUl0ZW1zGAEgAygL",
            "MikuZGVwbG95LkFkdmVudHVyZV9JbmNpZGVudF9NYXAuSXRlbXNFbnRyeRpI",
            "CgpJdGVtc0VudHJ5EgsKA2tleRgBIAEoBRIpCgV2YWx1ZRgCIAEoCzIaLmRl",
            "cGxveS5BZHZlbnR1cmVfSW5jaWRlbnQ6AjgBYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Deploy.Adventure_Incident), global::Deploy.Adventure_Incident.Parser, new[]{ "Id", "Name", "Describe", "LinkIncidents", "LinkIncidentScoreConditions", "LinkIncidentChooseConditions", "Items", "Probability", "Priority", "Weight", "ConditionArray", "LogicExpression" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Deploy.Adventure_Incident_Map), global::Deploy.Adventure_Incident_Map.Parser, new[]{ "Items" }, null, null, null, new pbr::GeneratedClrTypeInfo[] { null, })
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class Adventure_Incident : pb::IMessage<Adventure_Incident> {
    private static readonly pb::MessageParser<Adventure_Incident> _parser = new pb::MessageParser<Adventure_Incident>(() => new Adventure_Incident());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Adventure_Incident> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Deploy.AdventureIncidentReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Adventure_Incident() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Adventure_Incident(Adventure_Incident other) : this() {
      id_ = other.id_;
      name_ = other.name_;
      describe_ = other.describe_;
      linkIncidents_ = other.linkIncidents_.Clone();
      linkIncidentScoreConditions_ = other.linkIncidentScoreConditions_.Clone();
      linkIncidentChooseConditions_ = other.linkIncidentChooseConditions_.Clone();
      items_ = other.items_.Clone();
      probability_ = other.probability_;
      priority_ = other.priority_;
      weight_ = other.weight_;
      conditionArray_ = other.conditionArray_.Clone();
      logicExpression_ = other.logicExpression_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Adventure_Incident Clone() {
      return new Adventure_Incident(this);
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

    /// <summary>Field number for the "Name" field.</summary>
    public const int NameFieldNumber = 2;
    private string name_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Name {
      get { return name_; }
      set {
        name_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "Describe" field.</summary>
    public const int DescribeFieldNumber = 3;
    private string describe_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Describe {
      get { return describe_; }
      set {
        describe_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "LinkIncidents" field.</summary>
    public const int LinkIncidentsFieldNumber = 4;
    private static readonly pb::FieldCodec<string> _repeated_linkIncidents_codec
        = pb::FieldCodec.ForString(34);
    private readonly pbc::RepeatedField<string> linkIncidents_ = new pbc::RepeatedField<string>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<string> LinkIncidents {
      get { return linkIncidents_; }
    }

    /// <summary>Field number for the "LinkIncidentScoreConditions" field.</summary>
    public const int LinkIncidentScoreConditionsFieldNumber = 5;
    private static readonly pb::FieldCodec<string> _repeated_linkIncidentScoreConditions_codec
        = pb::FieldCodec.ForString(42);
    private readonly pbc::RepeatedField<string> linkIncidentScoreConditions_ = new pbc::RepeatedField<string>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<string> LinkIncidentScoreConditions {
      get { return linkIncidentScoreConditions_; }
    }

    /// <summary>Field number for the "LinkIncidentChooseConditions" field.</summary>
    public const int LinkIncidentChooseConditionsFieldNumber = 6;
    private static readonly pb::FieldCodec<string> _repeated_linkIncidentChooseConditions_codec
        = pb::FieldCodec.ForString(50);
    private readonly pbc::RepeatedField<string> linkIncidentChooseConditions_ = new pbc::RepeatedField<string>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<string> LinkIncidentChooseConditions {
      get { return linkIncidentChooseConditions_; }
    }

    /// <summary>Field number for the "Items" field.</summary>
    public const int ItemsFieldNumber = 7;
    private static readonly pb::FieldCodec<string> _repeated_items_codec
        = pb::FieldCodec.ForString(58);
    private readonly pbc::RepeatedField<string> items_ = new pbc::RepeatedField<string>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<string> Items {
      get { return items_; }
    }

    /// <summary>Field number for the "Probability" field.</summary>
    public const int ProbabilityFieldNumber = 8;
    private int probability_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Probability {
      get { return probability_; }
      set {
        probability_ = value;
      }
    }

    /// <summary>Field number for the "Priority" field.</summary>
    public const int PriorityFieldNumber = 9;
    private int priority_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Priority {
      get { return priority_; }
      set {
        priority_ = value;
      }
    }

    /// <summary>Field number for the "Weight" field.</summary>
    public const int WeightFieldNumber = 10;
    private int weight_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Weight {
      get { return weight_; }
      set {
        weight_ = value;
      }
    }

    /// <summary>Field number for the "ConditionArray" field.</summary>
    public const int ConditionArrayFieldNumber = 11;
    private static readonly pb::FieldCodec<string> _repeated_conditionArray_codec
        = pb::FieldCodec.ForString(90);
    private readonly pbc::RepeatedField<string> conditionArray_ = new pbc::RepeatedField<string>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<string> ConditionArray {
      get { return conditionArray_; }
    }

    /// <summary>Field number for the "LogicExpression" field.</summary>
    public const int LogicExpressionFieldNumber = 12;
    private string logicExpression_ = "";
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string LogicExpression {
      get { return logicExpression_; }
      set {
        logicExpression_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Adventure_Incident);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Adventure_Incident other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Id != other.Id) return false;
      if (Name != other.Name) return false;
      if (Describe != other.Describe) return false;
      if(!linkIncidents_.Equals(other.linkIncidents_)) return false;
      if(!linkIncidentScoreConditions_.Equals(other.linkIncidentScoreConditions_)) return false;
      if(!linkIncidentChooseConditions_.Equals(other.linkIncidentChooseConditions_)) return false;
      if(!items_.Equals(other.items_)) return false;
      if (Probability != other.Probability) return false;
      if (Priority != other.Priority) return false;
      if (Weight != other.Weight) return false;
      if(!conditionArray_.Equals(other.conditionArray_)) return false;
      if (LogicExpression != other.LogicExpression) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Id != 0) hash ^= Id.GetHashCode();
      if (Name.Length != 0) hash ^= Name.GetHashCode();
      if (Describe.Length != 0) hash ^= Describe.GetHashCode();
      hash ^= linkIncidents_.GetHashCode();
      hash ^= linkIncidentScoreConditions_.GetHashCode();
      hash ^= linkIncidentChooseConditions_.GetHashCode();
      hash ^= items_.GetHashCode();
      if (Probability != 0) hash ^= Probability.GetHashCode();
      if (Priority != 0) hash ^= Priority.GetHashCode();
      if (Weight != 0) hash ^= Weight.GetHashCode();
      hash ^= conditionArray_.GetHashCode();
      if (LogicExpression.Length != 0) hash ^= LogicExpression.GetHashCode();
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
      if (Name.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Name);
      }
      if (Describe.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(Describe);
      }
      linkIncidents_.WriteTo(output, _repeated_linkIncidents_codec);
      linkIncidentScoreConditions_.WriteTo(output, _repeated_linkIncidentScoreConditions_codec);
      linkIncidentChooseConditions_.WriteTo(output, _repeated_linkIncidentChooseConditions_codec);
      items_.WriteTo(output, _repeated_items_codec);
      if (Probability != 0) {
        output.WriteRawTag(64);
        output.WriteInt32(Probability);
      }
      if (Priority != 0) {
        output.WriteRawTag(72);
        output.WriteInt32(Priority);
      }
      if (Weight != 0) {
        output.WriteRawTag(80);
        output.WriteInt32(Weight);
      }
      conditionArray_.WriteTo(output, _repeated_conditionArray_codec);
      if (LogicExpression.Length != 0) {
        output.WriteRawTag(98);
        output.WriteString(LogicExpression);
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
      if (Name.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Name);
      }
      if (Describe.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Describe);
      }
      size += linkIncidents_.CalculateSize(_repeated_linkIncidents_codec);
      size += linkIncidentScoreConditions_.CalculateSize(_repeated_linkIncidentScoreConditions_codec);
      size += linkIncidentChooseConditions_.CalculateSize(_repeated_linkIncidentChooseConditions_codec);
      size += items_.CalculateSize(_repeated_items_codec);
      if (Probability != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Probability);
      }
      if (Priority != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Priority);
      }
      if (Weight != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Weight);
      }
      size += conditionArray_.CalculateSize(_repeated_conditionArray_codec);
      if (LogicExpression.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(LogicExpression);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Adventure_Incident other) {
      if (other == null) {
        return;
      }
      if (other.Id != 0) {
        Id = other.Id;
      }
      if (other.Name.Length != 0) {
        Name = other.Name;
      }
      if (other.Describe.Length != 0) {
        Describe = other.Describe;
      }
      linkIncidents_.Add(other.linkIncidents_);
      linkIncidentScoreConditions_.Add(other.linkIncidentScoreConditions_);
      linkIncidentChooseConditions_.Add(other.linkIncidentChooseConditions_);
      items_.Add(other.items_);
      if (other.Probability != 0) {
        Probability = other.Probability;
      }
      if (other.Priority != 0) {
        Priority = other.Priority;
      }
      if (other.Weight != 0) {
        Weight = other.Weight;
      }
      conditionArray_.Add(other.conditionArray_);
      if (other.LogicExpression.Length != 0) {
        LogicExpression = other.LogicExpression;
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
          case 18: {
            Name = input.ReadString();
            break;
          }
          case 26: {
            Describe = input.ReadString();
            break;
          }
          case 34: {
            linkIncidents_.AddEntriesFrom(input, _repeated_linkIncidents_codec);
            break;
          }
          case 42: {
            linkIncidentScoreConditions_.AddEntriesFrom(input, _repeated_linkIncidentScoreConditions_codec);
            break;
          }
          case 50: {
            linkIncidentChooseConditions_.AddEntriesFrom(input, _repeated_linkIncidentChooseConditions_codec);
            break;
          }
          case 58: {
            items_.AddEntriesFrom(input, _repeated_items_codec);
            break;
          }
          case 64: {
            Probability = input.ReadInt32();
            break;
          }
          case 72: {
            Priority = input.ReadInt32();
            break;
          }
          case 80: {
            Weight = input.ReadInt32();
            break;
          }
          case 90: {
            conditionArray_.AddEntriesFrom(input, _repeated_conditionArray_codec);
            break;
          }
          case 98: {
            LogicExpression = input.ReadString();
            break;
          }
        }
      }
    }

  }

  public sealed partial class Adventure_Incident_Map : pb::IMessage<Adventure_Incident_Map> {
    private static readonly pb::MessageParser<Adventure_Incident_Map> _parser = new pb::MessageParser<Adventure_Incident_Map>(() => new Adventure_Incident_Map());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Adventure_Incident_Map> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Deploy.AdventureIncidentReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Adventure_Incident_Map() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Adventure_Incident_Map(Adventure_Incident_Map other) : this() {
      items_ = other.items_.Clone();
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Adventure_Incident_Map Clone() {
      return new Adventure_Incident_Map(this);
    }

    /// <summary>Field number for the "Items" field.</summary>
    public const int ItemsFieldNumber = 1;
    private static readonly pbc::MapField<int, global::Deploy.Adventure_Incident>.Codec _map_items_codec
        = new pbc::MapField<int, global::Deploy.Adventure_Incident>.Codec(pb::FieldCodec.ForInt32(8, 0), pb::FieldCodec.ForMessage(18, global::Deploy.Adventure_Incident.Parser), 10);
    private readonly pbc::MapField<int, global::Deploy.Adventure_Incident> items_ = new pbc::MapField<int, global::Deploy.Adventure_Incident>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::MapField<int, global::Deploy.Adventure_Incident> Items {
      get { return items_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Adventure_Incident_Map);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Adventure_Incident_Map other) {
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
    public void MergeFrom(Adventure_Incident_Map other) {
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
