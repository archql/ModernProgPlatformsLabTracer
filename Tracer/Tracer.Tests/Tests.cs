using NUnit.Framework;

using lab1Tracer.Example;
using lab1Tracer.Core;
using System.Threading;

namespace lab1Tracer.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_SingleThread_ZeroObject()
        {
			// Arrange
			var tracer = new Tracer();

			// Act
			tracer.StartTrace();
			Thread.Sleep(600);
			tracer.StopTrace();
			var traceResult = tracer.GetTraceResult();

			// Assert (trace res)
			var thInfo = traceResult.ThreadsInfo;
			Assert.Multiple(() =>
			{
				Assert.That(thInfo.Count, Is.EqualTo(1));
				Assert.That(thInfo[0].Time, Is.InRange(600, 700));
				Assert.That(thInfo[0].ChildMethods.Count, Is.EqualTo(1));
			});	
		}
		[Test]
		public void Test_SingleThread_SingleObject()
		{
			// Arrange
			var tracer = new Tracer();

			var foo = new Foo(tracer);
			// Act
			foo.MyMethod();

			var traceResult = tracer.GetTraceResult();

			// Assert (trace res)
			var thInfo = traceResult.ThreadsInfo;
			Assert.That(thInfo.Count, Is.EqualTo(1));
			Assert.That(thInfo[0].ChildMethods.Count, Is.EqualTo(2));

			var th1Childs = thInfo[0].ChildMethods;
			Assert.Multiple(() =>
			{
				Assert.That(th1Childs[0].Time + th1Childs[1].Time, Is.InRange(1100, 1320)); // time sum
				Assert.That(th1Childs[0].Time, Is.InRange(600, 720)); // time for first
				Assert.That(th1Childs[0].ChildMethods.Count, Is.EqualTo(0)); // count of first
				Assert.That(th1Childs[1].ChildMethods.Count, Is.EqualTo(1)); // count of 2nd
				Assert.That(th1Childs[0].Name, Is.EqualTo(th1Childs[1].Name));
				Assert.That(th1Childs[0].Name, Is.EqualTo("MyMethod"));
				Assert.That(th1Childs[0].ClassName, Is.EqualTo(th1Childs[1].ClassName));
			});
		}
		[Test]
		public void Test_DualThread_DualObject()
		{
			// Arrange
			var tracer = new Tracer();

			var bar = new Bar(tracer);
			var foo = new Foo(tracer);
			// Act
			var thread1 = new Thread(() =>
			{
				bar.InnerMethod();
			});
			thread1.Start();

			Thread.Sleep(00);

			var thread2 = new Thread(() =>
			{
				foo.MyMethod();
				bar.InnerMethod();
			});
			thread2.Start();

			thread1.Join();
			thread2.Join();

			var traceResult = tracer.GetTraceResult();

			// Assert (trace res)
			var thInfo = traceResult.ThreadsInfo;
			Assert.That(thInfo.Count, Is.EqualTo(2));

			// check 1st chld
			var th1Childs = thInfo[0].ChildMethods;
			Assert.That(th1Childs.Count, Is.EqualTo(1));
			Assert.Multiple(() =>
			{
				Assert.That(th1Childs[0].Time, Is.InRange(100, 120)); 
				Assert.That(th1Childs[0].ChildMethods.Count, Is.EqualTo(0)); // count of first
				Assert.That(th1Childs[0].ClassName, Is.EqualTo("Bar"));
				Assert.That(th1Childs[0].Name, Is.EqualTo("InnerMethod"));
			});
			// check 2nd chld
			var th2Childs = thInfo[1].ChildMethods;
			Assert.That(th2Childs.Count, Is.EqualTo(3));
			Assert.Multiple(() =>
			{
				Assert.That(th2Childs[0].Time + th2Childs[1].Time + th2Childs[2].Time, Is.InRange(1200, 1440));
				Assert.That(th2Childs[0].Time, Is.InRange(600, 720));
				Assert.That(th2Childs[2].Time, Is.InRange(100, 120));
				Assert.That(th2Childs[0].ChildMethods.Count, Is.EqualTo(0)); // count of first
				Assert.That(th2Childs[2].ChildMethods.Count, Is.EqualTo(0)); // count of first
				Assert.That(th2Childs[0].ClassName, Is.EqualTo("Foo"));
				Assert.That(th2Childs[0].Name, Is.EqualTo("MyMethod"));
				Assert.That(th2Childs[1].ClassName, Is.EqualTo("Foo"));
				Assert.That(th2Childs[1].Name, Is.EqualTo("MyMethod"));
				Assert.That(th2Childs[2].ClassName, Is.EqualTo("Bar"));
				Assert.That(th2Childs[2].Name, Is.EqualTo("InnerMethod"));
			});
			// check 2nd2nd chlds
			var mth2childs = th2Childs[1].ChildMethods;
			Assert.That(mth2childs.Count, Is.EqualTo(1));
			Assert.Multiple(() =>
			{
				Assert.That(mth2childs[0].Time, Is.InRange(100, 120));
				Assert.That(mth2childs[0].ChildMethods.Count, Is.EqualTo(0)); // count of first
				Assert.That(mth2childs[0].ClassName, Is.EqualTo("Bar"));
				Assert.That(mth2childs[0].Name, Is.EqualTo("InnerMethod"));
			});
		}
	}
}