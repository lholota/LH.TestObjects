namespace LH.TestObjects.Compare.ValueComparators.KnownTypes
{
    using System;

    internal class TimeSpanValueComparator : IValueComparator
    {
        public bool CanHandle(Type type)
        {
            return type == typeof(TimeSpan);
        }

        public bool Compare(ComparisonContext context, ValueComparison comparison)
        {
            var areEqual = TimeSpan.Equals(
                (TimeSpan)comparison.ExpectedValue,
                (TimeSpan)comparison.ActualValue
                );

            if (!areEqual)
            {
                context.AddDifference(comparison);
            }

            return areEqual;
        }
    }
}