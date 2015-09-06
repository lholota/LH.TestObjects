namespace LH.TestObjects.Tests.Compare.ValueComparators.DictionaryValueComparator
{
    using System.Collections;
    using System.Linq;
    using Domain;
    using FluentAssertions;
    using NUnit.Framework;
    using TestObjects.Compare;

    [TestFixture]
    public abstract class WhenComparingIDictionaryBase<T>
        where T : IDictionary, new()
    {
        private T objA;
        private T objB;
        private ObjectComparator<T> comparator;

        // TODO Check the values are compared recursively!!!

        [SetUp]
        public void Setup()
        {
            this.objA = this.CreateDictionary();
            this.objB = this.CreateDictionary();
            this.comparator = new ObjectComparator<T>();
        }

        [Test]
        public void ThenShouldPassWhenValuesMatch()
        {
            var result = this.comparator.Compare(this.objA, this.objB);

            result.AreSame.Should().BeTrue();
        }

        [Test]
        public void ThenShouldFailIfKeyIsMissing()
        {
            this.objB.Remove("Key2");

            var result = this.comparator.Compare(this.objA, this.objB);

            result.AreSame.Should().BeFalse();
            result.Differences.Single().PropertyPath.Should().Be("[Key2]");
        }

        [Test]
        public void ThenShouldFailIfValuesDiffer()
        {
            this.objA["Key1"] = "AAA";
            this.objB["Key1"] = "BBB";

            var result = this.comparator.Compare(this.objA, this.objB);

            result.AreSame.Should().BeFalse();

            var difference = result.Differences.Single();
            difference.PropertyPath.Should().Be("[Key1]");
        }

        [Test]
        public void ThenShouldFailIfOneValueIsNull()
        {
            this.objB["Key2"] = null;
            
            var result = this.comparator.Compare(this.objA, this.objB);

            result.AreSame.Should().BeFalse();
            result.Differences.Single().PropertyPath.Should().Be("[Key2]");
        }

        [Test]
        public void ThenPathShouldBeCorrectWhenIsNested()
        {
            var domainA = new DictionaryDomain<T> { DictProp = this.CreateDictionary() };
            var domainB = new DictionaryDomain<T> { DictProp = this.CreateDictionary() };

            domainA.DictProp["Key1"] = "AAA";
            domainB.DictProp["Key1"] = "BBB";

            var domainComparator = new ObjectComparator<DictionaryDomain<T>>();
            var result = domainComparator.Compare(domainA, domainB);

            result.AreSame.Should().BeFalse();
            result.Differences.Single().PropertyPath.Should().Be("DictProp[Key1]");
        }

        [Test]
        public void ThenShouldFailWhenNestedObjectsInValuesDiffer()
        {
            var domainA = SimpleDomain.CreateObjectWithValueSet1();
            var domainB = SimpleDomain.CreateObjectWithValueSet1();

            domainA.StringProp = "AAA";
            domainB.StringProp = "BBB";

            this.objA["Key1"] = domainA;
            this.objB["Key1"] = domainB;

            var result = this.comparator.Compare(this.objA, this.objB);
            result.AreSame.Should().BeFalse();
            result.Differences.Single().PropertyPath.Should().Be("[Key1].StringProp");
        }

        protected T CreateDictionary()
        {
            var result = new T();
            result.Add("Key1", "Value1");
            result.Add("Key2", "Value2");

            return result;
        }
    }
}