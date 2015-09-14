namespace LH.TestObjects.Tests.Compare.ValueComparators.CollectionValueComparator
{
    using System.Collections.Generic;
    using Domain;
    using NUnit.Framework;

    [TestFixture]
    public class WhenComparingListValues : WhenComparingCollectionValuesBase<IList<SimpleDomain>>
    {
        internal override IList<SimpleDomain> CreateEmptyCollection()
        {
            return new List<SimpleDomain>();
        }

        internal override IList<SimpleDomain> CreateCollectionOfThreeItems()
        {
            return new List<SimpleDomain>
            {
                SimpleDomain.CreateObjectWithValueSet1(),
                SimpleDomain.CreateObjectWithValueSet2(),
                SimpleDomain.CreateObjectWithValueSet3()
            };
        }

        internal override IList<SimpleDomain> RemoveLastItem(IList<SimpleDomain> input)
        {
            input.RemoveAt(2);
            return input;
        }

        internal override IList<SimpleDomain> SwitchLastTwoItems(IList<SimpleDomain> input)
        {
            var temp = input[1];
            input[1] = input[2];
            input[2] = temp;

            return input;
        }
    }
}
