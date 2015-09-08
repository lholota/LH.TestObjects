namespace LH.TestObjects.Compare
{
    using System.Collections.Generic;
    using Logging;
    using Rules;

    internal class ComparisonContext
    {       
        public ComparisonContext(ILogger log, IEnumerable<PropertySelectionRule> customRules)
        {
            this.Result = new ComparisonResult();
            this.Rules = new RulesCollection(customRules);
            this.Log = log;
        }

        public ILogger Log { get; }

        public ComparisonResult Result { get; }

        public RulesCollection Rules { get; }

        public bool CompareItem(object expected, object actual, PropertyPathItem propertyPath)
        {
            var areSame = true;

            var valueComparison = new ValueComparison(propertyPath, expected, actual);
            var comparator = this.Rules.GetComparator(valueComparison);

            if (this.Rules.IsIgnored(valueComparison))
            {
                this.Log.Log(LogLevel.Info, valueComparison, "{0}: The property is ignored", valueComparison.PropertyPath);
            }
            else if (!this.IsNullComparisonMatch(expected, actual))
            {
                this.AddDifference(valueComparison);
                areSame = false;
            }
            else if (ReferenceEquals(expected, null) && ReferenceEquals(actual, null))
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
                areSame = false;
            }
            else
            {
                areSame = comparator.Compare(this, valueComparison);
            }

            return areSame;
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

        private bool IsNullComparisonMatch(object expected, object actual)
        {
            var bothNull = ReferenceEquals(expected, null) && ReferenceEquals(actual, null);
            var noneNull = !ReferenceEquals(expected, null) && !ReferenceEquals(actual, null);

            return bothNull || noneNull;
        }
    }
}
