namespace LH.TestObjects.Tests.Compare.ValueComparators
{
    using System.Linq;
    using Domain;
    using FluentAssertions;
    using NUnit.Framework;
    using TestObjects.Compare;

    [TestFixture]
    public class WhenComparingNullableValues
    {
        private GenericDomain<int?> objA;
        private GenericDomain<int?> objB;
        private ObjectComparator<GenericDomain<int?>> comparator;

        [SetUp]
        public void Setup()
        {
            this.objA = new GenericDomain<int?>();
            this.objB = new GenericDomain<int?>();
            this.comparator = Extensions.CreateComparator<GenericDomain<int?>>();
        }

        [Test]
        public void ThenShouldSucceedIfBothHaveEqualValues()
        {
            this.objA.GenericProp = 1;
            this.objB.GenericProp = 1;

            var result = this.comparator.Compare(this.objA, this.objB);
            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldPassIfNoneHasValue()
        {
            this.objA.GenericProp = null;
            this.objB.GenericProp = null;

            var result = this.comparator.Compare(this.objA, this.objB);
            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldFailIfOnlyOneHasValue()
        {
            this.objA.GenericProp = 1;
            this.objB.GenericProp = null;

            var result = this.comparator.Compare(this.objA, this.objB);
            result.AreSame.Should().BeFalse();
            result.Differences.Single().PropertyPath.Should().Be("GenericProp");
        }

        [Test]
        public void ThenShouldFailIfValuesDiffer()
        {
            this.objA.GenericProp = 1;
            this.objB.GenericProp = 2;

            var result = this.comparator.Compare(this.objA, this.objB);
            result.AreSame.Should().BeFalse();
            result.Differences.Single().PropertyPath.Should().Be("GenericProp.Value");
        }
    }
}