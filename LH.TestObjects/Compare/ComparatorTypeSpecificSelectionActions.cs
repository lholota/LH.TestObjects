namespace LH.TestObjects.Compare
{
    using System;

    internal class ComparatorTypeSpecificSelectionActions<TProp> : ComparatorGenericSelectionActions, IComparatorTypeSpecificSelectionActions<TProp>
    {
        public ComparatorTypeSpecificSelectionActions(PropertyOptions propertyOptions)
            : base(propertyOptions)
        { }

        public IComparatorTypeSpecificSelectionActions<TProp> CustomCompare(Func<TProp, TProp, bool> comparisonFunc)
        {
            // TODO: Use context instead of the two values?
            throw new NotImplementedException();
        }
    }
}