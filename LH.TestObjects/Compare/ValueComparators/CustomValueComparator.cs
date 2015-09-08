namespace LH.TestObjects.Compare.ValueComparators
{
    using System;

    internal class CustomValueComparator : IValueComparator
    {
        private readonly Func<ValueComparison, ComparisonContext, bool> customCompare;

        public CustomValueComparator(Func<ValueComparison, ComparisonContext, bool> customCompare)
        {
            this.customCompare = customCompare;
        }

        public bool CanHandle(Type type)
        {
            return true;
        }

        public bool Compare(ComparisonContext context, ValueComparison comparison)
        {
            var areSame = this.customCompare.Invoke(comparison, context);
            if (!areSame)
            {
                if (string.IsNullOrEmpty(comparison.Message))
                {
                    comparison.Message = "Custom comparator returned false";
                }
                context.AddDifference(comparison);
            }

            return areSame;
        }
    }
}