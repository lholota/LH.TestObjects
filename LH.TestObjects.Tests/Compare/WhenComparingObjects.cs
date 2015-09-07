namespace LH.TestObjects.Tests.Compare
{
    using System.Linq;
    using Domain;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture]
    public class WhenComparingObjects : ComparatorTestsBase
    {
        [Test]
        public void ThenShouldReturnDifferentIfOneIsNull()
        {
            var obj = SimpleDomain.CreateObjectWithValueSet1();
            var result = this.Comparator.Compare(obj, null);

            result.AreSame.Should().BeFalse();
            result.Differences.Should().NotBeNull();
            result.Differences.Count().Should().Be(1);
            result.Differences.Single().PropertyInfo.Should().BeNull();
            result.Differences.Single().ActualValue.Should().BeNull();
            result.Differences.Single().ExpectedValue.Should().NotBeNull();
        }

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
            difference.ExpectedValue.Should().Be(objA.StringProp);
            difference.ActualValue.Should().Be(objB.StringProp);

            difference.PropertyInfo.Should().NotBeNull();
            difference.PropertyInfo.Name.Should().Be("StringProp");
            difference.PropertyInfo.PropertyType.Should().Be(typeof (string));
        }

        [Test]
        public void ThenShouldFailIfOneValueIsNull()
        {
            var objA = SimpleDomain.CreateObjectWithValueSet1();
            var objB = SimpleDomain.CreateObjectWithValueSet1();

            objB.StringProp = null;

            var result = this.Comparator.Compare(objA, objB);
            result.AreSame.Should().BeFalse();
            result.Differences.Count().Should().Be(1);
        }

        [Test]
        public void ThenShouldPassIfBothValuesAreNull()
        {
            var objA = SimpleDomain.CreateObjectWithValueSet1();
            var objB = SimpleDomain.CreateObjectWithValueSet1();

            objA.StringProp = null;
            objB.StringProp = null;

            var result = this.Comparator.Compare(objA, objB);
            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldLogIfValuesDiffer()
        {
            var logCallCount = 0;
            this.Comparator.Log.Callback(x => logCallCount++);

            var objA = SimpleDomain.CreateObjectWithValueSet1();
            var objB = SimpleDomain.CreateObjectWithValueSet2();

            this.Comparator.Compare(objA, objB);
            logCallCount.Should().Be(3);
        }
    }
}