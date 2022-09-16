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

        [XmlAttribute("time")]
        [JsonInclude, JsonPropertyName("time")]

        public long Time;

        [XmlElement("methods")]
        [JsonInclude, JsonPropertyName("methods")]
        public List<SerializableMethodInfo> ChildMethods = new List<SerializableMethodInfo>();

        public SerializableThreadInfo(ReadOnlyThreadInfo threadInfo)
        {
            Id = threadInfo.Id;
            Time = threadInfo.Time;

            foreach (var methodInfo in threadInfo.ChildMethods)
            {
                ChildMethods.Add(new SerializableMethodInfo(methodInfo));
                Time += methodInfo.Time;
            }
        }
    }
}
