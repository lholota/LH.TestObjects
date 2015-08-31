namespace LH.TestObjects.Compare
{
    using System.Reflection;

    /// <summary>
    /// Contains the values and other information used for the values comparison.
    /// </summary>
    public interface IComparisonContext
    {
        /// <summary>
        /// Gets a value indicating whether the value on the expected and the actual objects are equal.
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

        /// <summary>
        /// Gets the message summarizing the difference between the values or a successful comparison notification if the values are equals. This message is passed to the loggers.
        /// </summary>
        string LogMessage { get; }
    }    
}