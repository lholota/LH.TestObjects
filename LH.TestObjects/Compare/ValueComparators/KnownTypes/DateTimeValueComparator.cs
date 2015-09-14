namespace LH.TestObjects.Compare.ValueComparators.KnownTypes
{
    using System;

    internal class DateTimeValueComparator : IValueComparator
    {
        public bool CanHandle(Type type)
        {
            return type == typeof(DateTime);
        }

        public bool Compare(ComparisonContext context, ValueComparison comparison)
        {
            var areEqual = DateTime.Equals(
                (DateTime)comparison.ExpectedValue,
                (DateTime)comparison.ActualValue);

            if (!areEqual)
            {
                context.AddDifference(comparison);
            }

            return areEqual;
        }
    }
}