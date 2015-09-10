namespace LH.TestObjects.Compare.ValueComparators.KnownTypes
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Linq;
    using System.Linq.Expressions;
    using Microsoft.CSharp.RuntimeBinder;

    internal class DynamicObjectValueComparator : IValueComparator
    {
        public bool CanHandle(Type type)
        {
            return type == typeof(ExpandoObject) 
                || type == typeof(DynamicObject)
                || type.IsSubclassOf(typeof(ExpandoObject))
                || type.IsSubclassOf(typeof(DynamicObject));
        }

        public bool Compare(ComparisonContext context, ValueComparison comparison)
        {
            var actualProps = this.GetPropertyNames(comparison.ActualValue)
                .OrderBy(x => x)
                .ToArray();

            var expectedProps = this.GetPropertyNames(comparison.ExpectedValue)
                .OrderBy(x => x)
                .ToArray();

            if (actualProps.Length != expectedProps.Length)
            {
                comparison.Message = string.Format(
                    "The objects have a different number of properties, expected: {0}, actual: {1}.\r\nExpected properties: {2}\r\nActual properties: {3}", 
                    expectedProps.Length,
                    actualProps.Length,
                    string.Join(", ", expectedProps),
                    string.Join(", ", actualProps));

                context.AddDifference(comparison);
                return false;
            }

            if (!expectedProps.SequenceEqual(actualProps))
            {
                comparison.Message = string.Format(
                    "The objects have different properties.\r\nExpected: {0}\r\nActual: {1}",
                    string.Join(", ", expectedProps),
                    string.Join(", ", actualProps));

                context.AddDifference(comparison);
                return false;
            }

            var areEqual = true;
            foreach (var propertyName in expectedProps)
            {
                var actualValue = this.GetPropertyValue(comparison.ActualValue, propertyName);
                var expectedValue = this.GetPropertyValue(comparison.ExpectedValue, propertyName);

                var propertyPath = new PropertyPathItem(propertyName, comparison.PropertyPathItem, false);
                if (!context.CompareItem(expectedValue, actualValue, propertyPath))
                {
                    areEqual = false;
                }
            }

            return areEqual;
        }

        private IEnumerable<string> GetPropertyNames(dynamic dynamicObj)
        {
            var metaDataProvider = (IDynamicMetaObjectProvider)dynamicObj;
            var metaData = metaDataProvider.GetMetaObject(Expression.Constant(dynamicObj));

            return metaData.GetDynamicMemberNames();
        }

        private object GetPropertyValue(dynamic dynamicObj, string propertyName)
        {
            if (this.IsOfType<ExpandoObject>(dynamicObj.GetType()))
            {
                var dictionary = (IDictionary<string, object>)dynamicObj;
                return dictionary[propertyName];
            }

            var binder = (GetMemberBinder)Binder.GetMember(0, propertyName, dynamicObj.GetType(), new[]
                {
                    CSharpArgumentInfo.Create(0, null)
                });

            object value;
            dynamicObj.TryGetMember(binder, out value);

            return value;
        }

        private bool IsOfType<T>(Type type)
        {
            return type == typeof(T) || type.IsSubclassOf(typeof(T));
        }
    }
}