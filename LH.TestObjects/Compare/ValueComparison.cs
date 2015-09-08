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

        public System.Type PropertyType
        {
            get
            {
                if (this.PropertyInfo != null)
                {
                    return this.PropertyInfo.PropertyType;
                }

                if (this.ExpectedValue != null)
                {
                    return this.ExpectedValue.GetType();
                }

                if (this.ActualValue != null)
                {
                    return this.ActualValue.GetType();
                }

                return null;
            }
        }

        public string PropertyName
        {
            get { return this.PropertyPathItem.ItemDescription; }
        }

        public string PropertyPath
        {
            get { return this.PropertyPathItem.GetPathString(); }
        }

        internal PropertyInfo PropertyInfo
        {
            get { return this.PropertyPathItem.PropertyInfo; }
        }

        internal PropertyPathItem PropertyPathItem { get; }
    }
}
