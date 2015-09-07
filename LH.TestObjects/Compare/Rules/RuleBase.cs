namespace LH.TestObjects.Compare.Rules
{
    using ValueComparators;

    internal abstract class RuleBase
    {
        protected RuleBase()
        {
            this.Options = new PropertyOptions();
        }

        public int OrderIndex { get; set; }

        public PropertyOptions Options { get; private set; }

        public abstract bool CanCompare { get; }

        public abstract IValueComparator Comparator { get; }

        public abstract bool IsMatch(PropertyPathItem propertyPath);
    }
}