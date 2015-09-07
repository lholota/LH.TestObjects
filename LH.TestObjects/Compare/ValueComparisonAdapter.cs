namespace LH.TestObjects.Compare
{
    using System.Reflection;

    internal class ValueComparisonAdapter<TProp> : IValueComparison<TProp>
    {
        private readonly IValueComparison comparison;

        public ValueComparisonAdapter(IValueComparison comparison)
        {
            this.comparison = comparison;
        }

        public bool AreSame { get; set; }

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

        public TProp ActualValue
        {
            get { return (TProp)this.comparison.ActualValue; }
        }

        public PropertyInfo PropertyInfo
        {
            get { return this.comparison.PropertyInfo; }
        }

        public string PropertyPath
        {
            get { return this.comparison.PropertyPath; }
        }
    }
}