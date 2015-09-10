namespace LH.TestObjects.Compare.ValueComparators.KnownTypes
{
    using System;
    using System.Collections;

    internal class CollectionValueComparatorOptions
    {
        public Func<IEnumerable, IEnumerable> OrderFunction { get; set; }
    }
}