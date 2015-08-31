namespace LH.TestObjects.Compare
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
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
            // TODO: Logging
            var context = new ComparisonContext(propertyPath, expected, actual);

            if (this.IsPropertyIgnored(propertyPath))
            {
                return;
            }

            var customComparator = this.GetCustomComparator(propertyPath);
            if (customComparator != null)
            {
                customComparator.Invoke(context);
                return;
            }

            if (!this.IsNullComparisonMatch(expected, actual))
            {
                this.Result.DifferencesList.Add(context);
            }
            else if (ReferenceEquals(expected, null) && ReferenceEquals(actual, null))
            {
                // Both values are null, nothing to compare
                return;
            }

            // ReSharper disable once PossibleNullReferenceException
            if (expected.GetType() != actual.GetType())
            {
                this.Result.DifferencesList.Add(context);
            }

            var knownTypeComparator = knownTypeComparators
                .FirstOrDefault(x => x.CanHandle(expected.GetType()));

            if (knownTypeComparator != null)
            {
                knownTypeComparator.Compare(context);
                return;
            }

            var properties = expected.GetType()
                .GetRuntimeProperties(); // TODO: Will this work?

            foreach (var property in properties)
            {
                var propertyPathItem = new PropertyPathItem(property, propertyPath);

                var expectedPropValue = property.GetValue(expected);
                var actualPropValue = property.GetValue(actual);

                this.CompareRecursively(propertyPathItem, expectedPropValue, actualPropValue);
            }
        }

        private Action<ComparisonContext> GetCustomComparator(PropertyPathItem propertyPath)
        {
            var matchingRule = this.propertySelections
                .Where(x => x.Options.CustomCompare != null && x.Selection.IsMatch(propertyPath))
                .OrderByDescending(x => x.OrderIndex)
                .FirstOrDefault();

            if (matchingRule != null)
            {
                return matchingRule.Options.CustomCompare;
            }

            return null;
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