using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ProtoBuf;

namespace EventSource
{
    
    enum EventType
    {
        Create,
        Change
    }

    [ProtoContract(SkipConstructor = true)]
    class Event
    {
        [ProtoMember(99)]
        [JsonConverter(typeof(StringEnumConverter))]
        public EventType Type;

        [ProtoMember(100)]
        public string EntityName;
    }
}
