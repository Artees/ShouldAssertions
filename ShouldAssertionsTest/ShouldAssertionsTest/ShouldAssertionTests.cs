using System;
using System.Collections.Generic;
using System.Linq;
using Artees.BDD;
using NUnit.Framework;

namespace ShouldAssertionsTest
{
    [TestFixture]
    public class ShouldAssertionTests
    {
        [SetUp]
        public void SetUp()
        {
            _listeners = ShouldAssertions.Listeners.ToList();
            ShouldAssertions.Listeners.Clear();
            ShouldAssertions.Listeners.Add(_nUnitListener);
        }

        [TearDown]
        public void TearDown()
        {
            ShouldAssertions.Listeners.Clear();
            ShouldAssertions.Listeners.AddRange(_listeners);
        }

        private readonly ShouldListener _nUnitListener = new NUnitShouldListener(),
            _exceptionListener = new ExceptionShouldListener();

        private List<ShouldListener> _listeners;

        private static void NullAction()
        {
        }

        private static void ThrowShouldException()
        {
            3.Should().BeEqual(4);
        }

        private static void ThrowShouldThrowException()
        {
            Action throwShouldException = NullAction;
            throwShouldException.Should().Throw<ShouldException>();
        }

        private static void ThrowShouldNotThrowException()
        {
            Action throwShouldException = ThrowShouldException;
            throwShouldException.Should().Not().Throw<ShouldException>();
        }

        private static void ThrowShouldBeNullException()
        {
            3.Should().BeNull();
        }

        [Test]
        public void TestBool()
        {
            true.Should().BeTrue();
            false.Should().BeFalse();
            false.Should().Not().BeTrue();
            true.Should().Not().BeFalse();
            3.Should().Not().BeTrue();
            3.Should().Not().BeFalse();
        }

        [Test]
        public void TestCompare()
        {
            3.Should().BeGreaterThan(2);
            3.Should().Not().BeGreaterThan(4);
            3.Should().Not().BeGreaterThan(3);
            3.7f.Should().BeGreaterThan(2.7f);
            3.7f.Should().Not().BeGreaterThan(3.8f);
            Action throwException = () => 3.7f.Should().BeGreaterThan(3.8f);
            throwException.Should().Throw<AssertionException>();
            3.7.Should().BeGreaterThan(2.7);
            3.7.Should().Not().BeGreaterThan(3.8);
            "car".Should().BeGreaterThan("bag");
            ((string) null).Should().Not().BeGreaterThan("bag");
            throwException = () => "car".Should().BeGreaterThan((string) null);
            throwException.Should().Throw<AssertionException>();
            3.Should().BeLessThan(4);
            3.Should().Not().BeLessThan(2);
            3.Should().Not().BeLessThan(3);
            3.7f.Should().BeLessThan(3.8f);
            3.7f.Should().Not().BeLessThan(2.7f);
            throwException = () => 3.7f.Should().BeLessThan(2.7f);
            throwException.Should().Throw<AssertionException>();
            3.7.Should().BeLessThan(3.8);
            3.7.Should().Not().BeLessThan(2.7);
            "bag".Should().BeLessThan("car");
            ((string) null).Should().BeLessThan("bag");
            "car".Should().Not().BeLessThan((string) null);
            throwException = () => "car".Should().BeLessThan((string) null);
            throwException.Should().Throw<AssertionException>();
            3.Should().BeGreaterThanOrEqual(2);
            3.Should().BeGreaterThanOrEqual(3);
            3.Should().Not().BeGreaterThanOrEqual(4);
            3.Should().BeLessThanOrEqual(4);
            3.Should().BeLessThanOrEqual(3);
            3.Should().Not().BeLessThanOrEqual(2);
        }

        [Test]
        public void TestContains()
        {
            new[] {1, 2, 7}.Aka<int>("Array").Should().Contains(2);
            new[] {1, 2, 7}.Aka<int>(() => "Array").Should().Not().Contains(3);
            Action throwException = () => new[] {1, 2, 7}.Should<int>().Contains(3);
            throwException.Should().Throw<AssertionException>();
            ((int[]) null).Should<int>().Not().Contains(2);
            throwException = () => ((int[]) null).Should<int>().Contains(2);
            throwException.Should().Throw<AssertionException>();
            new[] {1, 2, 7}.Should().Contains(7);
            throwException = () => 2.Should().Contains(7);
            throwException.Should().Throw<AssertionException>();
            "Test string".Aka("String").Should().Contains('s');
            "Test string".Aka(() => "String").Should().Not().Contains('n');
            "Test string".Should().Contains("st str");
            "Test string".Should().Not().Contains("no");
            "Test string".Should().Not().Contains(null);
            ((string) null).Should().Not().Contains("st str");
            throwException = () => "Test string".Should().Contains("no");
            throwException.Should().Throw<AssertionException>();
            throwException = () => ((string) null).Should().Contains("st str");
            throwException.Should().Throw<AssertionException>();
            throwException = () => "Test string".Should().Contains(null);
            throwException.Should().Throw<AssertionException>();
            ShouldAssertions.Listeners.Clear();
            2.Should().Contains(7);
        }

