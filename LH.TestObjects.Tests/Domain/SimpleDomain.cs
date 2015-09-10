namespace LH.TestObjects.Tests.Domain
{
    public class SimpleDomain
    {
        public static SimpleDomain CreateObjectWithValueSet1()
        {
            return new SimpleDomain
            {
                StringProp = "AAA".EnsureUniqueInstance(),
                StringProp2 = "AAA".EnsureUniqueInstance(),
                IntProp = 1
            };            
        }

        public static SimpleDomain CreateObjectWithValueSet2()
        {
            return new SimpleDomain
            {
                StringProp = "BBB".EnsureUniqueInstance(),
                StringProp2 = "BBB".EnsureUniqueInstance(),
                IntProp = 2
            };
        }

        public static SimpleDomain CreateObjectWithValueSet3()
        {
            return new SimpleDomain
            {
                StringProp = "CCC".EnsureUniqueInstance(),
                StringProp2 = "CCC".EnsureUniqueInstance(),
                IntProp = 3
            };
        }

        public string StringProp { get; set; }

        public string StringProp2 { get; set; }

        public int IntProp { get; set; }
    }
}
