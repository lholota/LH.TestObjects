namespace LH.TestObjects.Tests.Compare
{
    using Domain;
    using NUnit.Framework;
    using TestObjects.Compare;

    [TestFixture]
    public abstract class ComparatorTestsBase
    {
        protected ObjectComparator<SimpleDomain> Comparator;

        [SetUp]
        public virtual void Setup()
        {
            this.Comparator = Extensions.CreateComparator<SimpleDomain>();
        }
    }
}