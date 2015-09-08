namespace LH.TestObjects.Compare.ValueComparators
{
    using System;

    internal class ReferenceEqualsValueComparator : IValueComparator
    {
        public bool CanHandle(Type type)
        {
            return true;
        }

        public bool Compare(ComparisonContext context, ValueComparison comparison)
        {
            var areEqual = ReferenceEquals(comparison.ExpectedValue, comparison.ActualValue);
            if (!areEqual)
            {
                comparison.Message = "The instances are different, reference equals failed.";
                context.AddDifference(comparison);
            }

            return areEqual;
        }
    }
}