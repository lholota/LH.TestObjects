namespace LH.TestObjects.Tests.Compare.ValueComparators.CollectionValueComparator
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain;
    using FluentAssertions;
    using NUnit.Framework;
    using TestObjects.Compare;

    public abstract class WhenComparingCollectionValuesBase<T>
        where T : IEnumerable<SimpleDomain>
    {
        private GenericDomain<T> objA;
        private GenericDomain<T> objB;
        private ObjectComparator<GenericDomain<T>> comparator;

        [SetUp]
        public void Setup()
        {
            this.objA = new GenericDomain<T>();
            this.objB = new GenericDomain<T>();

            this.objA.GenericProp = this.CreateCollectionOfThreeItems();
            this.objB.GenericProp = this.CreateCollectionOfThreeItems();

            this.comparator = new ObjectComparator<GenericDomain<T>>();
        }

        [Test]
        public void ThenShouldPassIfItemsAreEqual()
        {
            var result = this.comparator.Compare(this.objA, this.objB);
            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldFailIfNumberOfItemsDiffers()
        {
            this.objB.GenericProp = this.RemoveLastItem(this.objB.GenericProp);

            var result = this.comparator.Compare(this.objA, this.objB);
            result.AreSame.Should().BeFalse();
            result.Differences.Single().PropertyPath.Should().Be("GenericProp");
        }

        [Test]
        public void ThenShouldFailItemsAreInDifferentOrder()
        {
            this.objB.GenericProp = this.SwitchLastTwoItems(this.objB.GenericProp);

            var result = this.comparator.Compare(this.objA, this.objB);
            result.AreSame.Should().BeFalse();
            result.Differences.Count().Should().Be(6); // 2 objects * 3 properties
        }

        [Test]
        public void ThenShouldPassIfItemsAreInDifferentOrderButExplicitOrderIsProvided()
        {
            this.objB.GenericProp = this.SwitchLastTwoItems(this.objB.GenericProp);

            this.comparator
                .Property(x => x.GenericProp)
                .OrderBeforeComparison(x => x.OrderBy(y => y.StringProp));

            var result = this.comparator.Compare(this.objA, this.objB);
            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldFailIfItemPropertyDiffers()
        {
            this.objA.GenericProp.Last().StringProp = "AAA";
            this.objB.GenericProp.Last().StringProp = "BBB";

            var result = this.comparator.Compare(this.objA, this.objB);
            result.AreSame.Should().BeFalse();
            result.Differences.Single().PropertyPath.Should().Be("GenericProp[2].StringProp");
        }

        [Test]
        public void ThenShouldPassIfBothCollectionsAreEmpty()
        {
            this.objA.GenericProp = this.CreateEmptyCollection();
            this.objB.GenericProp = this.CreateEmptyCollection();

            var result = this.comparator.Compare(this.objA, this.objB);
            result.AreSame.Should().BeTrue();
        }

        internal abstract T CreateEmptyCollection();

        internal abstract T CreateCollectionOfThreeItems();

        internal abstract T RemoveLastItem(T input);

        internal abstract T SwitchLastTwoItems(T input);
    }
}