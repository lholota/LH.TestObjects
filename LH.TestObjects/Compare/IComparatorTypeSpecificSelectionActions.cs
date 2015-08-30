namespace LH.TestObjects.Compare
{
    using System;

    /// <summary>
    /// Operations available on property selection when their type is resolved.
    /// </summary>
    /// <typeparam name="TProp">Compared type</typeparam>
    public interface IComparatorTypeSpecificSelectionActions<out TProp> : IGenericSelectionActions
    {
        IComparatorTypeSpecificSelectionActions<TProp> CustomCompare(Func<TProp, TProp, bool> comparisonFunc);
    }
}