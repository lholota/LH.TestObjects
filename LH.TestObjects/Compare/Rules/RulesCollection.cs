namespace LH.TestObjects.Compare.Rules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ValueComparators;
    using ValueComparators.KnownTypes;

    internal class RulesCollection
    {
        private readonly RuleBase[] orderedRules;

        public RulesCollection(IEnumerable<PropertySelectionRule> customRules)
        {
            var rules = new List<RuleBase>();
            this.AddDefaultRules(rules);
            rules.AddRange(customRules.OrderBy(x => x.OrderIndex));
            rules.Reverse();

            this.orderedRules = rules.ToArray();
        }

        internal T GetOptions<T>(ValueComparison comparison) where T : new()
        {
            var rule = this.orderedRules
                .OfType<PropertySelectionRule>()
                .FirstOrDefault(x => x.IsMatch(comparison) && x.Options.ValueComparatorOptions != null);

            if (rule != null)
            {
                return (T)rule.Options.ValueComparatorOptions;
            }

            return new T();
        }

        internal IValueComparator GetComparator(ValueComparison valueComparison)
        {
            var rule = this.orderedRules
                .First(x => x.IsMatch(valueComparison) && x.CanCompare);

            if (rule == null)
            {
                var message = string.Format(
                    "No value comparator could be found for the property {0}.",
                    valueComparison.PropertyPath);

                throw new NotSupportedException(message);
            }

            return rule.Comparator;
        }

        internal bool IsIgnored(ValueComparison valueComparison)
        {
            return this.orderedRules
                .OfType<PropertySelectionRule>()
                .Any(x => x.IsMatch(valueComparison) && x.Options.IsIgnored);
        }

        private void AddDefaultRules(List<RuleBase> rules)
        {
            this.AddValueComparator<RecursivePropertyComparator>(rules);
            this.AddValueComparator<ObjectValueComparator>(rules);
            this.AddValueComparator<CollectionValueComparator>(rules);
            this.AddValueComparator<DictionaryValueComparator>(rules);
            this.AddValueComparator<StringValueComparator>(rules);
            this.AddValueComparator<NumberValueComparator>(rules);
            this.AddValueComparator<FloatValueComparator>(rules);
            this.AddValueComparator<TimeSpanValueComparator>(rules);
            this.AddValueComparator<DateTimeValueComparator>(rules);
            this.AddValueComparator<DynamicObjectValueComparator>(rules);
        }

        private void AddValueComparator<T>(List<RuleBase> rules)
            where T : IValueComparator, new()
        {
            var rule = new ValueComparatorRule();
            rule.ValueComparator = new T();

            rules.Add(rule);
        }
    }
}
