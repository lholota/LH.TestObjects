namespace LH.TestObjects.Compare
{
    /// <summary>
    /// Contains the values and other information used for the values comparison.
    /// </summary>
    public interface IComparisonContext : IDifference
    {
        /// <summary>
        /// Gets a value indicating whether the value on the expected and the actual objects are equal.
        /// </summary>
        bool AreSame { get; }
    }
}