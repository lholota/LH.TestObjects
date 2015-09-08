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
            result.Differences.Single().PropertyName.Should().BeNullOrEmpty();
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
            difference.PropertyName.Should().Be("StringProp");
            difference.PropertyType.Should().Be(typeof (string));
        }

        [Test]
        public void ThenMessageShouldNotBeEmptyIfDifferenceIsFound()
        {
            var objA = SimpleDomain.CreateObjectWithValueSet1();
            var objB = SimpleDomain.CreateObjectWithValueSet1();

            objA.StringProp = "AAA";
            objB.StringProp = "BBB";

            var result = this.Comparator.Compare(objA, objB);

            result.AreSame.Should().BeFalse();
            result.Differences.Should().NotBeNull();
            result.Differences.Count().Should().Be(1);

            var difference = result.Differences.Single();
            difference.Message.Should().NotBeNullOrEmpty();
            difference.Message.Should().Contain(objA.StringProp);
            difference.Message.Should().Contain(objB.StringProp);
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