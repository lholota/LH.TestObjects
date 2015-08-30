namespace LH.TestObjects.Compare
{
    using System;
    using System.Reflection;
    
    internal class ComparisonContext : IComparisonContext
    {
        public ComparisonContext(PropertyPathItem propertyPath, object expected, object actual)
        {
            this.PropertyPathItem = propertyPath;
            this.ExpectedValue = expected;
            this.ActualValue = actual;
        }

        internal PropertyPathItem PropertyPathItem { get; private set; }

        public bool AreSame { get; internal set; }

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

        public string LogMessage
        {
            get
            {
                if (this.AreSame)
                {
                    return null;
                }

                throw new NotImplementedException();
            }
        }
    }
}