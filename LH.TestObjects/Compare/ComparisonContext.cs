namespace LH.TestObjects.Compare
{
    using System.Diagnostics.CodeAnalysis;
    using System.Reflection;

    internal class ComparisonContext : IComparisonContext
    {
        public ComparisonContext(PropertyPathItem propertyPath, object expected, object actual)
        {
            this.PropertyPathItem = propertyPath;
            this.ExpectedValue = expected;
            this.ActualValue = actual;
        }

        public bool AreSame { get; set; }

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

    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Related types.")]
    internal class ComparisonContext<TProp> : ComparisonContext, IComparisonContext<TProp>
    {
        public ComparisonContext(ComparisonContext genericContext)
            : base(genericContext.PropertyPathItem, genericContext.ExpectedValue, genericContext.ActualValue)
        {
            this.ExpectedValue = (TProp)genericContext.ExpectedValue;
            this.ActualValue = (TProp)genericContext.ActualValue;
            this.AreSame = genericContext.AreSame;
        }

        public new TProp ExpectedValue { get; }

        public new TProp ActualValue { get; }
    }
}