﻿namespace LH.TestObjects.Compare.ValueComparators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal class IntegerComparator : IValueComparator
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

        public bool Compare(IComparisonContext comparisonContext)
        {
            throw new NotImplementedException();
        }
    }
}
