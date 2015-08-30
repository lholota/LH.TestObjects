namespace LH.TestObjects.Compare
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ValueComparators;

    internal class RecursivePropertyComparator
    {
        private readonly static IEnumerable<IValueComparator> knownTypeComparators = GetKnownTypeComparators();

        private readonly ILoggerConfiguration log;
        private readonly IEnumerable<ComparatorPropertyRule> propertySelections;

        public RecursivePropertyComparator(ILoggerConfiguration log, IEnumerable<ComparatorPropertyRule> propertySelections)
        {
            this.propertySelections = propertySelections;
            this.log = log;
        }

        internal ComparisonResult Result { get; private set; }

        public void CompareRecursively(object expected, object actual)
        {
            this.Result = new ComparisonResult();

            throw new NotImplementedException();
        }

        private void CompareRecursively(PropertyPathItem propertyPath, object expected, object actual)
        {
            var context = new ComparisonContext(propertyPath, expected, actual);

            if (this.IsPropertyIgnored(propertyPath))
            {
                return;
            }

            throw new NotImplementedException();
            //var customComparator = this.GetCustomComparator(propertyPath);
            //if (customComparator != null)
            //{
            //    customComparator.Compare(/* TODO */);
            //    return;
            //}

            if (!this.IsNullComparisonMatch(expected, actual))
            {
                this.Result.DifferencesList.Add(context);
            }
            else if (ReferenceEquals(expected, null) && ReferenceEquals(actual, null))
            {
                // Both values are null, nothing to compare
                return;
            }




            // TODO: Logging


            // TODO: If custom compare -->
            // TODO: Check if it's known type --> use it
            // Otherwise: reflect and browse



            throw new NotImplementedException();
        }

        private bool IsPropertyIgnored(PropertyPathItem propertyPath)
        {
            return this.propertySelections
                .Any(x => x.Selection.IsMatch(propertyPath) && x.Options.IsIgnored);
        }

        private bool IsNullComparisonMatch(object expected, object actual)
        {
            return ReferenceEquals(expected, null) ^ ReferenceEquals(actual, null);
        }

        private static IEnumerable<IValueComparator> GetKnownTypeComparators()
        {
            yield return new StringValueComparator();
        }
    }
}