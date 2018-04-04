using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Artees.Diagnostics.BDD;
using NUnit.Framework;

namespace ShouldAssertionsTest
{
    [TestFixture]
    public class ShouldAssertionTracingTest : TraceListener
    {
        private string _trace = string.Empty;

        public override void Write(string message)
        {
            _trace = message;
        }

        public override void WriteLine(string message)
        {
            Write(message);
        }

        private List<ShouldListener> _listeners;

        [SetUp]
        public void SetUp()
        {
            _listeners = ShouldAssertions.Listeners.ToList();
            ShouldAssertions.Clear();
            Trace.Listeners.Add(this);
        }
        
        [Test]
        public void TestEmptyWarningShouldListenerTrace()
        {
            true.Should().BeFalse();
            var listener = new NUnitShouldListener();
            ShouldAssertions.Listeners.Add(listener);
            _trace.Should().Not().BeEqual(string.Empty);
        }
        
        [Test]
        public void TestEmptyWarningShouldListenerDontTrace()
        {
            var exceptionListener = new ExceptionShouldListener();
            ShouldAssertions.Listeners.Add(exceptionListener);
            try
            {
                true.Should().BeFalse();
            }
            catch (ShouldException)
            {
            }
            finally
            {
                ShouldAssertions.Listeners.Remove(exceptionListener);
            }
            var listener = new NUnitShouldListener();
            ShouldAssertions.Listeners.Add(listener);
            _trace.Should().BeEqual(string.Empty);
        }
        
        [Test]
        public void TestEmptyWarningShouldListenerPending()
        {
            true.Should("be false");
            var listener = new NUnitShouldListener();
            ShouldAssertions.Listeners.Add(listener);
            _trace.Should().Not().BeEqual(string.Empty);
        }

        [Test]
        public void TestTraceShouldListener()
        {
            _trace.Should().BeEqual(string.Empty);
            ShouldAssertions.Listeners.Clear();
            ShouldAssertions.Listeners.Add(new TraceShouldListener());
            3.Should().BeEqual(2);
            ShouldAssertions.Listeners.Add(new NUnitShouldListener());
            _trace.Should().Contains("should");
            _trace = string.Empty;
            ShouldAssertions.Listeners.Clear();
            ShouldAssertions.Listeners.Add(new TraceShouldListener());
            3.Should("be equal 2");
            ShouldAssertions.Listeners.Add(new NUnitShouldListener());
            _trace.Should().Contains("should");
        }

        [Test]
        public void TestWarninigShouldListener()
        {
            _trace.Should().BeEqual(string.Empty);
            ShouldAssertions.Listeners.Clear();
            ShouldAssertions.Listeners.Add(new WarningShouldListener());
            3.Should().BeEqual(2);
            ShouldAssertions.Listeners.Add(new NUnitShouldListener());
            _trace.Should().Contains("should");
            _trace = string.Empty;
            ShouldAssertions.Listeners.Clear();
            ShouldAssertions.Listeners.Add(new WarningShouldListener());
            3.Should("be equal 2");
            ShouldAssertions.Listeners.Add(new NUnitShouldListener());
            _trace.Should().Contains("should");
        }

        [Test]
        public void TestNull()
        {
            ShouldAssertions.Listeners.Add(new TraceShouldListener());
            const string name = "Null object";
            ((object) null).Aka(name).Should().Not().BeNull();
            ShouldAssertions.Listeners.Add(new NUnitShouldListener());
            _trace.Should().Contains(name);
        }

        [TearDown]
        public void TearDown()
        {
            ShouldAssertions.Listeners.Clear();
            ShouldAssertions.Listeners.AddRange(_listeners);
            Trace.Listeners.Remove(this);
            _trace = string.Empty;
        }
    }
}