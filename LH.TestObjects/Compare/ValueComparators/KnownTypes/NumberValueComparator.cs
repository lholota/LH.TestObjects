namespace LH.TestObjects.Compare.ValueComparators.KnownTypes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class NumberValueComparator : IValueComparator
    {
        private readonly IEnumerable<Type> handledTypes = new[]
        {
            typeof(decimal),
            typeof(short),
            typeof(ushort),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong)
        };

        public bool CanHandle(Type type)
        {
            return this.handledTypes.Contains(type) || type.IsEnum;
        }

        public bool Compare(ComparisonContext comparisonContext, ValueComparison valueComparison)
        {
            var expected = (IComparable)valueComparison.ExpectedValue;
            var actual = (IComparable)valueComparison.ActualValue;

            var areEqual = expected.CompareTo(actual) == 0;
            if (!areEqual)
            {
                comparisonContext.AddDifference(valueComparison);
            }

            return areEqual;
        }
    }
}
