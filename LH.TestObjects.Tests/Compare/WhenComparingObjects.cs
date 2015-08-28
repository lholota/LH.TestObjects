namespace LH.TestObjects.Tests.Compare
{
    using System.Linq;
    using FluentAssertions;
    using NUnit.Framework;
    using TestDomain;

    [TestFixture]
    public class WhenComparingObjects : ComparatorTestsBase
    {
        [Test]
        public void ThenShouldReturnSameIfPropertyValuesMatch()
        {
            var objA = SimpleDomain.CreateObjectWithValueSet1();
            var objB = SimpleDomain.CreateObjectWithValueSet1();

            var result = this.Comparator.Compare(objA, objB);

            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldReturnDifferentIfStringPropDiffers()
        {
            var objA = SimpleDomain.CreateObjectWithValueSet1();
            var objB = SimpleDomain.CreateObjectWithValueSet2();

            objA.IntProp = objB.IntProp;
            objA.StringProp2 = objB.StringProp2;

            var result = this.Comparator.Compare(objA, objB);

            result.AreSame.Should().BeFalse();

            result.Differences.Should().NotBeNull();
            result.Differences.Count().Should().Be(1);

            var difference = result.Differences.Single();
            difference.LogMessage.Should().NotBeNullOrEmpty();
            difference.ExpectedValue.Should().Be(objA.StringProp);
            difference.ActualValue.Should().Be(objB.StringProp);

            difference.PropertyInfo.Should().NotBeNull();
            difference.PropertyInfo.Name.Should().Be("StringProp");
            difference.PropertyInfo.PropertyType.Should().Be(typeof (string));
        }
    }
}