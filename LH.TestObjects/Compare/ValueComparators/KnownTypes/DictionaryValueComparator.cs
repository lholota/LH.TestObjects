namespace LH.TestObjects.Compare.ValueComparators.KnownTypes
{
    using System;
    using System.Collections;
    using System.Linq;

    internal class DictionaryValueComparator : IValueComparator
    {
        public bool CanHandle(Type type)
        {
            return typeof(IDictionary).IsAssignableFrom(type);
        }

        public void Compare(ComparisonContext comparisonContext, ValueComparison valueComparison)
        {
            var actual = (IDictionary)valueComparison.ActualValue;
            var expected = (IDictionary)valueComparison.ExpectedValue;

            if (!this.AreKeysEqual(expected, actual, comparisonContext, valueComparison))
            {
                return;
            }

            throw new NotImplementedException();
        }

        private bool AreKeysEqual(IDictionary expected, IDictionary actual, IComparisonContext comparisonContext, ValueComparison valueComparison)
        {
            var notInExpected = actual.Keys
                .Cast<object>()
                .Where(x => !expected.Contains(x));

            var notInActual = expected.Keys
                .Cast<object>()
                .Where(x => !actual.Contains(x));

            var result = true;

            foreach (var key in notInActual)
            {
                var message = string.Format(
                        "The dictionaries at {0} differ, the key '{1}' is missing in the actual value.",
                        valueComparison.PropertyPath,
                        key);

                var keyComparison = this.CreateKeyNamedComparison(valueComparison, key.ToString());
                comparisonContext.AddDifference(keyComparison, message);
                result = false;
            }

            foreach (var key in notInExpected)
            {
                var message = string.Format(
                        "The dictionaries at {0} differ, there is an extra key '{1}' in the actual value.",
                        valueComparison.PropertyPath,
                        key);

                var keyComparison = this.CreateKeyNamedComparison(valueComparison, key.ToString());
                comparisonContext.AddDifference(keyComparison, message);
                result = false;
            }

            return result;
        }

        private ValueComparison CreateKeyNamedComparison(ValueComparison comparison, string key)
        {
            var itemPropPath = new PropertyPathItem(key, comparison.PropertyPathItem);

            return new ValueComparison(
                itemPropPath,
                comparison.ExpectedValue,
                comparison.ActualValue);
        }
    }
}
