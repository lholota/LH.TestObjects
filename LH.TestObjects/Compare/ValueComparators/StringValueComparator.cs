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
            throw new NotImplementedException();
        }
    }

    internal interface IHasComparatorOptions<in TOptions>
    {
        TOptions Options { set; }
    }
}
