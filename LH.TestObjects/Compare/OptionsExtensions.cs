namespace LH.TestObjects.Compare
{
    using System;
    using ValueComparators;

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
        public static IComparatorTypeSpecificSelectionActions<string> WithComparisonType(this IComparatorTypeSpecificSelectionActions<string> selectionActions, StringComparison comparisonType)
        {
            var actions = (ComparatorTypeSpecificSelectionActions<string>)selectionActions;
            var options = EnsureOptions<StringValueComparatorOptions>(opt => actions.ComparatorOptions = opt);

            options.ComparisonType = comparisonType;

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
