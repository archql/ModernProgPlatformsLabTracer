namespace lab1Tracer.Serialization.XML
{
    using lab1Tracer.Core;
    using lab1Tracer.Serialization.Abstractions;
    using System.IO;

    public class XMLTraceResultSerializer : ITraceResultSerializer
    {
        public static readonly string TAG = "xml";
        public string Format { get { return TAG; } }

        public void Serialize(TraceResult traceResult, Stream to)
        {
            throw new NotImplementedException();
        }
    }
}