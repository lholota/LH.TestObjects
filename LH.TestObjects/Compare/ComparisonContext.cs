namespace LH.TestObjects.Compare
{
    using System;
    using System.Reflection;
    
    internal class ComparisonContext<TProp> : IComparisonContext
    {
        public bool AreSame { get; internal set; }
        
        public TProp ExpectedValue { get; internal set; }
        
        public TProp ActualValue { get; internal set; }
        
        public PropertyInfo PropertyInfo { get; internal set; }

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

        object IDifference.ExpectedValue
        {
            get { return this.ExpectedValue; }
        }

        object IDifference.ActualValue
        {
            get { return this.ActualValue; }
        }
    }
}