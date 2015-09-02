namespace LH.TestObjects.Compare
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Object allowing strongly typed property selection.
    /// </summary>
    /// <typeparam name="TUserType">The compared/generated type</typeparam>
    public interface IComparatorTypeSpecificPropertySelector<TUserType> : IGenericPropertySelector<TUserType>
    {
        /// <summary>
        /// Selects a property by an expression (for example x => x.MyProperty)
        /// </summary>
        /// <typeparam name="TProp">Property type</typeparam>
        /// <param name="propertyExpression">Expression defining the property (for example x => x.MyProperty or x => x.MyProperty.AnotherProperty)</param>
        /// <returns>The <see cref="IComparatorTypeSpecificSelectionActions{TProp}"/> which allows you to apply property configuration.</returns>
        IComparatorTypeSpecificSelectionActions<TProp> Property<TProp>(Expression<Func<TUserType, TProp>> propertyExpression);

        /// <summary>
        /// Selects all properties of a given type and optionally matching a predicate expression.
        /// </summary>
        /// <typeparam name="TProp">Property type</typeparam>
        /// <param name="includeInheritedTypes">Sets whether types inherited from {TProp} will be included in the selection (true) or not (false).</param>
        /// <param name="predicate">Predicate used to filter the properties. The parameter is optional.</param>
        /// <returns>The <see cref="IComparatorTypeSpecificSelectionActions{TProp}"/> which allows you to apply property configuration.</returns>
        IComparatorTypeSpecificSelectionActions<TProp> PropertiesOfType<TProp>(bool includeInheritedTypes = true, Func<PropertyInfo, bool> predicate = null);
    }
}