// ReSharper disable PossibleMultipleEnumeration
namespace LH.TestObjects.Compare.ValueComparators.KnownTypes
{
    using System;
    using System.Collections;
    using System.Linq;

    internal class CollectionValueComparator : IValueComparator
    {
        public bool CanHandle(Type type)
        {
            return typeof(IEnumerable).IsAssignableFrom(type);
        }

        public bool Compare(ComparisonContext context, ValueComparison comparison)
        {
            var areEqual = true;
            var options = context.Rules.GetOptions<CollectionValueComparatorOptions>(comparison);

            var actual = (IEnumerable)comparison.ActualValue;
            var expected = (IEnumerable)comparison.ExpectedValue;

            if (options.OrderFunction != null)
            {
                actual = options.OrderFunction.Invoke(actual);
                expected = options.OrderFunction.Invoke(expected);
            }

            var actualEnumerator = actual.GetEnumerator();
            var expectedEnumerator = expected.GetEnumerator();

            var actualHasNext = actualEnumerator.MoveNext();
            var expectedHasNext = expectedEnumerator.MoveNext();

            var itemCounter = 0;
            while (actualHasNext && expectedHasNext)
            {
                var propertyPath = new PropertyPathItem(itemCounter.ToString(), comparison.PropertyPathItem);
                if (!context.CompareItem(expectedEnumerator.Current, actualEnumerator.Current, propertyPath))
                {
                    areEqual = false;
                }

                expectedHasNext = expectedEnumerator.MoveNext();
                actualHasNext = actualEnumerator.MoveNext();
                itemCounter++;
            }

            if (actualHasNext != expectedHasNext)
            {
                comparison.Message = string.Format(
                    "The collections have a different number of items, expected: {0}, actual: {1}.",
                    expected.Cast<object>().Count(),
                    actual.Cast<object>().Count());

                context.AddDifference(comparison);
                areEqual = false;
            }

            return areEqual;
        }
    }
}