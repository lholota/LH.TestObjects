namespace LH.TestObjects
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    internal class PropertySelection
    {
        public Func<PropertyInfo, bool> Predicate { get; set; }

        public Expression PropertyExpression { get; set; }

        public Type PropertyType { get; set; }

        public bool IsMatch(PropertyInfo propertyInfo)
        {
            throw new NotImplementedException();
        }
    }
}