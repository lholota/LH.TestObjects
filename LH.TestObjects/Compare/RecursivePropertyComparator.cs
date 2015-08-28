namespace LH.TestObjects.Compare
{
    using System.Collections.Generic;

    internal class RecursivePropertyComparator
    {
        private readonly ILoggerConfiguration log;
        private readonly IList<ComparatorPropertySelection> propertySelections;

        public RecursivePropertyComparator(ILoggerConfiguration log, IList<ComparatorPropertySelection> propertySelections)
        {
            this.propertySelections = propertySelections;
            this.log = log;

            // TODO: Register value comparators
            // TODO: Add structs into the tests
        }

        public IComparisonResult CompareRecursively(object expected, object actual)
        {
            throw new System.NotImplementedException();
        }
    }
}