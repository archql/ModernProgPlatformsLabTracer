using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1Tracer.Core
{
    interface ITracer
    {
        // вызывается в начале замеряемого метода
        public void StartTrace();

        // вызывается в конце замеряемого метода
        public void StopTrace();

        // получить результаты измерений
        TraceResult GetTraceResult();
    }
}
