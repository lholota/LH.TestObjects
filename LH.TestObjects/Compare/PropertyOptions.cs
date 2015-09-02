namespace LH.TestObjects.Compare
{
    using System;

    internal class PropertyOptions
    {
        public bool IsIgnored { get; set; }

        public Action<ComparisonContext> CustomCompare { get; set; }

        public object ValueComparatorOptions { get; set; }
    }
}