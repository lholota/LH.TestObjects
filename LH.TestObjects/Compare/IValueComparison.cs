namespace LH.TestObjects.Compare
{
    using System.Reflection;

    /// <summary>
    /// Contains the values and other information used for the values comparison.
    /// </summary>
    public interface IValueComparison
    {      
        /// <summary>
        /// Gets the property value of the expected object.
        /// </summary>
        object ExpectedValue { get; }

        /// <summary>
        /// Gets the property value of the actual object.
        /// </summary>
        object ActualValue { get; }

        /// <summary>
        /// Gets the <see cref="PropertyInfo"/> of the compared property.
        /// </summary>
        System.Type PropertyType { get; }

        /// <summary>
        /// Gets the name of the property.
        /// </summary>
        string PropertyName { get; }

        /// <summary>
        /// Gets the dot delimited path to the property.
        /// </summary>
        string PropertyPath { get; }

        /// <summary>
        /// Gets the summary of the difference.
        /// </summary>
        string Message { get; }
    }

    /// <inheritdoc />
    public interface IValueComparison<out TProp> : IValueComparison
    {
        /// <summary>
        /// Gets or sets a value indicating whether the value on the expected and the actual objects are equal.
        /// </summary>
        bool AreSame { get; set; }

        /// <inheritdoc />
        new TProp ExpectedValue { get; }

        /// <inheritdoc />s
        new TProp ActualValue { get; }

        /// <summary>
        /// Gets the summary of the difference.
        /// </summary>
        new string Message { get; set; }

        /// <summary>
        /// This is used to compare any nested values during a custom comparison
        /// </summary>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The actual value.</param>
        /// <param name="propertyName">The property name which is used to match rules and log path where the difference occurred.</param>
        /// <returns>Returns true if the objects are same or false when they are not.</returns>
        bool CompareItem(object expected, object actual, string propertyName);
    }
}