using System.Diagnostics;

namespace lab1Tracer.Core
{
    public struct ThreadInfo
    {
        public Stopwatch StopWatch = new Stopwatch();
        public Stack<MethodInfo> RunningMethods = new Stack<MethodInfo>();
        public List<MethodInfo> ChildMethods = new List<MethodInfo>();
        public ThreadInfo()
        {
            StopWatch.Start();
        }
    }
    public class ReadOnlyThreadInfo
    {
        public int Id { get; private set; }
        public long Time { get; private set; }
        public IReadOnlyList<ReadOnlyMethodInfo> ChildMethods { get; private set; }
        public ReadOnlyThreadInfo(int id, ThreadInfo threadInfo) 
        {
            Id = id;
            Time = 0L;

            List<ReadOnlyMethodInfo> methods = new List<ReadOnlyMethodInfo>();
            foreach (var methodInfo in threadInfo.ChildMethods)
            {
                methods.Add(new ReadOnlyMethodInfo(methodInfo));
                Time += methodInfo.Time;
            }
            ChildMethods = methods;
        }
    }
}
