namespace LH.TestObjects.Compare
{
    using System;

    internal class ComparatorTypeSpecificSelectionActions<TProp> : ComparatorGenericSelectionActions, IComparatorTypeSpecificSelectionActions<TProp>
    {
        public ComparatorTypeSpecificSelectionActions(PropertyOptions propertyOptions)
            : base(propertyOptions)
        {
        }

        internal object ComparatorOptions { get; set; }

        public IComparatorTypeSpecificSelectionActions<TProp> CustomCompare(Action<IComparisonContext<TProp>> comparisonFunc)
        {
            this.Options.CustomCompare = context =>
            {
                comparisonFunc.Invoke(new ComparisonContext<TProp>(context));
            };

            return this;
        }
    }
}