namespace LH.TestObjects.Tests.Compare
{
    using NUnit.Framework;
    using TestDomain;
    using TestObjects.Compare;

    [TestFixture]
    public abstract class ComparatorTestsBase
    {
        protected ObjectComparator<SimpleDomain> Comparator;

        [SetUp]
        public virtual void Setup()
        {
            this.Comparator = new ObjectComparator<SimpleDomain>();
        }
    }
}