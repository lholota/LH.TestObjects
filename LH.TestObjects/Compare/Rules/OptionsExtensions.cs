// ReSharper disable CheckNamespace

// The namespace is different by design so the user does not have to import additional 
// namespaces when using the option extension methods

namespace LH.TestObjects.Compare
{
    using System;
    using System.Collections.Generic;
    using ValueComparators.KnownTypes;

    /// <summary>
    /// Contains extension methods to configure comparison for specific types
    /// </summary>
    public static class OptionsExtensions
    {
        /// <summary>
        /// Defines the string comparison type - e.g. case sensitive or insensitive.
        /// </summary>
        /// <param name="selectionActions">The property selection the configuration will be applied on</param>
        /// <param name="comparisonType">The comparison type of <see cref="StringComparison"/></param>
        /// <returns>The property selection</returns>
        public static IComparatorTypeSpecificSelectionActions<string> WithComparisonType(
            this IComparatorTypeSpecificSelectionActions<string> selectionActions, 
            StringComparison comparisonType)
        {
            var actions = (ComparatorTypeSpecificSelectionActions<string>)selectionActions;
            var options = EnsureOptions<StringValueComparatorOptions>(opt => actions.Options.ValueComparatorOptions = opt);

            options.ComparisonType = comparisonType;

            return actions;
        }

        /// <summary>
        /// Defines the allowed deviation when comparing float values. Default value is float.MinValue.
        /// </summary>
        /// <param name="selectionActions">Property selection on which the configuration action should be applied.</param>
        /// <param name="epsilon">The allowed deviation</param>
        /// <returns><see cref="IComparatorTypeSpecificSelectionActions{TProp}"/></returns>
        public static IComparatorTypeSpecificSelectionActions<float> WithEpsilon(
            this IComparatorTypeSpecificSelectionActions<float> selectionActions, 
            float epsilon)
        {
            var actions = (ComparatorTypeSpecificSelectionActions<float>)selectionActions;
            var options = EnsureOptions<FloatValueComparatorOptions>(opt => actions.Options.ValueComparatorOptions = opt);

            options.FloatEpsilon = epsilon;

            return actions;
        }

        /// <summary>
        /// Defines the allowed deviation when comparing double values. Default value is double.MinValue.
        /// </summary>
        /// <param name="selectionActions">Property selection on which the configuration action should be applied.</param>
        /// <param name="epsilon">The allowed deviation</param>
        /// <returns><see cref="IComparatorTypeSpecificSelectionActions{TProp}"/></returns>
        public static IComparatorTypeSpecificSelectionActions<double> WithEpsilon(
            this IComparatorTypeSpecificSelectionActions<double> selectionActions,
            float epsilon)
        {
            var actions = (ComparatorTypeSpecificSelectionActions<double>)selectionActions;
            var options = EnsureOptions<FloatValueComparatorOptions>(opt => actions.Options.ValueComparatorOptions = opt);

            options.DoubleEpsilon = epsilon;

            return actions;
        }

        public static IComparatorTypeSpecificSelectionActions<TCollection> OrderBeforeComparison<TCollection, TItem>(
            this IComparatorTypeSpecificSelectionActions<TCollection> selectionActions,
            Func<TCollection, IEnumerable<TItem>> orderAction)
            where TCollection : IEnumerable<TItem>
        {
            var actions = (ComparatorTypeSpecificSelectionActions<TCollection>)selectionActions;
            var options = EnsureOptions<CollectionValueComparatorOptions>(opt => actions.Options.ValueComparatorOptions = opt);

            options.OrderFunction = x => orderAction.Invoke((TCollection)x);

            return actions;
        }

        private static T EnsureOptions<T>(Action<T> assignOptions)
            where T : new()
        {
            var options = new T();
            assignOptions.Invoke(options);

            return options;
        }
    }
}
