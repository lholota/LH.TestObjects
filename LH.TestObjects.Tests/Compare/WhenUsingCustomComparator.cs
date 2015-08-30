namespace LH.TestObjects.Tests.Compare
{
    using FluentAssertions;
    using NUnit.Framework;
    using TestDomain;

    [TestFixture]
    public class WhenUsingCustomComparator : ComparatorTestsBase
    {
        [Test]
        public void ThenShouldPassWhenComparisonAlwaysReturnsTrueAndObjectsAreDifferent()
        {
            var objA = SimpleDomain.CreateObjectWithValueSet1();
            var objB = SimpleDomain.CreateObjectWithValueSet1();

            objA.StringProp = "AAA";
            objB.StringProp = "BBB";

            this.Comparator
                .Property(x => x.StringProp)
                .CustomCompare(x => true);

            var result = this.Comparator.Compare(objA, objB);

            result.AreSame.Should().BeTrue();
        }
    }
}