namespace LH.TestObjects.Compare.ValueComparators
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

        public void Compare(ComparisonContext comparisonContext, AddDifferenceDelegate addDifferenceCall)
        {
            var actual = (IDictionary)comparisonContext.ActualValue;
            var expected = (IDictionary)comparisonContext.ExpectedValue;

            if (!this.AreKeysEqual(expected, actual, comparisonContext, addDifferenceCall))
            {
                return;
            }

            throw new NotImplementedException();
        }

        private bool AreKeysEqual(IDictionary expected, IDictionary actual, ComparisonContext context, AddDifferenceDelegate addDifferenceCall)
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
                        context.PropertyPath,
                        key);

                addDifferenceCall.Invoke(DifferenceType.Value, this.CreateContext(context, key.ToString()), message);
                result = false;
            }

            foreach (var key in notInExpected)
            {
                var message = string.Format(
                        "The dictionaries at {0} differ, there is an extra key '{1}' in the actual value.",
                        context.PropertyPath,
                        key);

                addDifferenceCall.Invoke(DifferenceType.Value, this.CreateContext(context, key.ToString()), message);
                result = false;
            }

            return result;
        }

        private IComparisonContext CreateContext(ComparisonContext context, string key)
        {
            var parentItem = context.PropertyPathItem;
            var itemPropPath = new PropertyPathItem(key, parentItem);

            return new ComparisonContext(
                itemPropPath,
                context.ExpectedValue,
                context.ActualValue);
        }
    }
}
