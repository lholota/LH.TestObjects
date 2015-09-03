namespace LH.TestObjects.Tests.Compare.ValueComparators
{
    using System;
    using System.Linq;
    using Domain;
    using FluentAssertions;
    using NUnit.Framework;
    using TestObjects.Compare;

    [TestFixture]
    public class WhenComparingStringValues : ComparatorTestsBase
    {
        [Test]
        public void ThenShouldFailIfValuesDifferByCase()
        {
            var objA = SimpleDomain.CreateObjectWithValueSet1();
            var objB = SimpleDomain.CreateObjectWithValueSet1();

            objA.StringProp = objB.StringProp.ToLowerInvariant();

            var result = this.Comparator.Compare(objA, objB);

            result.AreSame.Should().BeFalse();
            result.Differences.Single().PropertyInfo.Name.Should().Be("StringProp");
        }

        [Test]
        public void ThenShouldPassIfValuesDifferByCaseAndOptionsSetToInsensitive()
        {
            var objA = SimpleDomain.CreateObjectWithValueSet1();
            var objB = SimpleDomain.CreateObjectWithValueSet1();

            objA.StringProp = objB.StringProp.ToLowerInvariant();

            this.Comparator.Property(x => x.StringProp)
                .WithComparisonType(StringComparison.OrdinalIgnoreCase);

            var result = this.Comparator.Compare(objA, objB);

            result.AreSame.Should().BeTrue();
        }
    }
}