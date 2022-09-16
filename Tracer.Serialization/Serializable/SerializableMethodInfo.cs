using lab1Tracer.Core;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace lab1Tracer.Serialization.Serializable
{
    public class SerializableMethodInfo
    {
        [XmlAttribute("name")]
        [JsonInclude, JsonPropertyName("name")]
        public string Name;

        [XmlAttribute("className")]
        [JsonInclude, JsonPropertyName("class")]
        public string ClassName;

        [XmlAttribute("time(ms)")]
        [JsonInclude, JsonPropertyName("time(ms)")]
        public long Time;

        [XmlElement("methods")]
        [JsonInclude, JsonPropertyName("methods")]
        public List<SerializableMethodInfo> ChildMethods = new List<SerializableMethodInfo>();

        public SerializableMethodInfo(ReadOnlyMethodInfo methodInfo)
        {
            Name = methodInfo.Name;
            ClassName = methodInfo.ClassName;
            Time = methodInfo.Time;
            foreach (var childMethodInfo in methodInfo.ChildMethods)
            {
                ChildMethods.Add(new SerializableMethodInfo(childMethodInfo));
            }
        }

        public SerializableMethodInfo()
        {
            Name = "";
            ClassName = "";
            Time = 0;
        }
    }
}
