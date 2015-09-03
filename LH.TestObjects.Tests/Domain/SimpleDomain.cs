namespace LH.TestObjects.Tests.Domain
{
    public class SimpleDomain
    {
        public static SimpleDomain CreateObjectWithValueSet1()
        {
            return new SimpleDomain
            {
                StringProp = "AAA",
                StringProp2 = "AAA",
                IntProp = 1
            };            
        }

        public static SimpleDomain CreateObjectWithValueSet2()
        {
            return new SimpleDomain
            {
                StringProp = "BBB",
                StringProp2 = "BBB",
                IntProp = 2
            };
        }

        public string StringProp { get; set; }

        public string StringProp2 { get; set; }

        public int IntProp { get; set; }
    }
}
