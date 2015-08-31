namespace LH.TestObjects.Compare
{
    using System;

    /// <summary>
    /// Operations available on property selection when their type is resolved.
    /// </summary>
    /// <typeparam name="TProp">Compared type</typeparam>
    public interface IComparatorTypeSpecificSelectionActions<out TProp> : IGenericSelectionActions
    {
        /// <summary>
        /// Sets an override for the comparison logic.
        /// </summary>
        /// <param name="comparisonAction">Your comparison logic.</param>
        /// <returns><see cref="IComparatorTypeSpecificSelectionActions{TProp}"/></returns>
        IComparatorTypeSpecificSelectionActions<TProp> CustomCompare(Action<IComparisonContext<TProp>> comparisonAction);
    }
}