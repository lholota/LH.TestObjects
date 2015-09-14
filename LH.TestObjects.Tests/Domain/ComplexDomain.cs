namespace LH.TestObjects.Tests.Domain
{
    public class ComplexDomain
    {
        public static ComplexDomain CreateObjectWithValueSet1()
        {
            return new ComplexDomain
            {
                Name = "Set1",
                Simple = SimpleDomain.CreateObjectWithValueSet1()
            };
        }

        public static ComplexDomain CreateObjectWithValueSet2()
        {
            return new ComplexDomain
            {
                Name = "Set2",
                Simple = SimpleDomain.CreateObjectWithValueSet2()
            };
        }

        public string Name { get; set; }

        public SimpleDomain Simple { get; set; }
    }
}