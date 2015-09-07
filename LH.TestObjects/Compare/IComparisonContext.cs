namespace LH.TestObjects.Compare
{
    /// <summary>
    /// The context containing logic for recursive comparison.
    /// </summary>
    public interface IComparisonContext
    {
        /// <summary>
        /// Report a different when found.
        /// </summary>
        /// <param name="valueComparison">The comparison which is being performed.</param>
        /// <param name="message">Custom log message</param>
        void AddDifference(IValueComparison valueComparison, string message = null);
    }
}