namespace LH.TestObjects.Tests.Compare.ValueComparators.CollectionValueComparator
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain;

    public class WhenComparingReadOnlyCollectionValues : WhenComparingCollectionValuesBase<IReadOnlyCollection<SimpleDomain>>
    {
        private readonly WhenComparingListValues listTests = new WhenComparingListValues();

        internal override IReadOnlyCollection<SimpleDomain> CreateEmptyCollection()
        {
            return this.ToReadOnly(this.listTests.CreateEmptyCollection());
        }

        internal override IReadOnlyCollection<SimpleDomain> CreateCollectionOfThreeItems()
        {
            return this.ToReadOnly(this.listTests.CreateCollectionOfThreeItems());
        }

        internal override IReadOnlyCollection<SimpleDomain> RemoveLastItem(IReadOnlyCollection<SimpleDomain> input)
        {
            return this.ToReadOnly(this.listTests.RemoveLastItem(input.ToList()));
        }

        internal override IReadOnlyCollection<SimpleDomain> SwitchLastTwoItems(IReadOnlyCollection<SimpleDomain> input)
        {
            return this.ToReadOnly(this.listTests.SwitchLastTwoItems(input.ToList()));
        }

        private IReadOnlyCollection<SimpleDomain> ToReadOnly(IList<SimpleDomain> list)
        {
            return ((List<SimpleDomain>)list)
                .AsReadOnly();
        }
    }
}
