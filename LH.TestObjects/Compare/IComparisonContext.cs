namespace LH.TestObjects.Compare
{
    using System.Reflection;

    /// <summary>
    /// Contains the values and other information used for the values comparison.
    /// </summary>
    public interface IComparisonContext
    {
        /// <summary>
        /// Gets or sets a value indicating whether the value on the expected and the actual objects are equal.
        /// </summary>
        bool AreSame { get; set; }

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
        PropertyInfo PropertyInfo { get; }
    }

    /// <inheritdoc />
    public interface IComparisonContext<out TProp> : IComparisonContext
    {
        /// <inheritdoc />
        new TProp ExpectedValue { get; }

        /// <inheritdoc />s
        new TProp ActualValue { get; }
    }
}