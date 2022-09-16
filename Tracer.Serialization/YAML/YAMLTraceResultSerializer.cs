namespace lab1Tracer.Serialization.YAML
{
    using lab1Tracer.Core;
    using lab1Tracer.Serialization.Abstractions;
    using lab1Tracer.Serialization.Serializable;
    using System.IO;
    using YamlDotNet.Serialization;
    using YamlDotNet.Serialization.NamingConventions;

    public class YAMLTraceResultSerializer : ITraceResultSerializer
    {
        public static readonly string TAG = "yaml";
        public string Format { get { return TAG; } }

        public void Serialize(TraceResult traceResult, Stream to)
        {
            var serializer = new SerializerBuilder()
                           .WithNamingConvention(CamelCaseNamingConvention.Instance)
                           .Build();

            var yaml = serializer.Serialize(new SerializableTraceResult(traceResult));

            using var sw = new StreamWriter(to);
            sw.Write(yaml);
            sw.Flush();
        }
    }
}