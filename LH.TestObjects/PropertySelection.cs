namespace LH.TestObjects
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using Compare;

    internal class PropertySelection
    {
        public Func<PropertyInfo, bool> Predicate { get; set; }

        public Expression PropertyExpression { get; set; }

        public Type PropertyType { get; set; }

        public bool IsMatch(PropertyPathItem propertyInfo)
        {
            throw new NotImplementedException();
        }
    }
}