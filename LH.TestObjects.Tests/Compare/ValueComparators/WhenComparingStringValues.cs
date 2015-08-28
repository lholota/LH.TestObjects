namespace LH.TestObjects.Tests.Compare.ValueComparators
{
    using System;
    using System.Linq;
    using FluentAssertions;
    using TestDomain;
    using TestObjects.Compare;

    public class WhenComparingStringValues : ComparatorTestsBase
    {
        public void ThenShouldFailIfValuesDifferByCase()
        {
            var objA = SimpleDomain.CreateObjectWithValueSet1();
            var objB = SimpleDomain.CreateObjectWithValueSet1();

            objA.StringProp = objB.StringProp.ToLowerInvariant();

            var result = this.Comparator.Compare(objA, objB);

            result.AreSame.Should().BeFalse();
            result.Differences.Single().PropertyInfo.Name.Should().Be("StringProp");
        }

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