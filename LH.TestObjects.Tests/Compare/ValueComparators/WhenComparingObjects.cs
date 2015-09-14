namespace LH.TestObjects.Tests.Compare.ValueComparators
{
    using System.Linq;
    using Domain;
    using FluentAssertions;
    using NUnit.Framework;
    using TestObjects.Compare;

    [TestFixture]
    public class WhenComparingObjects
    {
        private GenericDomain<object> objA;
        private GenericDomain<object> objB;
        private ObjectComparator<GenericDomain<object>> comparator;

        [SetUp]
        public void Setup()
        {
            this.objA = new GenericDomain<object>();
            this.objB = new GenericDomain<object>();
            this.comparator = new ObjectComparator<GenericDomain<object>>();
        }

        [Test]
        public void ThenShouldPassWhenInstancesAreSame()
        {
            var instance = new object();
            this.objA.GenericProp = instance;
            this.objB.GenericProp = instance;

            var result = this.comparator.Compare(this.objA, this.objB);
            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldFailWhenInstancesDiffer()
        {
            this.objA.GenericProp = new object();
            this.objB.GenericProp = new object();

            var result = this.comparator.Compare(this.objA, this.objB);
            result.AreSame.Should().BeFalse();
            result.Differences.Single().PropertyPath.Should().Be("GenericProp");
        }
    }
}