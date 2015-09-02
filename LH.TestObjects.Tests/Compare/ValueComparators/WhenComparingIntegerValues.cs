namespace LH.TestObjects.Tests.Compare.ValueComparators
{
    using System.Linq;
    using FluentAssertions;
    using NUnit.Framework;
    using TestDomain;

    [TestFixture]
    public class WhenComparingIntegerValues : ComparatorTestsBase
    {
        [Test]
        public void ThenShouldFailIfValuesDiffer()
        {
            var objA = SimpleDomain.CreateObjectWithValueSet1();
            var objB = SimpleDomain.CreateObjectWithValueSet1();

            objA.IntProp = 1;
            objB.IntProp = 2;

            var result = this.Comparator.Compare(objA, objB);

            result.AreSame.Should().BeFalse();
            result.Differences.Should().NotBeNull();
            result.Differences.Count().Should().Be(1);
            result.Differences.Single().PropertyInfo.Name.Should().Be("IntProp");
        }

        [Test]
        public void ShouldPassIfValuesMatch()
        {
            var objA = SimpleDomain.CreateObjectWithValueSet1();
            var objB = SimpleDomain.CreateObjectWithValueSet1();

            var result = this.Comparator.Compare(objA, objB);
            result.AreSame.Should().BeTrue();
        }
    }
}