namespace LH.TestObjects.Compare
{
    using System.Reflection;

    internal class ValueComparison : IValueComparison
    {
        public ValueComparison(PropertyPathItem propertyPath, object expected, object actual)
        {
            this.PropertyPathItem = propertyPath;
            this.ExpectedValue = expected;
            this.ActualValue = actual;
        }

        public object ExpectedValue { get; }

        public object ActualValue { get; }

        public PropertyInfo PropertyInfo
        {
            get { return this.PropertyPathItem.PropertyInfo; }
        }

        public string PropertyPath
        {
            get { return this.PropertyPathItem.GetPathString(); }
        }

        internal PropertyPathItem PropertyPathItem { get; }
    }
}
