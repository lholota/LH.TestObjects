namespace LH.TestObjects.Compare
{
    using System;

    internal class ValueComparisonAdapter<TProp> : IValueComparison<TProp>
    {
        private readonly ValueComparison comparison;
        private readonly ComparisonContext context;

        public ValueComparisonAdapter(ValueComparison comparison, ComparisonContext context)
        {
            this.comparison = comparison;
            this.context = context;
        }

        object IValueComparison.ExpectedValue
        {
            get { return this.comparison.ExpectedValue; }
        }

        public TProp ExpectedValue
        {
            get { return (TProp)this.comparison.ExpectedValue; }
        }

        object IValueComparison.ActualValue
        {
            get { return this.comparison.ActualValue; }
        }

        string IValueComparison<TProp>.Message
        {
            get { return this.comparison.Message; }
            set { this.comparison.Message = value; }
        }

        public Type PropertyType
        {
            get { return this.comparison.PropertyType; }
        }

        public string PropertyName
        {
            get { return this.comparison.PropertyName; }
        }

        public TProp ActualValue
        {
            get { return (TProp)this.comparison.ActualValue; }
        }

        public string PropertyPath
        {
            get { return this.comparison.PropertyPath; }
        }

        public string Message
        {
            get { return this.comparison.Message; }
        }

        public bool CompareItem(object expected, object actual, string propertyName)
        {
            var propertyPath = new PropertyPathItem(propertyName, this.comparison.PropertyPathItem, false);
            return this.context.CompareItem(expected, actual, propertyPath);
        }
    }
}