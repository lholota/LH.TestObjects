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

        [SetUp]
        public void Setup()
        {
            this.objA = this.CreateDictionary();
            this.objB = this.CreateDictionary();
            this.comparator = Extensions.CreateComparator<T>();
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
        public void ThenShouldFailIfExtraKey()
        {
            this.objB.Add("Key3", new SimpleDomain());

            var result = this.comparator.Compare(this.objA, this.objB);

            result.AreSame.Should().BeFalse();
            result.Differences.Single().PropertyPath.Should().Be("[Key3]");
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

            domainA.DictProp["Key1"] = new SimpleDomain {StringProp = "AAA"};
            domainB.DictProp["Key1"] = new SimpleDomain {StringProp = "BBB"};

            var domainComparator = Extensions.CreateComparator<DictionaryDomain<T>>();
            var result = domainComparator.Compare(domainA, domainB);

            result.AreSame.Should().BeFalse();
            result.Differences.Single().PropertyPath.Should().Be("DictProp[Key1].StringProp");
        }

        [Test]
        public void ThenShouldFailWhenNestedObjectsInValuesDiffer()
        {
            var domainA = (SimpleDomain)this.objA["Key1"];
            var domainB = (SimpleDomain)this.objB["Key1"];

            domainA.StringProp = "AAA";
            domainB.StringProp = "BBB";

            var result = this.comparator.Compare(this.objA, this.objB);
            result.AreSame.Should().BeFalse();
            result.Differences.Single().PropertyPath.Should().Be("[Key1].StringProp");
        }

        protected T CreateDictionary()
        {
            var result = new T();
            result.Add("Key1", SimpleDomain.CreateObjectWithValueSet1());
            result.Add("Key2", SimpleDomain.CreateObjectWithValueSet2());

            return result;
        }
    }
}