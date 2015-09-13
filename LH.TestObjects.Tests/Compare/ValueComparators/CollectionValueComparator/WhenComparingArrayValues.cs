namespace LH.TestObjects.Tests.Compare.ValueComparators.CollectionValueComparator
{
    using System.Linq;
    using Domain;
    using FluentAssertions;
    using NUnit.Framework;
    using TestObjects.Compare;

    [TestFixture]
    public class WhenComparingArrayValues : WhenComparingCollectionValuesBase<SimpleDomain[]>
    {
        internal override SimpleDomain[] CreateEmptyCollection()
        {
            return new SimpleDomain[0];
        }

        internal override SimpleDomain[] CreateCollectionOfThreeItems()
        {
            return new[]
            {
                SimpleDomain.CreateObjectWithValueSet1(),
                SimpleDomain.CreateObjectWithValueSet2(),
                SimpleDomain.CreateObjectWithValueSet3(),
            };
        }

        internal override SimpleDomain[] RemoveLastItem(SimpleDomain[] input)
        {
            return input.Take(2).ToArray();
        }

        internal override SimpleDomain[] SwitchLastTwoItems(SimpleDomain[] input)
        {
            return new[]
            {
                input[0],
                input[2],
                input[1]
            };
        }

        [Test]
        public void ThenShouldSuccedWhenComparingEqualJaggedArrays()
        {
            var objA = new GenericDomain<int[][]> { GenericProp = this.CreateJaggedArray() };
            var objB = new GenericDomain<int[][]> { GenericProp = this.CreateJaggedArray() };

            var comparator = new ObjectComparator<GenericDomain<int[][]>>();
            var result = comparator.Compare(objA, objB);

            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldSuccedWhenComparingJaggedArraysWithDifferentValues()
        {
            var objA = new GenericDomain<int[][]> { GenericProp = this.CreateJaggedArray() };
            var objB = new GenericDomain<int[][]> { GenericProp = this.CreateJaggedArray() };

            objA.GenericProp[0][1] = 5;
            objB.GenericProp[0][1] = 10;

            var comparator = new ObjectComparator<GenericDomain<int[][]>>();
            var result = comparator.Compare(objA, objB);

            result.AreSame.Should().BeFalse();
            result.Differences.Single().PropertyPath.Should().Be("GenericProp[0][1]");
        }

        [Test]
        public void ThenShouldSuccedWhenComparingJaggedArraysWithDifferentLengths()
        {
            var objA = new GenericDomain<int[][]> { GenericProp = this.CreateJaggedArray() };
            var objB = new GenericDomain<int[][]> { GenericProp = this.CreateJaggedArray() };

            objA.GenericProp[0] = new int[3];
            objB.GenericProp[0] = new int[5];

            var comparator = new ObjectComparator<GenericDomain<int[][]>>();
            var result = comparator.Compare(objA, objB);

            result.AreSame.Should().BeFalse();
            result.Differences.Single().PropertyPath.Should().Be("GenericProp[0]");
        }

        private int[][] CreateJaggedArray()
        {
            var result = new int[2][];
            result[0] = new[] { 1, 2, 3 };
            result[1] = new[] { 4 };

            return result;
        }
    }
}
