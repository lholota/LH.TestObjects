namespace LH.TestObjects.Compare.ValueComparators
{
    using System;

    internal interface IValueComparator
    {
        bool CanHandle(Type type);

        bool Compare(ComparisonContext context, ValueComparison comparison);
    }
}
