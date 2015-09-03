namespace LH.TestObjects.Tests.Domain
{
    public class SimpleDomainInheritor : SimpleDomain
    {
        public static new SimpleDomainInheritor CreateObjectWithValueSet1()
        {
            return new SimpleDomainInheritor
            {
                StringProp = "AAA",
                StringProp2 = "AAA",
                IntProp = 1
            };
        }
    }
}