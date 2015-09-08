namespace LH.TestObjects.Compare.ValueComparators.KnownTypes
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    internal class FloatValueComparator : IValueComparator
    {
        public bool CanHandle(Type type)
        {
            return type == typeof(float)
                   || type == typeof(double);
        }

        public bool Compare(ComparisonContext context, ValueComparison valueComparison)
        {
            bool areEqual;
            var options = context.Rules.GetOptions<FloatValueComparatorOptions>(valueComparison);

            if (valueComparison.PropertyInfo.PropertyType == typeof(float))
            {
                areEqual = this.CompareFloats(
                    (float)valueComparison.ExpectedValue,
                    (float)valueComparison.ActualValue,
                    options.FloatEpsilon);
            }
            else
            {
                areEqual = this.CompareDoubles(
                    (double)valueComparison.ExpectedValue,
                    (double)valueComparison.ActualValue,
                    options.DoubleEpsilon);
            }

            if (!areEqual)
            {
                context.AddDifference(valueComparison);
            }

            return areEqual;
        }

        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator", Justification = "ByDesign")]
        private bool CompareFloats(float expected, float actual, float epsilon)
        {
            var absDiff = Math.Abs(expected - actual);

            if (expected == actual)
            {
                return true;
            }

            return absDiff < epsilon;
        }

        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator", Justification = "ByDesign")]
        private bool CompareDoubles(double expected, double actual, double epsilon)
        {
            var absDiff = Math.Abs(expected - actual);

            if (expected == actual)
            {
                return true;
            }

            return absDiff < epsilon;
        }
    }
}
