using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1Tracer.Core
{
    internal class TraceResult
    {
        public IReadOnlyList<ReadOnlyThreadInfo> ThreadsInfo { get; }

        public TraceResult(IReadOnlyList<ReadOnlyThreadInfo> threadsInfo)
        {
            ThreadsInfo = threadsInfo;
        }
    }
}
