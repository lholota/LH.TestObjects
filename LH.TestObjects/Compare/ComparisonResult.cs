namespace LH.TestObjects.Compare
{
    using System.Collections.Generic;
    using System.Linq;

    internal class ComparisonResult : IComparisonResult
    {
        internal ComparisonResult()
        {
            this.DifferencesList = new List<IComparisonContext>();
        }

        public bool AreSame
        {
            get { return !this.DifferencesList.Any(); }
        }

        public IEnumerable<IComparisonContext> Differences
        {
            get { return this.DifferencesList; }
        }

        internal IList<IComparisonContext> DifferencesList { get; set; } 
    }
}