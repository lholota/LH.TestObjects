﻿namespace LH.TestObjects.Tests.Compare
{
    using System;
    using System.Linq;
    using Domain;
    using FluentAssertions;
    using NUnit.Framework;
    using TestObjects.Compare;

    [TestFixture]
    public class WhenSelectingProperties : ComparatorTestsBase, IPropertySelectorTests
    {
        [Test]
        public void ThenShouldPassWhenIgnoringPropertiesByPredicate()
        {
            var objA = SimpleDomain.CreateObjectWithValueSet1();
            var objB = SimpleDomain.CreateObjectWithValueSet1();

            objA.StringProp = "AAA";
            objB.StringProp = "BBB";

            this.Comparator
                .PropertiesMatching(x => x.PropertyName == "StringProp")
                .Ignore();

            var result = this.Comparator.Compare(objA, objB);

            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldPassWhenIgnoringPropertiesByType()
        {
            var objA = SimpleDomain.CreateObjectWithValueSet1();
            var objB = SimpleDomain.CreateObjectWithValueSet2();

            objA.IntProp = objB.IntProp;

            this.Comparator
                .PropertiesOfType<string>()
                .Ignore();

            var result = this.Comparator.Compare(objA, objB);

            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldPassWhenIgnoringPropertiesByTypeAndPredicate()
        {
            var objA = SimpleDomain.CreateObjectWithValueSet1();
            var objB = SimpleDomain.CreateObjectWithValueSet1();

            objA.StringProp = "AAA";
            objB.StringProp = "BBB";

            this.Comparator
                .PropertiesOfType<string>(predicate: x => x.PropertyName == nameof(objA.StringProp))
                .Ignore();

            var result = this.Comparator.Compare(objA, objB);

            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldPassWhenIgnoringPropertiesByExpression()
        {
            var objA = SimpleDomain.CreateObjectWithValueSet1();
            var objB = SimpleDomain.CreateObjectWithValueSet1();

            objA.StringProp = "AAA";
            objB.StringProp = "BBB";

            this.Comparator
                .Property(x => x.StringProp)
                .Ignore();

            var result = this.Comparator.Compare(objA, objB);
            
            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldFailWhenIgnoringByExpressionAndBothStringPropsDiffer()
        {
            var objA = SimpleDomain.CreateObjectWithValueSet1();
            var objB = SimpleDomain.CreateObjectWithValueSet2();

            objA.IntProp = objB.IntProp;

            this.Comparator
                .Property(x => x.StringProp)
                .Ignore();

            var result = this.Comparator.Compare(objA, objB);

            result.AreSame.Should().BeFalse();
            result.Differences.Should().NotBeNull();
            result.Differences.Count().Should().Be(1);
        }

        [Test]
        public void ThenShouldPassWhenIgnoringMultiplePropertiesByExpression()
        {
            var objA = SimpleDomain.CreateObjectWithValueSet1();
            var objB = SimpleDomain.CreateObjectWithValueSet2();

            objA.StringProp2 = objB.StringProp2;

            this.Comparator
                .Property(x => x.StringProp)
                .Ignore();

            this.Comparator
                .Property(x => x.IntProp)
                .Ignore();

            var result = this.Comparator.Compare(objA, objB);

            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldFailWhenNotIgnoringInheritedTypes()
        {
            var objA = ComplexDomainWithInheritor.CreateObject();
            var objB = ComplexDomainWithInheritor.CreateObject();

            objA.Simple.StringProp = "AAA";
            objB.Simple.StringProp = "BBB";

            var comparator = Extensions.CreateComparator<ComplexDomainWithInheritor>();
            comparator
                .PropertiesOfType<SimpleDomain>(false)
                .Ignore();

            var result = comparator.Compare(objA, objB);
            result.AreSame.Should().BeFalse();
        }

        [Test]
        public void ThenShouldSucceedWhenIgnoringInheritedTypes()
        {
            var objA = ComplexDomainWithInheritor.CreateObject();
            var objB = ComplexDomainWithInheritor.CreateObject();

            objA.Simple.StringProp = "AAA";
            objB.Simple.StringProp = "BBB";

            var comparator = Extensions.CreateComparator<ComplexDomainWithInheritor>();
            comparator
                .PropertiesOfType<SimpleDomain>()
                .Ignore();

            var result = comparator.Compare(objA, objB);
            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldSucceedWhenIgnoringByDeclarationType()
        {
            var objA = SimpleDomainInheritor.CreateObjectWithValueSet1();
            var objB = SimpleDomainInheritor.CreateObjectWithValueSet1();

            objA.StringProp = "AAA";
            objB.StringProp = "BBB";

            var comparator = Extensions.CreateComparator<SimpleDomainInheritor>();
            comparator
                .PropertiesDeclaredInType(typeof(SimpleDomain))
                .Ignore();

            var result = comparator.Compare(objA, objB);
            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldFailWhenIgnoringByDeclarationTypeAndOtherValuesDiffer()
        {
            var objA = SimpleDomainInheritor.CreateObjectWithValueSet1();
            var objB = SimpleDomainInheritor.CreateObjectWithValueSet1();

            objA.InheritorStringProp = "AAA";
            objB.InheritorStringProp = "BBB";

            var comparator = Extensions.CreateComparator<SimpleDomainInheritor>();
            comparator
                .PropertiesDeclaredInType(typeof(SimpleDomain))
                .Ignore();

            var result = comparator.Compare(objA, objB);
            result.AreSame.Should().BeFalse();
            result.Differences.Count().Should().Be(1);
            result.Differences.Single().PropertyPath.Should().Be(nameof(objA.InheritorStringProp));
        }

        [Test]
        public void ThenShouldUseLastMatchingRule()
        {
            var objA = SimpleDomain.CreateObjectWithValueSet1();
            var objB = SimpleDomain.CreateObjectWithValueSet1();

            objA.StringProp = "AAA";
            objB.StringProp = "BBB";

            this.Comparator
                .Property(x => x.StringProp)
                .CustomCompare(x =>
                {
                    Assert.Fail();
                    return false;
                });

            this.Comparator
                .Property(x => x.StringProp)
                .CustomCompare(x => true);

            var result = this.Comparator.Compare(objA, objB);
            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldFailWhenExpressionRuleAndNestedPropertyDiffers()
        {
            var comparator = new ObjectComparator<HierarchyDomain>();
            comparator
                .Property(x => x.Name)
                .Ignore();

            var objA = new HierarchyDomain
            {
                Name = "AAA",
                ChildNode = new HierarchyDomain
                {
                    Name = "BBB"
                }
            };

            var objB = new HierarchyDomain
            {
                Name = "AAA",
                ChildNode = new HierarchyDomain
                {
                    Name = "CCC"
                }
            };

            var result = comparator.Compare(objA, objB);
            result.AreSame.Should().BeFalse();
            result.Differences.Single().PropertyPath.Should().Be("ChildNode.Name");
        }

        [Test]
        public void ThenShouldPassWhenValuesDifferByCaseAndRootPropertyOptionsAreApplied()
        {
            var stringComparator = new ObjectComparator<string>();
            stringComparator.Root
                .WithCaseSensitivity(StringComparison.OrdinalIgnoreCase);

            var result = stringComparator.Compare("AAA", "aaa");
            result.AreSame.Should().BeTrue();
        }
    }
}