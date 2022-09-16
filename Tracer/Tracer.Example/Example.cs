namespace lab1Tracer.Example
{
	using lab1Tracer.Core;
	using lab1Tracer.Serialization;
    using lab1Tracer.Serialization.Abstractions;

    public class Example
	{
		static void Main()
		{
			var example = new Example();

			var tracer = new Tracer();

			var foo = new Foo(tracer);
			var bar = new Bar(tracer);

			var thread1 = new Thread(() =>
			{
				foo.MyMethod();
			});
			thread1.Start();

			var thread2 = new Thread(() =>
			{
				bar.InnerMethod();
			});
			thread2.Start();

			thread1.Join();
			thread2.Join();

			var traceResult = tracer.GetTraceResult();

			List<ITraceResultSerializer> serializersList = new List<ITraceResultSerializer>();
            Loader.LoadSerializersFromPath("plugins", ref serializersList);
			//TraceResultSerializer.SerializeToFiles(serializers, traceResult, "notRes");
		}
	}

	public class Foo
	{
		private Bar _bar;
		private ITracer _tracer;

		public Foo(ITracer tracer)
		{
			_tracer = tracer;
			_bar = new Bar(_tracer);
		}

		public void MyMethod()
		{
			_tracer.StartTrace();
			Thread.Sleep(600);
			_tracer.StopTrace();

			_tracer.StartTrace();
			Thread.Sleep(200);
			_bar.InnerMethod();
			Thread.Sleep(200);
			_tracer.StopTrace();
		}
	}

	public class Bar
	{
		private ITracer _tracer;

		public Bar(ITracer tracer)
		{
			_tracer = tracer;
		}

		public void InnerMethod()
		{
			_tracer.StartTrace();
			Thread.Sleep(100);
			_tracer.StopTrace();
		}
	}
}