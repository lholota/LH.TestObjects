namespace LH.TestObjects.Compare.ValueComparators.KnownTypes
{
    using System;

    internal class ObjectValueComparator : IValueComparator
    {
        public bool CanHandle(Type type)
        {
            return type == typeof(object);
        }

        public bool Compare(ComparisonContext context, ValueComparison comparison)
        {
            var areEqual = object.Equals(comparison.ExpectedValue, comparison.ActualValue);
            if (!areEqual)
            {
                context.AddDifference(comparison);
            }

            return areEqual;
        }
    }
}