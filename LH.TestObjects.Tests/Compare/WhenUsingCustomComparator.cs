namespace LH.TestObjects.Tests.Compare
{
    using System.Linq;
    using Domain;
    using FluentAssertions;
    using NUnit.Framework;

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
                .CustomCompare(x => x.AreSame = true);

            var result = this.Comparator.Compare(objA, objB);

            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldFailWhenValuesDiffer()
        {
            var objA = SimpleDomain.CreateObjectWithValueSet1();
            var objB = SimpleDomain.CreateObjectWithValueSet1();

            objA.StringProp = "AAA";
            objB.StringProp = "BBB";

            this.Comparator
                .Property(x => x.StringProp)
                .CustomCompare(x => x.AreSame = false);

            var result = this.Comparator.Compare(objA, objB);

            result.AreSame.Should().BeFalse();
            result.Differences.Should().NotBeNull();
            result.Differences.Count().Should().Be(1);
            result.Differences.Single().PropertyPath.Should().Be("StringProp");
        }
    }
}