namespace LH.TestObjects.Compare.ValueComparators
{
    using System;

    internal interface IValueComparator
    {
        bool CanHandle(Type type);

        void Compare(ComparisonContext context, ValueComparison comparison);
    }
}
