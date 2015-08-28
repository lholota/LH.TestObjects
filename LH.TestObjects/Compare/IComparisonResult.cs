namespace LH.TestObjects.Compare
{
    using System.Collections.Generic;

    /// <summary>
    /// Summarizes the comparison of two objects and lists all found differences.
    /// </summary>
    public interface IComparisonResult
    {
        /// <summary>
        /// Gets a value indicating whether the value on the expected and the actual objects are equal.
        /// </summary>
        bool AreSame { get; }

        /// <summary>
        /// Gets the list of differences found between the expected and the actual objects.
        /// </summary>
        IEnumerable<IDifference> Differences { get; }
    }
}