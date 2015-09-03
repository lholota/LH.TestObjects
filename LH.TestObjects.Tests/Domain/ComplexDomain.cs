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

        public string Name { get; set; }

        public SimpleDomain Simple { get; set; }
    }
}