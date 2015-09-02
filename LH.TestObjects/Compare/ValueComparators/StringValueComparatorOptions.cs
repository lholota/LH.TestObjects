namespace LH.TestObjects.Compare.ValueComparators
{
    using System;

    internal class StringValueComparatorOptions
    {
        public static readonly StringValueComparatorOptions Default = new StringValueComparatorOptions(false)
        {
            ComparisonType = StringComparison.CurrentCulture
        };

        public StringValueComparatorOptions()
            : this(true)
        {
        }

        public StringValueComparatorOptions(bool setDefaults)
        {
            if (setDefaults)
            {
                this.ComparisonType = Default.ComparisonType;
            }
        }

        public StringComparison ComparisonType { get; set; }
    }
}