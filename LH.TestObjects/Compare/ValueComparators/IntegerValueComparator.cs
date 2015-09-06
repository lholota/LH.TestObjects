namespace LH.TestObjects.Compare.ValueComparators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class IntegerValueComparator : IValueComparator
    {
        private readonly IEnumerable<Type> handledTypes = new[]
        {
            typeof(short),
            typeof(ushort),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong)
        };

        public bool CanHandle(Type type)
        {
            return this.handledTypes.Contains(type);
        }

        public void Compare(IComparisonContext comparisonContext, AddDifferenceDelegate addDifferenceCall)
        {
            var expected = (IComparable)comparisonContext.ExpectedValue;
            var actual = (IComparable)comparisonContext.ActualValue;

            if (expected.CompareTo(actual) != 0)
            {
                addDifferenceCall.Invoke(DifferenceType.Value, comparisonContext);
            }
        }
    }
}
