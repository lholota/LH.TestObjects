namespace LH.TestObjects.Compare
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Logging;
    using Rules;

    internal class ComparisonContext : IDisposable
    {
        private readonly ICollection<ValueComparison> comparisons;

        public ComparisonContext(ILogger log, IEnumerable<PropertySelectionRule> customRules)
        {
            this.comparisons = new List<ValueComparison>();

            this.Result = new ComparisonResult();
            this.Rules = new RulesCollection(customRules);
            this.Log = log;
        }

        public ILogger Log { get; }

        public ComparisonResult Result { get; }

        public RulesCollection Rules { get; }

        public bool CompareItem(object expected, object actual, PropertyPathItem propertyPath)
        {
            var valueComparison = new ValueComparison(propertyPath, expected, actual);
            valueComparison.AreSame = true;

            var previousComparison = this.comparisons.SingleOrDefault(x => x.Equals(valueComparison));
            if (previousComparison != null)
            {
                this.Log.Log(LogLevel.Info, valueComparison, "The comparison between these values has already been done, skipping the property.");
                return previousComparison.AreSame;
            }
            else
            {
                this.comparisons.Add(valueComparison);
            }

            var comparator = this.Rules.GetComparator(valueComparison);

            if (this.Rules.IsIgnored(valueComparison))
            {
                this.Log.Log(LogLevel.Info, valueComparison, "{0}: The property is ignored", valueComparison.PropertyPath);
            }
            else if (!this.IsNullComparisonMatch(expected, actual))
            {
                this.AddDifference(valueComparison);
                valueComparison.AreSame = false;
            }
            else if (ReferenceEquals(expected, null) && ReferenceEquals(actual, null))
            {
            }
            else if (ReferenceEquals(expected, actual))
            {
            }
            // ReSharper disable once PossibleNullReferenceException
            else if (expected.GetType() != actual.GetType())
            {
                valueComparison.Message = string.Format(
                    "The types do not match, the expected type is {0} but the actual one is {1}.",
                    expected.GetType(),
                    actual.GetType());

                this.AddDifference(valueComparison);
                valueComparison.AreSame = false;
            }
            else
            {
                valueComparison.AreSame = comparator.Compare(this, valueComparison);
            }

            return valueComparison.AreSame;
        }

        public void AddDifference(ValueComparison valueComparison)
        {
            if (string.IsNullOrEmpty(valueComparison.Message))
            {
                valueComparison.Message = string.Format(
                    "{0}: The property values do not match, expected is {1}, but the actual {2}.",
                    valueComparison.PropertyPath,
                    valueComparison.ExpectedValue ?? "[Null]",
                    valueComparison.ActualValue ?? "[Null]");
            }

            this.Log.Log(LogLevel.Error, valueComparison);
            this.Result.DifferencesList.Add(valueComparison);
        }

        public void Dispose()
        {
            this.comparisons.Clear();
        }

        private bool IsNullComparisonMatch(object expected, object actual)
        {
            var bothNull = ReferenceEquals(expected, null) && ReferenceEquals(actual, null);
            var noneNull = !ReferenceEquals(expected, null) && !ReferenceEquals(actual, null);

            return bothNull || noneNull;
        }
    }
}
