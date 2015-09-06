namespace LH.TestObjects.Compare.ValueComparators
{
    using System;

    internal interface IValueComparator
    {
        bool CanHandle(Type type);

        void Compare(IComparisonContext comparisonContext, AddDifferenceDelegate addDifference);
    }
}
