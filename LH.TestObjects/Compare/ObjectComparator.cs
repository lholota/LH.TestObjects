namespace LH.TestObjects.Compare
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public class ObjectComparator<TUserType> : ITypeSpecificComparisonPropertySelector<TUserType>
    {
        public ILoggingConfiguration Log { get; set; }

        public IGenericActions PropertiesMatching(Func<PropertyInfo, bool> predicate = null)
        {
            throw new NotImplementedException();
        }

        public ITypeSpecificComparisonActions<TProp> Property<TProp>(Expression<Func<TUserType, TProp>> propertyExpression)
        {
            throw new NotImplementedException();
        }

        public ITypeSpecificComparisonActions<TProp> PropertiesOfType<TProp>(Func<PropertyInfo, bool> predicate = null)
        {
            throw new NotImplementedException();
        }
    }
}