namespace lab1Tracer.Serialization.Abstractions
{
    using lab1Tracer.Core;
    public interface ITraceResultSerializer
    {
        // Опционально: возвращает формат, используемый сериализатором (xml/json/yaml).
        // Может быть удобно для выбора имени файлов (см. ниже).
        string Format { get; }
        void Serialize(TraceResult traceResult, Stream to);
    }
}