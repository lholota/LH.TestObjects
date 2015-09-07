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
        public void ThenContextShouldHaveValuesWhenLoggingToCallback()
        {
            var hasBeenCalled = false;
            var stringPropChecked = false;
            var objA = SimpleDomain.CreateObjectWithValueSet1();
            var objB = SimpleDomain.CreateObjectWithValueSet2();

            Action<LogEvent> callback = ctx =>
            {
                hasBeenCalled = true;

                ctx.Message.Should().NotBeNullOrEmpty();
                ctx.Comparison.Should().NotBeNull();
                ctx.Comparison.PropertyInfo.Should().NotBeNull();
                ctx.Comparison.ActualValue.Should().NotBeNull();
                ctx.Comparison.ExpectedValue.Should().NotBeNull();

                if (ctx.Comparison.PropertyInfo.Name == nameof(objA.StringProp))
                {
                    stringPropChecked = true;

                    ctx.Comparison.ExpectedValue.Should().Be(objA.StringProp);
                    ctx.Comparison.ActualValue.Should().Be(objB.StringProp);
                }
            };

            this.Comparator.Log.Callback(callback);
            this.Comparator.Compare(objA, objB);

            hasBeenCalled.Should().BeTrue();
            stringPropChecked.Should().BeTrue();
        }
    }
}