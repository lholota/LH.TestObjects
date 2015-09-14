namespace LH.TestObjects.Compare.ValueComparators.KnownTypes
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