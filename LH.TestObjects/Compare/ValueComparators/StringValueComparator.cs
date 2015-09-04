namespace LH.TestObjects.Compare.ValueComparators
{
    using System;

    internal class StringValueComparator : IValueComparator, IHasComparatorOptions
    {
        object IHasComparatorOptions.Options
        {
            set { this.Options = (StringValueComparatorOptions)value; }
        }

        public StringValueComparatorOptions Options { get; set; }

        public bool CanHandle(Type type)
        {
            return type == typeof(string);
        }

        public bool Compare(IComparisonContext comparisonContext)
        {
            var options = this.Options ?? new StringValueComparatorOptions();

            return string.Equals(
                (string)comparisonContext.ExpectedValue,
                (string)comparisonContext.ActualValue, 
                options.ComparisonType);
        }
    }
}
