namespace LH.TestObjects.Tests.Compare
{
    using FluentAssertions;
    using NUnit.Framework;

    [TestFixture]
    public class WhenSelectingProperties : ComparatorTestsBase, IPropertySelectorTests
    {
        [Test]
        public void ThenShouldPassWhenIgnoringPropertiesByPredicate()
        {
            this.ObjA.StringProp2 = this.ObjB.StringProp2;
            this.ObjA.IntProp = this.ObjB.IntProp;

            this.Comparator
                .PropertiesMatching(x => x.Name == "StringProp")
                .Ignore();

            var result = this.Comparator.Compare(this.ObjA, this.ObjB);

            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldPassWhenIgnoringPropertiesByType()
        {
            this.ObjA.IntProp = this.ObjB.IntProp;

            this.Comparator
                .PropertiesOfType<string>()
                .Ignore();

            var result = this.Comparator.Compare(this.ObjA, this.ObjB);

            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldPassWhenIgnoringPropertiesByTypeAndPredicate()
        {
            this.ObjA.StringProp2 = this.ObjB.StringProp2;
            this.ObjA.IntProp = this.ObjB.IntProp;

            this.Comparator
                .PropertiesOfType<string>(x => x.Name == "StringProp")
                .Ignore();

            var result = this.Comparator.Compare(this.ObjA, this.ObjB);

            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldPassWhenIgnoringPropertiesByExpression()
        {
            this.ObjA.StringProp2 = this.ObjB.StringProp2;
            this.ObjA.IntProp = this.ObjB.IntProp;

            this.Comparator
                .Property(x => x.StringProp)
                .Ignore();

            var result = this.Comparator.Compare(this.ObjA, this.ObjB);
            
            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldPassWhenIgnoringMultiplePropertiesByExpression()
        {
            this.ObjA.StringProp2 = this.ObjB.StringProp2;

            this.Comparator
                .Property(x => x.StringProp)
                .Ignore();

            this.Comparator
                .Property(x => x.IntProp)
                .Ignore();

            var result = this.Comparator.Compare(this.ObjA, this.ObjB);

            result.AreSame.Should().BeTrue();
        }
    }
}