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
                ctx.Context.Should().NotBeNull();
                ctx.Context.PropertyInfo.Should().NotBeNull();
                ctx.Context.ActualValue.Should().NotBeNull();
                ctx.Context.ExpectedValue.Should().NotBeNull();

                if (ctx.Context.PropertyInfo.Name == nameof(objA.StringProp))
                {
                    stringPropChecked = true;

                    ctx.Context.ExpectedValue.Should().Be(objA.StringProp);
                    ctx.Context.ActualValue.Should().Be(objB.StringProp);
                }
            };

            this.Comparator.Log.Callback(callback);
            this.Comparator.Compare(objA, objB);

            hasBeenCalled.Should().BeTrue();
            stringPropChecked.Should().BeTrue();
        }
    }
}