﻿namespace LH.TestObjects.Compare
{
    using System;
    using Rules;

    internal class ComparatorTypeSpecificSelectionActions<TProp> : ComparatorGenericSelectionActions, IComparatorTypeSpecificSelectionActions<TProp>
    {
        public ComparatorTypeSpecificSelectionActions(PropertyOptions propertyOptions)
            : base(propertyOptions)
        {
        }

        public IComparatorTypeSpecificSelectionActions<TProp> CustomCompare(Func<IValueComparison<TProp>, bool> comparisonAction)
        {
            this.Options.CustomCompare = (valueComparison, context) =>
            {
                var adapter = new ValueComparisonAdapter<TProp>(valueComparison, context);
                return comparisonAction.Invoke(adapter);
            };

            return this;
        }
    }
}