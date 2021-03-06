﻿namespace LH.TestObjects.Compare
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using Logging;
    using Rules;

    /// <summary>
    /// This object allows you to compare two objects with a custom configuration.
    /// </summary>
    /// <typeparam name="TUserType">Compared type</typeparam>
    public class ObjectComparator<TUserType> : IComparatorTypeSpecificPropertySelector<TUserType>
    {
        private readonly ILogger log;
        private readonly IList<PropertySelectionRule> propertyRules;

        private PropertySelectionRule rootPropertyRule;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectComparator{TUserType}" /> class.
        /// </summary>
        public ObjectComparator()
        {
            this.propertyRules = new List<PropertySelectionRule>();
            this.log = new Logger();

            this.AddRootPropertyRule();
        }

        /// <summary>
        /// Gets the logging configuration.
        /// </summary>
        public ILoggerConfiguration Log
        {
            get { return this.log; }
        }

        /// <summary>
        /// Gets the options actions which can be applied to the root object comparison.
        /// </summary>
        public IComparatorTypeSpecificSelectionActions<TUserType> Root
        {
            get
            {
                return new ComparatorTypeSpecificSelectionActions<TUserType>(this.rootPropertyRule.Options);
            }
        }

        /// <summary>
        /// Compares two objects and returns a summary of the differences.
        /// </summary>
        /// <param name="expected">The object containing the expected values.</param>
        /// <param name="actual">The actual object.</param>
        /// <returns><see cref="ComparisonResult"/> summarizing the differences between the provided objects.</returns>
        public IComparisonResult Compare(TUserType expected, TUserType actual)
        {
            using (var context = new ComparisonContext(this.log, this.propertyRules))
            {
                context.CompareItem(expected, actual, PropertyPathItem.Root);
                return context.Result;
            }
        }

        /// <inheritdoc/>
        public IGenericSelectionActions PropertiesMatching(Func<IValueComparison, bool> predicate = null)
        {
            var rule = new PropertySelectionRule();
            rule.Selection.Predicate = predicate;

            this.AddPropertyRule(rule);

            return new ComparatorGenericSelectionActions(rule.Options);
        }

        /// <inheritdoc/>
        public IGenericSelectionActions PropertiesDeclaredInType(Type declationType)
        {
            var rule = new PropertySelectionRule();
            rule.Selection.DeclarationType = declationType;

            this.AddPropertyRule(rule);

            return new ComparatorGenericSelectionActions(rule.Options);
        }

        /// <inheritdoc/>
        public IComparatorTypeSpecificSelectionActions<TProp> Property<TProp>(Expression<Func<TUserType, TProp>> propertyExpression)
        {
            var rule = new PropertySelectionRule();
            rule.Selection.PropertyExpression = propertyExpression;
            rule.Selection.IncludeInheritedTypes = false;
            rule.Selection.PropertyType = typeof(TProp);

            this.AddPropertyRule(rule);

            return new ComparatorTypeSpecificSelectionActions<TProp>(rule.Options);
        }

        /// <inheritdoc/>
        public IComparatorTypeSpecificSelectionActions<TProp> PropertiesOfType<TProp>(bool includeInheritedTypes = true, Func<IValueComparison, bool> predicate = null)
        {
            var rule = new PropertySelectionRule();
            rule.Selection.IncludeInheritedTypes = includeInheritedTypes;
            rule.Selection.PropertyType = typeof(TProp);
            rule.Selection.Predicate = predicate;
            
            this.AddPropertyRule(rule);

            return new ComparatorTypeSpecificSelectionActions<TProp>(rule.Options);
        }

        private void AddPropertyRule(PropertySelectionRule rule)
        {
            rule.OrderIndex = this.propertyRules.Count;
            this.propertyRules.Add(rule);
        }

        private void AddRootPropertyRule()
        {
            this.rootPropertyRule = new PropertySelectionRule();
            this.rootPropertyRule.Selection.AppliesToRoot = true;
            this.AddPropertyRule(this.rootPropertyRule);
        }
    }
}