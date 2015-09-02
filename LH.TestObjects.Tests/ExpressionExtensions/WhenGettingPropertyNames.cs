namespace LH.TestObjects.Tests.ExpressionExtensions
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using FluentAssertions;
    using NUnit.Framework;
    using TestDomain;

    [TestFixture]
    public class WhenGettingPropertyNames
    {
        [Test]
        public void ThenShouldGetOneNameIfExpressionIsOneProperty()
        {
            Expression<Func<SimpleDomain, string>> expr = x => x.StringProp;
            var propertyNames = expr.GetPropertyNames().ToArray();

            propertyNames.Should().NotBeNull();
            propertyNames.Length.Should().Be(1);
            propertyNames[0].Should().Be("StringProp");
        }

        [Test]
        public void ThenShouldGetAllNamesInOrderIfExpressionHasNestedProperties()
        {
            Expression<Func<DummyRootClass, int>> expr = x => x.FirstNested.DummyProp;
            var propertyNames = expr.GetPropertyNames().ToArray();

            propertyNames.Should().NotBeNull();
            propertyNames.Length.Should().Be(2);
            propertyNames[0].Should().Be("DummyProp");
            propertyNames[1].Should().Be("FirstNested");
        }

        
        [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
        [SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty")]
        private class DummyRootClass
        {
            public DummyFirstNested FirstNested { get; }
        }

        [SuppressMessage("ReSharper", "ClassNeverInstantiated.Local")]
        [SuppressMessage("ReSharper", "UnassignedGetOnlyAutoProperty")]
        private class DummyFirstNested
        {
            public int DummyProp { get; } 
        }
    }
}
