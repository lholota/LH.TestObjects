namespace LH.TestObjects.Compare
{
    using System;

    /// <summary>
    /// Contains extension methods to configure comparison for specific types
    /// </summary>
    public static class OptionsExtensions
    {
        /// <summary>
        /// Defines the string comparison type - e.g. case sensitive or insensitive.
        /// </summary>
        /// <param name="stringPropertySelection">The property selection the configuration will be applied on</param>
        /// <param name="comparisonType">The comparison type of <see cref="StringComparison"/></param>
        /// <returns>The property selection</returns>
        public static ITypeSpecificComparisonActions<string> WithComparisonType(this ITypeSpecificComparisonActions<string> stringPropertySelection, StringComparison comparisonType)
        {
            throw new NotImplementedException();
        }
    }
}
