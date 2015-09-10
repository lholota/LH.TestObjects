namespace LH.TestObjects.Tests.Compare.ValueComparators.CollectionValueComparator
{
    using System.Linq;
    using Domain;
    using NUnit.Framework;

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
    }
}
