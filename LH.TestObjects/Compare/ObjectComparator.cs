namespace LH.TestObjects.Compare
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// This object allows you to compare two objects with a custom configuration.
    /// </summary>
    /// <typeparam name="TUserType">Compared type</typeparam>
    public class ObjectComparator<TUserType> : IComparatorTypeSpecificPropertySelector<TUserType>
    {
        private readonly IList<ComparatorPropertyRule> propertyRules; 

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectComparator{TUserType}" /> class.
        /// </summary>
        public ObjectComparator()
        {
            this.propertyRules = new List<ComparatorPropertyRule>();
            this.Log = new Logger();
        }
        
        /// <summary>
        /// Gets the logging configuration.
        /// </summary>
        public ILoggerConfiguration Log { get; }

        /// <summary>
        /// Compares two objects and returns a summary of the differences.
        /// </summary>
        /// <param name="expected">The object containing the expected values.</param>
        /// <param name="actual">The actual object.</param>
        /// <returns><see cref="ComparisonResult"/> summarizing the differences between the provided objects.</returns>
        public IComparisonResult Compare(TUserType expected, TUserType actual)
        {
            var prioritizedSelections = this.propertyRules.Reverse().ToArray();
            
            var recursivePropertyComparator = new RecursivePropertyComparator(this.Log, prioritizedSelections);
            recursivePropertyComparator.CompareRecursively(expected, actual);

            return recursivePropertyComparator.Result;
        }

        /// <inheritdoc/>
        public IGenericSelectionActions PropertiesMatching(Func<PropertyInfo, bool> predicate = null)
        {
            var rule = new ComparatorPropertyRule();
            rule.Selection.Predicate = predicate;

            this.AddPropertyRule(rule);

            return new ComparatorGenericSelectionActions(rule.Options);
        }

        /// <inheritdoc/>
        public IComparatorTypeSpecificSelectionActions<TProp> Property<TProp>(Expression<Func<TUserType, TProp>> propertyExpression)
        {
            var rule = new ComparatorPropertyRule();
            rule.Selection.PropertyExpression = propertyExpression;
            rule.Selection.IncludeInheritedTypes = false;
            rule.Selection.PropertyType = typeof(TProp);

            this.AddPropertyRule(rule);

            return new ComparatorTypeSpecificSelectionActions<TProp>(rule.Options);
        }

        /// <inheritdoc/>
        public IComparatorTypeSpecificSelectionActions<TProp> PropertiesOfType<TProp>(bool includeInheritedTypes = true, Func<PropertyInfo, bool> predicate = null)
        {
            var rule = new ComparatorPropertyRule();
            rule.Selection.IncludeInheritedTypes = includeInheritedTypes;
            rule.Selection.PropertyType = typeof(TProp);
            
            this.AddPropertyRule(rule);

            return new ComparatorTypeSpecificSelectionActions<TProp>(rule.Options);
        }

        private void AddPropertyRule(ComparatorPropertyRule rule)
        {
            rule.OrderIndex = this.propertyRules.Count;
            this.propertyRules.Add(rule);
        }
    }
}