        [Test]
        public void TestDispose()
        {
            ShouldAssertions.Listeners.Remove(_nUnitListener);
            Action a = ThrowShouldException;
            using (var listener = new NUnitShouldListener())
            {
                ShouldAssertions.Listeners.Add(listener);
                a.Should().Throw<AssertionException>();
            }

            a.Should().Not().Throw<AssertionException>();
        }

        [Test]
        public void TestEqual()
        {
            3.Should().BeEqual(3);
            3.Aka("Three").Should().BeEqual(3);
            3.Aka(() => "Three").Should().BeEqual(3);
            3.Should().Not().BeEqual(4);
            3.7f.Should().BeEqual(3.7f);
            3.7f.Aka("3.7f").Should().BeEqual(3.7f);
            3.7f.Aka(() => "3.7f").Should().BeEqual(3.701f, 0.1f);
            3.7f.Should().Not().BeEqual(3.801f, 0.1f);
            Action throwException = () => 3.7f.Should().BeEqual(3.801f, 0.1f);
            throwException.Should().Throw<AssertionException>();
            3.7.Should().BeEqual(3.7);
            3.7.Aka("3.7").Should().BeEqual(3.7);
            3.7.Aka(() => "3.7").Should().BeEqual(3.701, 0.1);
            3.7.Should().Not().BeEqual(3.801, 0.1);
            throwException = () => 3.7.Should().BeEqual(3.801, 0.1);
            throwException.Should().Throw<AssertionException>();
        }

        [Test]
        public void TestException()
        {
            Action throwShouldException = NullAction;
            throwShouldException.Should().Not().Throw<Exception>();
            throwShouldException = ThrowShouldException;
            throwShouldException.Should().Not().Throw<ShouldException>();
            ShouldAssertions.Listeners.Clear();
            ShouldAssertions.Listeners.Add(_exceptionListener);
            throwShouldException.Should().Throw<ShouldException>();
            throwShouldException.Aka("Throw action").Should().Throw<ShouldException>();
            throwShouldException.Aka(() => "Throw action").Should().Throw<ShouldException>();
            throwShouldException = ThrowShouldThrowException;
            throwShouldException.Should().Throw<ShouldException>();
            throwShouldException = ThrowShouldNotThrowException;
            throwShouldException.Should().Throw<ShouldException>();
            ShouldAssertions.Listeners.Clear();
            ThrowShouldException();
            ThrowShouldThrowException();
            ThrowShouldNotThrowException();
        }

        [Test]
        public void TestFail()
        {
            Action fail = () => ShouldAssertions.Fail();
            fail.Should().Throw<AssertionException>();
            Action pending = () => ShouldAssertions.LogPendingTest();
            pending.Should().Throw<IgnoreException>();
        }

        [Test]
        public void TestInstance()
        {
            2.Should().BeInstanceOf<int>();
            2.Should().Not().BeInstanceOf<float>();
        }

        [Test]
        public void TestNaN()
        {
            (0.0f / 0.0f).Should().BeNaN();
            (0.0f / 0.1f).Should().Not().BeNaN();
            (0.0 / 0.0).Should().BeNaN();
            (0.0 / 0.1).Should().Not().BeNaN();
            Action action = () => 3.7f.Should().BeNaN();
            action.Should().Throw<AssertionException>();
            action = () => 3.7.Should().BeNaN();
            action.Should().Throw<AssertionException>();
            ShouldAssertions.Listeners.Clear();
            3.7f.Should().BeNaN();
        }

        [Test]
        public void TestNull()
        {
            ((object) null).Should().BeNull();
            3.Should().Not().BeNull();
            Action throwShouldBeNullException = ThrowShouldBeNullException;
            throwShouldBeNullException.Should().Throw<AssertionException>();
            ShouldAssertions.Listeners.Clear();
            ThrowShouldBeNullException();
        }

        [Test]
        public void TestPending()
        {
            Action action = () => 3.Should("be greater than 2");
            action.Should().Throw<IgnoreException>();
            action = () => 3.Should().Not("be greater than 4");
            action.Should().Throw<IgnoreException>();
            action = () => 3.Aka("Three").Should("be greater than 2");
            action.Should().Throw<IgnoreException>();
            ShouldAssertions.Listeners.Clear();
            3.Should("be greater than 2");
            3.Should().Not("be greater than 4");
            3.Aka("Three").Should("be greater than 2");
            ShouldAssertions.Listeners.Add(_exceptionListener);
            3.Should("be greater than 2");
        }

        [Test]
        public void TestSame()
        {
            var o0 = new object();
            var o1 = o0;
            var o2 = new object();
            o0.Should().BeSame(o1);
            o0.Should().Not().BeSame(o2);
            3.7f.Should().Not().BeSame(3.7f);
            Action action = () => o0.Should().BeSame(o2);
            action.Should().Throw<AssertionException>();
        }
    }
}