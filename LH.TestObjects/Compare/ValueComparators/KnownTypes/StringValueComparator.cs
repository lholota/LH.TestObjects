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

        public void Compare(ComparisonContext context, ValueComparison valueComparison)
        {
            var options = context.Rules.GetOptions<StringValueComparatorOptions>(valueComparison);

            if (!string.Equals(
                (string)valueComparison.ExpectedValue,
                (string)valueComparison.ActualValue,
                options.ComparisonType))
            {
                context.AddDifference(valueComparison);
            }
        }
    }
}
