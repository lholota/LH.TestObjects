namespace LH.TestObjects.Compare
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// This object allows you to compare two objects with a custom configuration.
    /// </summary>
    /// <typeparam name="TUserType">Compared type</typeparam>
    public class ObjectComparator<TUserType> : ITypeSpecificComparisonPropertySelector<TUserType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectComparator{TUserType}" /> class.
        /// </summary>
        public ObjectComparator()
        {
        }
        
        /// <summary>
        /// Gets the logging configuration.
        /// </summary>
        public ILoggingConfiguration Log { get; private set; }

        /// <summary>
        /// Compares two objects and returns a summary of the differences.
        /// </summary>
        /// <param name="expected">The object containing the expected values.</param>
        /// <param name="actual">The actual object.</param>
        /// <returns><see cref="ComparisonResult"/> summarizing the differences between the provided objects.</returns>
        public ComparisonResult Compare(TUserType expected, TUserType actual)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public IGenericActions PropertiesMatching(Func<PropertyInfo, bool> predicate = null)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public ITypeSpecificComparisonActions<TProp> Property<TProp>(Expression<Func<TUserType, TProp>> propertyExpression)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public ITypeSpecificComparisonActions<TProp> PropertiesOfType<TProp>(Func<PropertyInfo, bool> predicate = null)
        {
            throw new NotImplementedException();
        }
    }
}