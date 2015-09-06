namespace LH.TestObjects.Compare.ValueComparators
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    internal class FloatValueComparator : IValueComparator, IHasComparatorOptions
    {
        object IHasComparatorOptions.Options
        {
            set { this.Options = (FloatValueComparatorOptions)value; }
        }

        public FloatValueComparatorOptions Options { get; set; }

        public bool CanHandle(Type type)
        {
            return type == typeof(float)
                   || type == typeof(double);
        }

        public bool Compare(IComparisonContext comparisonContext)
        {
            var options = this.Options ?? new FloatValueComparatorOptions();

            if (comparisonContext.PropertyInfo.PropertyType == typeof(float))
            {
                return this.CompareFloats(
                    (float)comparisonContext.ExpectedValue,
                    (float)comparisonContext.ActualValue,
                    options.FloatEpsilon);
            }

            return this.CompareDoubles(
                (double)comparisonContext.ExpectedValue,
                (double)comparisonContext.ActualValue,
                options.DoubleEpsilon);
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
