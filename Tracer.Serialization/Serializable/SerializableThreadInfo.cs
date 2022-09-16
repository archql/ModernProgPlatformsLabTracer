using lab1Tracer.Core;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace lab1Tracer.Serialization.Serializable
{
    public class SerializableThreadInfo
    {
        [XmlAttribute("thId")]
        [JsonInclude, JsonPropertyName("thId")]
        public int Id;

        [XmlAttribute("time(ms)")]
        [JsonInclude, JsonPropertyName("time(ms)")]

        public long Time;

        [XmlElement("methods")]
        [JsonInclude, JsonPropertyName("methods")]
        public List<SerializableMethodInfo> ChildMethods;

        public SerializableThreadInfo(ReadOnlyThreadInfo threadInfo)
        {
            ChildMethods = new List<SerializableMethodInfo>();

            Id = threadInfo.Id;
            Time = threadInfo.Time;

            foreach (var methodInfo in threadInfo.ChildMethods)
            {
                ChildMethods.Add(new SerializableMethodInfo(methodInfo));
            }
        }
        public SerializableThreadInfo()
        {
            ChildMethods = new List<SerializableMethodInfo>();
        }
    }
}
