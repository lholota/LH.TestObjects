namespace LH.TestObjects
{
    using System;
    using System.Reflection;

    /// <summary>
    /// Hello, world.
    /// </summary>
    /// <typeparam name="TUserType">Dummy parameter.</typeparam>
    public interface IGenericPropertySelector<TUserType>
    {
        /// <summary>
        /// Hello, world.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IGenericActions PropertiesMatching(Func<PropertyInfo, bool> predicate = null);
    }
}
