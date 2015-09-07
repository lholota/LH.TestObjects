namespace LH.TestObjects.Compare
{
    using System;

    internal class ComparatorTypeSpecificSelectionActions<TProp> : ComparatorGenericSelectionActions, IComparatorTypeSpecificSelectionActions<TProp>
    {
        public ComparatorTypeSpecificSelectionActions(PropertyOptions propertyOptions)
            : base(propertyOptions)
        {
        }

        public IComparatorTypeSpecificSelectionActions<TProp> CustomCompare(Action<IComparisonContext<TProp>> comparisonFunc)
        {
            this.Options.CustomCompare = context =>
            {
                var typeSpecificContext = new ComparisonContext<TProp>(context);
                comparisonFunc.Invoke(typeSpecificContext);
            };

            return this;
        }
    }
}