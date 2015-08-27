namespace LH.TestObjects.Tests.Compare
{
    using System.Linq;
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture]
    public class WhenComparingObjects : ComparatorTestsBase
    {
        [Test]
        public void ThenShouldReturnSameIfPropertyValuesMatch()
        {
            this.ObjA.IntProp = this.ObjB.IntProp;
            this.ObjA.StringProp = this.ObjB.StringProp;
            this.ObjA.StringProp2 = this.ObjB.StringProp2;

            var result = this.Comparator.Compare(this.ObjA, this.ObjB);

            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldReturnDifferentIfStringPropDiffers()
        {
            this.ObjA.IntProp = this.ObjB.IntProp;
            this.ObjA.StringProp2 = this.ObjB.StringProp2;

            var result = this.Comparator.Compare(this.ObjA, this.ObjB);

            result.AreSame.Should().BeFalse();

            result.Differences.Should().NotBeNull();
            result.Differences.Count().Should().Be(1);

            var difference = result.Differences.Single();
            difference.LogMessage.Should().NotBeNullOrEmpty();
            difference.ExpectedValue.Should().Be(this.ObjA.StringProp);
            difference.ActualValue.Should().Be(this.ObjB.StringProp);

            difference.PropertyInfo.Should().NotBeNull();
            difference.PropertyInfo.Name.Should().Be("StringProp");
            difference.PropertyInfo.PropertyType.Should().Be(typeof (string));
        }
    }
}