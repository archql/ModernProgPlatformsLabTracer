namespace lab1Tracer.Core
{
    public class TraceResult
    {
        public IReadOnlyList<ReadOnlyThreadInfo> ThreadsInfo { get; }

        public TraceResult(IReadOnlyList<ReadOnlyThreadInfo> threadsInfo)
        {
            ThreadsInfo = threadsInfo;
        }
    }
}
