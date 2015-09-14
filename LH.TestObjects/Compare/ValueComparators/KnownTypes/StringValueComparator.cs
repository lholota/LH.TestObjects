namespace LH.TestObjects.Compare.ValueComparators.KnownTypes
{
    using System;

    internal class StringValueComparator : IValueComparator
    {
        public StringValueComparatorOptions Options { get; set; }

        public bool CanHandle(Type type)
        {
            return type == typeof(string);
        }

        public bool Compare(ComparisonContext context, ValueComparison valueComparison)
        {
            var options = context.Rules.GetOptions<StringValueComparatorOptions>(valueComparison);
            var areEqual = string.Equals(
                (string)valueComparison.ExpectedValue,
                (string)valueComparison.ActualValue,
                options.ComparisonType);

            if (!areEqual)
            {
                context.AddDifference(valueComparison);
            }

            return areEqual;
        }
    }
}
