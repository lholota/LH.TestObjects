namespace LH.TestObjects.Compare.Rules
{
    using ValueComparators;

    internal class ValueComparatorRule : RuleBase
    {
        public IValueComparator ValueComparator { get; set; }

        public override bool CanCompare
        {
            get { return true; }
        }

        public override IValueComparator Comparator
        {
            get { return this.ValueComparator; }
        }

        public override bool IsMatch(PropertyPathItem propertyPath)
        {
            return this.Comparator.CanHandle(propertyPath.Type);
        }
    }
}
