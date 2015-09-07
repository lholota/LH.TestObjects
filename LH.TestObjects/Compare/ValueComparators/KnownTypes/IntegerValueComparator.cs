namespace LH.TestObjects.Compare.ValueComparators.KnownTypes
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

        public void Compare(ComparisonContext comparisonContext, ValueComparison valueComparison)
        {
            var expected = (IComparable)valueComparison.ExpectedValue;
            var actual = (IComparable)valueComparison.ActualValue;

            if (expected.CompareTo(actual) != 0)
            {
                comparisonContext.AddDifference(valueComparison);
            }
        }
    }
}
