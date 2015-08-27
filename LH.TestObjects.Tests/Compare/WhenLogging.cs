namespace LH.TestObjects.Tests.Compare
{
    using System;
    using FluentAssertions;
    using NUnit.Framework;
    using TestObjects.Compare;

    [TestFixture]
    public class WhenLogging : ComparatorTestsBase
    {
        [Test]
        public void ThenShouldLogIntoConsoleWhenConfigured()
        {
            // TODO: Create an internal Console mock call?
            // Use moles?
            throw new NotImplementedException();
        }

        [Test]
        public void ThenShouldCallCallbackWhenConfigured()
        {
            var hasBeenCalled = false;
            Action<IComparisonContext> callback = x => hasBeenCalled = true;

            this.Comparator.Log.Callback(callback);
            this.Comparator.Compare(this.ObjA, this.ObjB);

            hasBeenCalled.Should().BeTrue();
        }

        [Test]
        public void ThenContextShouldHaveValuesWhenLoggingToCallback()
        {
            var hasBeenCalled = false;
            var stringPropChecked = false;

            Action<IComparisonContext> callback = ctx =>
            {
                hasBeenCalled = true;

                ctx.PropertyInfo.Should().NotBeNull();
                ctx.ExpectedValue.Should().NotBeNull();
                ctx.ActualValue.Should().NotBeNull();
                ctx.LogMessage.Should().NotBeNull();

                if (ctx.PropertyInfo.Name == "StringProperty")
                {
                    stringPropChecked = true;

                    ctx.ExpectedValue.Should().Be(this.ObjA.StringProp);
                    ctx.ActualValue.Should().Be(this.ObjB.StringProp);
                    ctx.LogMessage.Should().NotBeNullOrEmpty();
                }
            };

            this.Comparator.Log.Callback(callback);
            this.Comparator.Compare(this.ObjA, this.ObjB);

            hasBeenCalled.Should().BeTrue();
            stringPropChecked.Should().BeTrue();

            //x.Should().NotBeNull();
            //x.PropertyInfo.Should().NotBeNull();

            throw new NotImplementedException();
        }
    }
}