namespace LH.TestObjects.Compare.ValueComparators
{
    using System;

    internal class StringValueComparator : IValueComparator, IHasComparatorOptions<StringValueComparatorOptions>
    {
        public StringValueComparatorOptions Options { get; set; }

        public bool CanHandle(Type type)
        {
            return type == typeof(string);
        }

        public bool Compare(IComparisonContext comparisonContext)
        {
            var options = this.Options ?? StringValueComparatorOptions.Default;

            return string.Equals(
                (string)comparisonContext.ExpectedValue,
                (string)comparisonContext.ActualValue, 
                options.ComparisonType);
        }
    }
}
