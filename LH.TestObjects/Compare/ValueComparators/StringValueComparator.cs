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

        public void Compare(ComparisonContext comparisonContext, AddDifferenceDelegate addDifferenceCall)
        {
            var options = this.Options ?? new StringValueComparatorOptions();

            if (!string.Equals(
                (string)comparisonContext.ExpectedValue,
                (string)comparisonContext.ActualValue,
                options.ComparisonType))
            {
                addDifferenceCall.Invoke(DifferenceType.Value, comparisonContext);
            }
        }
    }
}
