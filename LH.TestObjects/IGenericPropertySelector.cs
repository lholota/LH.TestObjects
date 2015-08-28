namespace LH.TestObjects
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Object allowing the property selection without the knowledge of their type.
    /// </summary>
    /// <typeparam name="TUserType">Compared/generated object type.</typeparam>
    public interface IGenericPropertySelector<TUserType>
    {
        /// <summary>
        /// Will select properties matching the predicate or if the predicate is not supplied, it will select all properties.
        /// </summary>
        /// <param name="predicate">Predicate to filter the properties</param>
        /// <returns><see cref="IGenericSelectionActions"/> - configuration actions.</returns>
        IGenericSelectionActions PropertiesMatching(Func<PropertyInfo, bool> predicate = null);
    }
}
