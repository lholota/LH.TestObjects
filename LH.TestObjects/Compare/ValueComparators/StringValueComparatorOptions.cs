namespace LH.TestObjects.Compare.ValueComparators
{
    using System;

    internal class StringValueComparatorOptions
    {
        public StringValueComparatorOptions()
        {
            this.ComparisonType = StringComparison.CurrentCulture;
        }

        public StringComparison ComparisonType { get; set; }
    }
}