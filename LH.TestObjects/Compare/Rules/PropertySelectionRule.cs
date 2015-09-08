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
            get
            {
                return this.Options.CustomCompare != null
                    || this.Options.UseReferenceEquals;
            }
        }

        public override IValueComparator Comparator
        {
            get
            {
                if (this.Options.UseReferenceEquals)
                {
                    return new ReferenceEqualsValueComparator(); 
                }

                return new CustomValueComparator(this.Options.CustomCompare);
            }
        }

        public override bool IsMatch(ValueComparison valueComparison)
        {
            return this.Selection.IsMatch(valueComparison);
        }
    }
}
