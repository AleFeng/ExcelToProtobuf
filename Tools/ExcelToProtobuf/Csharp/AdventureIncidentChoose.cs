// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: Adventure_IncidentChoose.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Deploy {

  /// <summary>Holder for reflection information generated from Adventure_IncidentChoose.proto</summary>
  public static partial class AdventureIncidentChooseReflection {

    #region Descriptor
    /// <summary>File descriptor for Adventure_IncidentChoose.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static AdventureIncidentChooseReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "Ch5BZHZlbnR1cmVfSW5jaWRlbnRDaG9vc2UucHJvdG8SBmRlcGxveSLEAQoY",
            "QWR2ZW50dXJlX0luY2lkZW50Q2hvb3NlEgoKAklkGAEgASgFEgwKBE5hbWUY",
            "AiABKAkSEAoIRGVzY3JpYmUYAyABKAkSDwoHVG9Ob2RlcxgEIAMoBRIVCg1U",
            "b05vZGVXZWlnaHRzGAUgAygFEg0KBVNjb3JlGAYgASgFEhQKDFNjb3JlVXNl",
            "VHlwZRgHIAEoBRIWCg5Db25kaXRpb25BcnJheRgIIAMoCRIXCg9Mb2dpY0V4",
            "cHJlc3Npb24YCSABKAkirgEKHEFkdmVudHVyZV9JbmNpZGVudENob29zZV9N",
            "YXASPgoFSXRlbXMYASADKAsyLy5kZXBsb3kuQWR2ZW50dXJlX0luY2lkZW50",
            "Q2hvb3NlX01hcC5JdGVtc0VudHJ5Gk4KCkl0ZW1zRW50cnkSCwoDa2V5GAEg",
            "ASgFEi8KBXZhbHVlGAIgASgLMiAuZGVwbG95LkFkdmVudHVyZV9JbmNpZGVu",
            "dENob29zZToCOAFiBnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Deploy.Adventure_IncidentChoose), global::Deploy.Adventure_IncidentChoose.Parser, new[]{ "Id", "Name", "Describe", "ToNodes", "ToNodeWeights", "Score", "ScoreUseType", "ConditionArray", "LogicExpression" }, null, null, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Deploy.Adventure_IncidentChoose_Map), global::Deploy.Adventure_IncidentChoose_Map.Parser, new[]{ "Items" }, null, null, null, new pbr::GeneratedClrTypeInfo[] { null, })
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class Adventure_IncidentChoose : pb::IMessage<Adventure_IncidentChoose> {
    private static readonly pb::MessageParser<Adventure_IncidentChoose> _parser = new pb::MessageParser<Adventure_IncidentChoose>(() => new Adventure_IncidentChoose());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Adventure_IncidentChoose> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Deploy.AdventureIncidentChooseReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Adventure_IncidentChoose() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Adventure_IncidentChoose(Adventure_IncidentChoose other) : this() {
      id_ = other.id_;
      name_ = other.name_;
      describe_ = other.describe_;
      toNodes_ = other.toNodes_.Clone();
      toNodeWeights_ = other.toNodeWeights_.Clone();
      score_ = other.score_;
      scoreUseType_ = other.scoreUseType_;
      conditionArray_ = other.conditionArray_.Clone();
      logicExpression_ = other.logicExpression_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Adventure_IncidentChoose Clone() {
      return new Adventure_IncidentChoose(this);
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

    /// <summary>Field number for the "ToNodes" field.</summary>
    public const int ToNodesFieldNumber = 4;
    private static readonly pb::FieldCodec<int> _repeated_toNodes_codec
        = pb::FieldCodec.ForInt32(34);
    private readonly pbc::RepeatedField<int> toNodes_ = new pbc::RepeatedField<int>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<int> ToNodes {
      get { return toNodes_; }
    }

    /// <summary>Field number for the "ToNodeWeights" field.</summary>
    public const int ToNodeWeightsFieldNumber = 5;
    private static readonly pb::FieldCodec<int> _repeated_toNodeWeights_codec
        = pb::FieldCodec.ForInt32(42);
    private readonly pbc::RepeatedField<int> toNodeWeights_ = new pbc::RepeatedField<int>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<int> ToNodeWeights {
      get { return toNodeWeights_; }
    }

    /// <summary>Field number for the "Score" field.</summary>
    public const int ScoreFieldNumber = 6;
    private int score_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Score {
      get { return score_; }
      set {
        score_ = value;
      }
    }

    /// <summary>Field number for the "ScoreUseType" field.</summary>
    public const int ScoreUseTypeFieldNumber = 7;
    private int scoreUseType_;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int ScoreUseType {
      get { return scoreUseType_; }
      set {
        scoreUseType_ = value;
      }
    }

    /// <summary>Field number for the "ConditionArray" field.</summary>
    public const int ConditionArrayFieldNumber = 8;
    private static readonly pb::FieldCodec<string> _repeated_conditionArray_codec
        = pb::FieldCodec.ForString(66);
    private readonly pbc::RepeatedField<string> conditionArray_ = new pbc::RepeatedField<string>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::RepeatedField<string> ConditionArray {
      get { return conditionArray_; }
    }

    /// <summary>Field number for the "LogicExpression" field.</summary>
    public const int LogicExpressionFieldNumber = 9;
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
      return Equals(other as Adventure_IncidentChoose);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Adventure_IncidentChoose other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Id != other.Id) return false;
      if (Name != other.Name) return false;
      if (Describe != other.Describe) return false;
      if(!toNodes_.Equals(other.toNodes_)) return false;
      if(!toNodeWeights_.Equals(other.toNodeWeights_)) return false;
      if (Score != other.Score) return false;
      if (ScoreUseType != other.ScoreUseType) return false;
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
      hash ^= toNodes_.GetHashCode();
      hash ^= toNodeWeights_.GetHashCode();
      if (Score != 0) hash ^= Score.GetHashCode();
      if (ScoreUseType != 0) hash ^= ScoreUseType.GetHashCode();
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
      toNodes_.WriteTo(output, _repeated_toNodes_codec);
      toNodeWeights_.WriteTo(output, _repeated_toNodeWeights_codec);
      if (Score != 0) {
        output.WriteRawTag(48);
        output.WriteInt32(Score);
      }
      if (ScoreUseType != 0) {
        output.WriteRawTag(56);
        output.WriteInt32(ScoreUseType);
      }
      conditionArray_.WriteTo(output, _repeated_conditionArray_codec);
      if (LogicExpression.Length != 0) {
        output.WriteRawTag(74);
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
      size += toNodes_.CalculateSize(_repeated_toNodes_codec);
      size += toNodeWeights_.CalculateSize(_repeated_toNodeWeights_codec);
      if (Score != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Score);
      }
      if (ScoreUseType != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(ScoreUseType);
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
    public void MergeFrom(Adventure_IncidentChoose other) {
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
      toNodes_.Add(other.toNodes_);
      toNodeWeights_.Add(other.toNodeWeights_);
      if (other.Score != 0) {
        Score = other.Score;
      }
      if (other.ScoreUseType != 0) {
        ScoreUseType = other.ScoreUseType;
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
          case 34:
          case 32: {
            toNodes_.AddEntriesFrom(input, _repeated_toNodes_codec);
            break;
          }
          case 42:
          case 40: {
            toNodeWeights_.AddEntriesFrom(input, _repeated_toNodeWeights_codec);
            break;
          }
          case 48: {
            Score = input.ReadInt32();
            break;
          }
          case 56: {
            ScoreUseType = input.ReadInt32();
            break;
          }
          case 66: {
            conditionArray_.AddEntriesFrom(input, _repeated_conditionArray_codec);
            break;
          }
          case 74: {
            LogicExpression = input.ReadString();
            break;
          }
        }
      }
    }

  }

  public sealed partial class Adventure_IncidentChoose_Map : pb::IMessage<Adventure_IncidentChoose_Map> {
    private static readonly pb::MessageParser<Adventure_IncidentChoose_Map> _parser = new pb::MessageParser<Adventure_IncidentChoose_Map>(() => new Adventure_IncidentChoose_Map());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Adventure_IncidentChoose_Map> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Deploy.AdventureIncidentChooseReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Adventure_IncidentChoose_Map() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Adventure_IncidentChoose_Map(Adventure_IncidentChoose_Map other) : this() {
      items_ = other.items_.Clone();
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Adventure_IncidentChoose_Map Clone() {
      return new Adventure_IncidentChoose_Map(this);
    }

    /// <summary>Field number for the "Items" field.</summary>
    public const int ItemsFieldNumber = 1;
    private static readonly pbc::MapField<int, global::Deploy.Adventure_IncidentChoose>.Codec _map_items_codec
        = new pbc::MapField<int, global::Deploy.Adventure_IncidentChoose>.Codec(pb::FieldCodec.ForInt32(8, 0), pb::FieldCodec.ForMessage(18, global::Deploy.Adventure_IncidentChoose.Parser), 10);
    private readonly pbc::MapField<int, global::Deploy.Adventure_IncidentChoose> items_ = new pbc::MapField<int, global::Deploy.Adventure_IncidentChoose>();
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public pbc::MapField<int, global::Deploy.Adventure_IncidentChoose> Items {
      get { return items_; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Adventure_IncidentChoose_Map);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Adventure_IncidentChoose_Map other) {
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
    public void MergeFrom(Adventure_IncidentChoose_Map other) {
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
