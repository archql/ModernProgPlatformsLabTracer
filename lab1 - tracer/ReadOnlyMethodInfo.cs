using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1Tracer.Core
{
    struct MethodInfo
    {
        public string Name;
        public string ClassName;
        public long Time;

        public List<MethodInfo> ChildMethods = new List<MethodInfo>();

        public MethodInfo(string name, string className, long timeStart)
        {
            Name = name;
            ClassName = className;
            Time = -timeStart;
        }

        public void StopMeasuring(long timeStop)
        {
            Time += timeStop;
        }
    }
    internal class ReadOnlyMethodInfo
    {
        public string Name { get; private set; }
        public string ClassName { get; private set; }
        public long Time { get; private set; }
        public IReadOnlyList<ReadOnlyMethodInfo> ChildMethods { get; private set; }

        public ReadOnlyMethodInfo(MethodInfo methodInfo)
        {
            Name = methodInfo.Name;
            ClassName = methodInfo.ClassName;
            Time = 0;

            List<ReadOnlyMethodInfo> innerMethods = new List<ReadOnlyMethodInfo>();
            foreach (var childMethodInfo in methodInfo.ChildMethods)
            {
                innerMethods.Add(new ReadOnlyMethodInfo(childMethodInfo));
                Time += childMethodInfo.Time;
            }
            ChildMethods = innerMethods;
        }
    }
}
