namespace LH.TestObjects.Compare
{
    using System.Collections.Generic;
    using System.Linq;

    internal class ComparisonResult : IComparisonResult
    {
        internal ComparisonResult()
        {
            this.DifferencesList = new List<IDifference>();
        }

        public bool AreSame
        {
            get { return !this.DifferencesList.Any(); }
        }

        public IEnumerable<IDifference> Differences
        {
            get { return this.DifferencesList; }
        }

        internal IList<IDifference> DifferencesList { get; set; } 
    }
}