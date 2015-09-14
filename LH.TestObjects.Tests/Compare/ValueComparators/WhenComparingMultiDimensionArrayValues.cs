namespace LH.TestObjects.Tests.Compare.ValueComparators
{
    using System.Linq;
    using Domain;
    using FluentAssertions;
    using NUnit.Framework;
    using TestObjects.Compare;

    [TestFixture]
    public class WhenComparingMultiDimensionArrayValues
    {
        private GenericDomain<int[,]> objA;
        private GenericDomain<int[,]> objB;
        private ObjectComparator<GenericDomain<int[,]>> comparator;

        [SetUp]
        public void Setup()
        {
            this.objA = new GenericDomain<int[,]> { GenericProp = this.CreateArray() };
            this.objB = new GenericDomain<int[,]> { GenericProp = this.CreateArray() };

            this.comparator = new ObjectComparator<GenericDomain<int[,]>>();
        }

        [Test]
        public void ThenShouldPassWhenArraysAreEqual()
        {
            var result = this.comparator.Compare(this.objA, this.objB);
            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldFailWhenArraysDiffer()
        {
            this.objA.GenericProp[1, 1] = 8;
            this.objB.GenericProp[1, 1] = 9;

            var result = this.comparator.Compare(this.objA, this.objB);
            result.AreSame.Should().BeFalse();
            result.Differences.Single().PropertyPath.Should().Be("GenericProp[1, 1]");
        }

        [Test]
        public void ThenShouldFailWhenNestedArraysHaveDifferentLengths()
        {
            this.objA.GenericProp = new int[2,3];
            this.objB.GenericProp = new int[2,2];

            var result = this.comparator.Compare(this.objA, this.objB);
            result.AreSame.Should().BeFalse();
            result.Differences.Single().PropertyPath.Should().Be("GenericProp");
            result.Differences.Single().Message.Should().Contain("length");
        }

        private int[,] CreateArray()
        {
            return new[,]
            {
                { 1, 2 },
                { 3, 4 }
            };
        }
    }
}