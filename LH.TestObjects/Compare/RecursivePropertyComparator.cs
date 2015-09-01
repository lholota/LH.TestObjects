namespace LH.TestObjects.Compare
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using ValueComparators;

    internal class RecursivePropertyComparator
    {
        private static readonly IEnumerable<IValueComparator> knownTypeComparators = GetKnownTypeComparators();

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
            this.CompareRecursively(PropertyPathItem.Root, expected, actual);
        }

        private static IEnumerable<IValueComparator> GetKnownTypeComparators()
        {
            yield return new StringValueComparator();
            yield return new IntegerValueComparator();
        }

        private void CompareRecursively(PropertyPathItem propertyPath, object expected, object actual)
        {
            var context = new ComparisonContext(propertyPath, expected, actual);

            if (this.IsPropertyIgnored(propertyPath))
            {
                this.log.Log(LogLevel.Info, context, "The property {0} is ignored.", context.PropertyPathItem.GetPathString());
                return;
            }

            var customComparator = this.GetCustomComparator(propertyPath);
            if (customComparator != null)
            {
                this.log.Log(LogLevel.Info, context, "Custom comparator found for the property {0}.", context.PropertyPathItem.GetPathString());
                customComparator.Invoke(context);
                return;
            }

            if (!this.IsNullComparisonMatch(expected, actual))
            {
                this.log.Log(LogLevel.Info, context, "One of the values is null and the other is not at {0}.", context.PropertyPathItem.GetPathString());
                this.Result.DifferencesList.Add(context);
            }
            else if (ReferenceEquals(expected, null) && ReferenceEquals(actual, null))
            {
                this.log.Log(LogLevel.Info, context, "Both items are null at {0}.", propertyPath.GetPathString());
                return;
            }

            // ReSharper disable once PossibleNullReferenceException
            if (expected.GetType() != actual.GetType())
            {
                this.log.Log(
                    LogLevel.Error,
                    context,
                    "The values have different types (expected: {0}, actual: {1}) at {2}",
                    expected.GetType(),
                    actual.GetType(),
                    propertyPath.GetPathString());

                this.Result.DifferencesList.Add(context);
            }

            var knownTypeComparator = knownTypeComparators
                .FirstOrDefault(x => x.CanHandle(expected.GetType()));

            if (knownTypeComparator != null)
            {
                this.log.Log(
                    LogLevel.Debug,
                    context,
                    "The property {0} is a knowntype, using the {1}.",
                    propertyPath.GetPathString(),
                    knownTypeComparator.GetType());

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
    }
}