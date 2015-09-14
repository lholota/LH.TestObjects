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

        public bool AreSame { get; set; }

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

        public string Message { get; set; }

        internal PropertyInfo PropertyInfo
        {
            get { return this.PropertyPathItem.PropertyInfo; }
        }

        internal PropertyPathItem PropertyPathItem { get; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            if (ReferenceEquals(obj, this))
            {
                return true;
            }

            if (obj.GetType() != typeof(ValueComparison))
            {
                return false;
            }

            var other = (ValueComparison)obj;
            return ReferenceEquals(this.ActualValue, other.ActualValue)
                   && ReferenceEquals(this.ExpectedValue, other.ExpectedValue);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (ReferenceEquals(this.ActualValue, null) ? 1 : this.ActualValue.GetHashCode())
                   ^ (ReferenceEquals(this.ExpectedValue, null) ? 2 : this.ExpectedValue.GetHashCode());
            }
        }
    }
}
