namespace LH.TestObjects
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    internal static class ExpressionExtensions
    {
        internal static IEnumerable<string> GetPropertyNames(this Expression method)
        {
            var lambda = method as LambdaExpression;
            if (lambda == null)
            {
                throw new ArgumentNullException("method");
            }

            MemberExpression memberExpr = null;

            if (lambda.Body.NodeType == ExpressionType.Convert)
            {
                memberExpr = ((UnaryExpression)lambda.Body).Operand as MemberExpression;
            }
            else if (lambda.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpr = lambda.Body as MemberExpression;
            }

            return GetPropertyNames(memberExpr);
        }

        private static IEnumerable<string> GetPropertyNames(MemberExpression memberExpr)
        {
            if (memberExpr == null)
            {
                throw new ArgumentException("method");
            }

            yield return memberExpr.Member.Name;

            var nestedExpression = memberExpr.Expression as MemberExpression;
            if (nestedExpression != null)
            {
                foreach (var propertyName in GetPropertyNames(nestedExpression))
                {
                    yield return propertyName;
                }
            }
        }
    }
}