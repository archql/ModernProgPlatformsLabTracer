namespace lab1Tracer.Serialization.JSON
{
    using lab1Tracer.Core;
    using lab1Tracer.Serialization.Abstractions;
    using lab1Tracer.Serialization.Serializable;
    using System.IO;
    using System.Text.Json;

    public class JSONTraceResultSerializer : ITraceResultSerializer
    {
        public static readonly string TAG = "json";
        public string Format { get { return TAG; } }

        public void Serialize(TraceResult traceResult, Stream to)
        {
            JsonSerializer.Serialize(to, new SerializableTraceResult(traceResult), 
                                         new JsonSerializerOptions () { WriteIndented = true });
        }
    }
}