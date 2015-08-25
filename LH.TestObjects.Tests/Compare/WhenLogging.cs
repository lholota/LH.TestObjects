namespace LH.TestObjects.Tests.Compare
{
    using System;
    using FluentAssertions;
    using NUnit.Framework;
    using TestDomain;
    using TestObjects.Compare;

    [TestFixture]
    public class WhenLogging
    {
        private ObjectComparator<SimpleDomain> comparator;
        
        [SetUp]
        public void Init()
        {
            this.comparator = new ObjectComparator<SimpleDomain>();
        }

        [Test]
        public void ThenShouldLogIntoConsoleWhenConfigured()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void ThenShouldCallCallbackWhenConfigured()
        {
            var hasBeenCalled = false;
            Action<object> callback = x => hasBeenCalled = true;

            this.comparator.Log.Callback(callback);

            hasBeenCalled.Should().BeTrue();
        }

        [Test]
        public void ThenContextShouldHaveValuesWhenLoggingToCallback()
        {
            throw new NotImplementedException();
        }
    }
}
