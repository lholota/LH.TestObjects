namespace LH.TestObjects.Compare
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public interface ITypeSpecificComparisonPropertySelector<TUserType> : IGenericPropertySelector<TUserType>
    {
        /// <summary>
        /// Hello, world.
        /// </summary>
        ITypeSpecificComparisonActions<TProp> Property<TProp>(Expression<Func<TUserType, TProp>> propertyExpression);

        /// <summary>
        /// Hello, world.
        /// </summary>
        /// <typeparam name="TProp"></typeparam>
        /// <param name="predicate"></param>
        /// <returns></returns>
        ITypeSpecificComparisonActions<TProp> PropertiesOfType<TProp>(Func<PropertyInfo, bool> predicate = null);
    }
}