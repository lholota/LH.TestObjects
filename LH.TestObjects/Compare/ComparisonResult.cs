namespace LH.TestObjects.Compare
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Summarizes the comparison of two objects and lists all found differences.
    /// </summary>
    public class ComparisonResult
    {
        internal ComparisonResult()
        {
            this.DifferencesList = new List<IDifference>();
        }

        /// <summary>
        /// Gets a value indicating whether the value on the expected and the actual objects are equal.
        /// </summary>
        public bool AreSame
        {
            get { return !this.DifferencesList.Any(); }
        }

        /// <summary>
        /// Gets the list of differences found between the expected and the actual objects.
        /// </summary>
        public IEnumerable<IDifference> Differences
        {
            get { return this.DifferencesList; }
        }

        internal IList<IDifference> DifferencesList { get; set; } 
    }
}