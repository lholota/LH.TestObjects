namespace LH.TestObjects.Compare
{
    using System.Collections.Generic;
    using System.Linq;

    internal class ComparisonResult : IComparisonResult
    {
        internal ComparisonResult()
        {
            this.DifferencesList = new List<IValueComparison>();
        }

        public bool AreSame
        {
            get { return !this.DifferencesList.Any(); }
        }

        public IEnumerable<IValueComparison> Differences
        {
            get { return this.DifferencesList; }
        }

        internal IList<IValueComparison> DifferencesList { get; set; } 
    }
}