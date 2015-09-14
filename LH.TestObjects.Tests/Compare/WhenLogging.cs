namespace LH.TestObjects.Tests.Compare
{
    using System;
    using Domain;
    using FluentAssertions;
    using NUnit.Framework;
    using TestObjects.Compare.Logging;

    [TestFixture]
    public class WhenLogging : ComparatorTestsBase
    {
        [Test]
        public void ThenShouldCallCallbackWhenConfigured()
        {
            var objA = SimpleDomain.CreateObjectWithValueSet1();
            var objB = SimpleDomain.CreateObjectWithValueSet2();

            var hasBeenCalled = false;
            Action<LogEvent> callback = x => hasBeenCalled = true;

            this.Comparator.Log.Callback(callback);
            this.Comparator.Compare(objA, objB);

            hasBeenCalled.Should().BeTrue();
        }

        [Test]
        public void ThenComparisonShouldHaveValuesWhenLoggingToCallback()
        {
            var hasBeenCalled = false;
            var stringPropChecked = false;
            var objA = SimpleDomain.CreateObjectWithValueSet1();
            var objB = SimpleDomain.CreateObjectWithValueSet2();

            Action<LogEvent> callback = evt =>
            {
                hasBeenCalled = true;

                evt.Message.Should().NotBeNullOrEmpty();
                evt.Comparison.Should().NotBeNull();
                evt.Comparison.PropertyName.Should().NotBeNullOrEmpty();
                evt.Comparison.ActualValue.Should().NotBeNull();
                evt.Comparison.ExpectedValue.Should().NotBeNull();

                if (evt.Comparison.PropertyName == nameof(objA.StringProp))
                {
                    stringPropChecked = true;

                    evt.Message.Should().StartWith("StringProp:");
                    evt.Comparison.ExpectedValue.Should().Be(objA.StringProp);
                    evt.Comparison.ActualValue.Should().Be(objB.StringProp);
                }
            };

            this.Comparator.Log.Callback(callback);
            this.Comparator.Compare(objA, objB);

            hasBeenCalled.Should().BeTrue();
            stringPropChecked.Should().BeTrue();
        }
    }
}