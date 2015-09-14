namespace LH.TestObjects.Tests.Domain
{
    public class ComplexDomainWithInheritor
    {
        public static ComplexDomainWithInheritor CreateObject()
        {
            return new ComplexDomainWithInheritor
            {
                Name = "AAA",
                Simple = SimpleDomainInheritor.CreateObjectWithValueSet1()
            };
        }

        public string Name { get; set; }

        public SimpleDomainInheritor Simple { get; set; }
    }
}
