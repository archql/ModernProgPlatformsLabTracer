using lab1Tracer.Core;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace lab1Tracer.Serialization.Serializable
{
    [XmlRoot("root")]
    public class SerializableTraceResult
    {
        [XmlElement("ThreadsInfo")]
        [JsonInclude, JsonPropertyName("threads")]
        public List<SerializableThreadInfo> ThreadsInfo;

        public SerializableTraceResult(TraceResult traceResult)
        {
            ThreadsInfo = new List<SerializableThreadInfo>();
            foreach (var thInfo in traceResult.ThreadsInfo)
            {
                ThreadsInfo.Add(new SerializableThreadInfo(thInfo));
            }
        }
        public SerializableTraceResult()
        {
            ThreadsInfo = new List<SerializableThreadInfo>();
        }
    }
}
