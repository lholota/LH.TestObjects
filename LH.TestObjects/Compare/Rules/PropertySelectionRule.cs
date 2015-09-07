namespace LH.TestObjects.Compare.Rules
{
    using ValueComparators;

    internal class PropertySelectionRule : RuleBase
    {
        public PropertySelectionRule()
        {
            this.Selection = new PropertySelection();
        }

        public PropertySelection Selection { get; }

        public override bool CanCompare
        {
            get { return this.Options.CustomCompare != null; }
        }

        public override IValueComparator Comparator
        {
            get
            {
                return new CustomValueComparator(this.Options.CustomCompare);
            }
        }

        public override bool IsMatch(ValueComparison valueComparison)
        {
            return this.Selection.IsMatch(valueComparison.PropertyPathItem);
        }
    }
}
