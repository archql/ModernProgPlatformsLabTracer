using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1Tracer.Core
{
    struct ThreadInfo
    {
        public Stopwatch StopWatch = new Stopwatch();
        public Stack<MethodInfo> RunningMethods = new Stack<MethodInfo>();
        public List<MethodInfo> ChildMethods = new List<MethodInfo>();
        public ThreadInfo()
        {
            StopWatch.Start();
        }
    }
    internal class ReadOnlyThreadInfo
    {
        public int Id;
        public long Time;
        public IReadOnlyList<ReadOnlyMethodInfo> ChildMethods;
        public ReadOnlyThreadInfo(int id, ThreadInfo threadInfo) {
            Id = id;

            List<ReadOnlyMethodInfo> methods = new List<ReadOnlyMethodInfo>();
            foreach (var methodInfo in threadInfo.ChildMethods)
            {
                methods.Add(new ReadOnlyMethodInfo(methodInfo));
            }
            ChildMethods = methods;
        }
    }
}
