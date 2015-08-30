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
        private readonly IList<ComparatorPropertySelection> propertySelections; 

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectComparator{TUserType}" /> class.
        /// </summary>
        public ObjectComparator()
        {
            this.propertySelections = new List<ComparatorPropertySelection>();
            this.Log = new Logger();
        }
        
        /// <summary>
        /// Gets the logging configuration.
        /// </summary>
        public ILoggerConfiguration Log { get; private set; }

        /// <summary>
        /// Compares two objects and returns a summary of the differences.
        /// </summary>
        /// <param name="expected">The object containing the expected values.</param>
        /// <param name="actual">The actual object.</param>
        /// <returns><see cref="ComparisonResult"/> summarizing the differences between the provided objects.</returns>
        public IComparisonResult Compare(TUserType expected, TUserType actual)
        {
            var prioritizedSelections = this.propertySelections.Reverse().ToArray();
            
            var recursivePropertyComparator = new RecursivePropertyComparator(this.Log, prioritizedSelections);
            recursivePropertyComparator.CompareRecursively(expected, actual);

            return recursivePropertyComparator.Result;
        }

        /// <inheritdoc/>
        public IGenericSelectionActions PropertiesMatching(Func<PropertyInfo, bool> predicate = null)
        {
            var comparatorPropertySelection = new ComparatorPropertySelection
            {
                Predicate = predicate
            };

            this.propertySelections.Add(comparatorPropertySelection);

            return new ComparatorGenericSelectionActions(comparatorPropertySelection.Options);
        }

        /// <inheritdoc/>
        public IComparatorTypeSpecificSelectionActions<TProp> Property<TProp>(Expression<Func<TUserType, TProp>> propertyExpression)
        {
            var comparatorPropertySelection = new ComparatorPropertySelection
            {
                PropertyExpression = propertyExpression,
                PropertyType = typeof(TProp)
            };

            this.propertySelections.Add(comparatorPropertySelection);

            return new ComparatorTypeSpecificSelectionActions<TProp>(comparatorPropertySelection.Options);
        }

        /// <inheritdoc/>
        public IComparatorTypeSpecificSelectionActions<TProp> PropertiesOfType<TProp>(Func<PropertyInfo, bool> predicate = null)
        {
            var comparatorPropertySelection = new ComparatorPropertySelection
            {
                PropertyType = typeof(TProp)
            };

            this.propertySelections.Add(comparatorPropertySelection);

            return new ComparatorTypeSpecificSelectionActions<TProp>(comparatorPropertySelection.Options);
        }
    }
}