namespace LH.TestObjects.Tests.Compare
{
    using Domain;
    using FluentAssertions;
    using NUnit.Framework;
    using TestObjects.Compare;

    [TestFixture]
    public class WhenComparingHierarchy
    {
        [Test]
        public void ThenShouldPassIfNodesAreRecursive()
        {
            var objA = this.CreateReferenceLoop();
            var objB = this.CreateReferenceLoop();

            var comparator = new ObjectComparator<HierarchyDomain>();
            var result = comparator.Compare(objA, objB);
            result.AreSame.Should().BeTrue();
        }

        private HierarchyDomain CreateReferenceLoop()
        {
            var firstNode = new HierarchyDomain { Name = "First" };
            var secondNode = new HierarchyDomain { Name = "Second" };

            firstNode.ChildNode = secondNode;
            secondNode.ChildNode = firstNode;

            return firstNode;
        }
    }
}