namespace lab1Tracer.Core
{
    public interface ITracer
    {
        // вызывается в начале замеряемого метода
        public void StartTrace();

        // вызывается в конце замеряемого метода
        public void StopTrace();

        // получить результаты измерений
        TraceResult GetTraceResult();
    }
}
