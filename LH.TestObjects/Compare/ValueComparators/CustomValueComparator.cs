namespace LH.TestObjects.Compare.ValueComparators
{
    using System;

    internal class CustomValueComparator : IValueComparator
    {
        private readonly Func<IValueComparison, bool> customCompare;

        public CustomValueComparator(Func<IValueComparison, bool> customCompare)
        {
            this.customCompare = customCompare;
        }

        public bool CanHandle(Type type)
        {
            return true;
        }

        public void Compare(ComparisonContext context, ValueComparison comparison)
        {
            if (!this.customCompare.Invoke(comparison))
            {
                context.AddDifference(comparison, "Custom comparator returned false");
            }
        }
    }
}