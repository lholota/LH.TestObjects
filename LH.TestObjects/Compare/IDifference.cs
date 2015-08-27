namespace LH.TestObjects.Compare
{
    using System.Reflection;

    /// <summary>
    /// Summary of a difference between the values on the expected and the actual object.
    /// </summary>
    public interface IDifference
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
        PropertyInfo PropertyInfo { get; }

        /// <summary>
        /// Gets the message summarizing the difference between the values or a successful comparison notification if the values are equals. This message is passed to the loggers.
        /// </summary>
        string LogMessage { get; }
    }
}