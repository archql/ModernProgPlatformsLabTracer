using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1Tracer.Core
{
    internal class Tracer : ITracer
    {
        ConcurrentDictionary<int, ThreadInfo> ThreadsDictionary = new ConcurrentDictionary<int, ThreadInfo>();

        public TraceResult GetTraceResult()
        {
            List<ReadOnlyThreadInfo> threads = new List<ReadOnlyThreadInfo>();
            foreach (var threadInfoPair in ThreadsDictionary)
            {
                threads.Add(new ReadOnlyThreadInfo(threadInfoPair.Key, threadInfoPair.Value));
            }
            return new TraceResult(threads);
        }

        public void StartTrace()
        {
            // get thread info for current method
            int curThreadId = Thread.CurrentThread.ManagedThreadId;
            var threadInfo = ThreadsDictionary.GetOrAdd(curThreadId, new ThreadInfo());
            var stopWatch = threadInfo.StopWatch;

            // get info about method above
            var stackMethod = new StackFrame(1).GetMethod();
            if (stackMethod == null)
                return;
            string methodName = stackMethod.Name;
            string className = stackMethod.ReflectedType != null ? stackMethod.ReflectedType.Name : "";

            threadInfo.RunningMethods.Push(
            new MethodInfo(
                    methodName,
                    className,
                    stopWatch.ElapsedMilliseconds
                ));
        }

        public void StopTrace()
        {
            int curThreadId = Thread.CurrentThread.ManagedThreadId;
            if (!ThreadsDictionary.ContainsKey(curThreadId))
            {
                // this case must not be happen!
                return;
            }
            var threadInfo = ThreadsDictionary[curThreadId];
            var methodInfo = threadInfo.RunningMethods.Pop();

            MethodInfo topMethodInfo;
            if (threadInfo.RunningMethods.TryPeek(out topMethodInfo))
            {
                // it has element, so assign to it as its child
                topMethodInfo.ChildMethods.Add(methodInfo);
            }
            else
            {
                // were already at the top
                threadInfo.ChildMethods.Add(methodInfo);
            }

            methodInfo.StopMeasuring(threadInfo.StopWatch.ElapsedMilliseconds);
        }
    }
}
