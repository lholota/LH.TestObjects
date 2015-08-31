namespace LH.TestObjects.Compare
{
    using System;

    internal class PropertyOptions
    {
        public bool IsIgnored { get; set; }

        public Action<IComparisonContext> CustomCompare { get; set; }
    }
}