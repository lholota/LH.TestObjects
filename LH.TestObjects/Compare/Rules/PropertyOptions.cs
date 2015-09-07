﻿namespace LH.TestObjects.Compare.Rules
{
    using System;

    internal class PropertyOptions
    {
        public bool IsIgnored { get; set; }

        public Func<IValueComparison, bool> CustomCompare { get; set; }

        public object ValueComparatorOptions { get; set; }
    }
}