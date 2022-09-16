namespace lab1Tracer.Serialization.YAML
{
    using lab1Tracer.Core;
    using lab1Tracer.Serialization.Abstractions;
    using System.IO;

    public class YAMLTraceResultSerializer : ITraceResultSerializer
    {
        public static readonly string TAG = "yaml";
        public string Format { get { return TAG; } }

        public void Serialize(TraceResult traceResult, Stream to)
        {
            throw new NotImplementedException();
        }
    }
}