namespace lab1Tracer.Serialization.JSON
{
    using lab1Tracer.Core;
    using lab1Tracer.Serialization.Abstractions;
    using System.IO;
    using System.Text.Json;

    public class JSONTraceResultSerializer : ITraceResultSerializer
    {
        public static readonly string TAG = "json";
        public string Format { get { return TAG; } }

        public void Serialize(TraceResult traceResult, Stream to)
        {
            List<ThreadInfo> threadsInfo = new List<ThreadInfo>();
            foreach (var thread in traceResult.ThreadsInfo)
            {
               
            }
            

            
        }
    }
}