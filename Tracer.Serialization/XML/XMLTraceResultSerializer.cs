namespace lab1Tracer.Serialization.XML
{
    using lab1Tracer.Core;
    using lab1Tracer.Serialization.Abstractions;
    using lab1Tracer.Serialization.Serializable;
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;

    public class XMLTraceResultSerializer : ITraceResultSerializer
    {
        public static readonly string TAG = "xml";
        public string Format { get { return TAG; } }

        public void Serialize(TraceResult traceResult, Stream to)
        {
            using var xmlWriter = XmlWriter.Create(to, new XmlWriterSettings { Indent = true });
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(SerializableTraceResult));
            xmlSerializer.Serialize(xmlWriter, new SerializableTraceResult(traceResult));
        }
    }
}