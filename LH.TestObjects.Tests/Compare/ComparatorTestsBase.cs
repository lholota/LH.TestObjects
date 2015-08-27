namespace LH.TestObjects.Tests.Compare
{
    using NUnit.Framework;
    using TestDomain;
    using TestObjects.Compare;

    [TestFixture]
    public abstract class ComparatorTestsBase
    {
        protected SimpleDomain ObjA;
        protected SimpleDomain ObjB;
        protected ObjectComparator<SimpleDomain> Comparator;

        [SetUp]
        public virtual void Setup()
        {
            this.Comparator = new ObjectComparator<SimpleDomain>();

            this.ObjA = new SimpleDomain
            {
                StringProp = "AAA",
                StringProp2 = "AAA",
                IntProp = 1
            };

            this.ObjB = new SimpleDomain
            {
                StringProp = "BBB",
                StringProp2 = "BBB",
                IntProp = 2
            };
        }
    }
}