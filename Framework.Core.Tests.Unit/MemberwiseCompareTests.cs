using FizzWare.NBuilder;
using jixer.Framework.Core;
using jixer.Framework.Core.Tests.Unit.TestClass;
using NUnit.Framework;
using System;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using jixer.Framework.Core.Tests.Unit.Helper;

namespace jixer.Framework.Core.Tests.Unit
{
    [TestFixture]
    public class MemberwiseCompareTests
    {
        [Test]
        public void MemberwiseCompare_returns_true_when_comparing_two_identical_objects()
        {
            // arrange
            TestClass1 obj1 = NBuilderUtility.Create<TestClass1>();
            TestClass1 obj2 = NBuilderUtility.Create<TestClass1>();

            // act
            bool actual = MemberwiseCompare.AreEqual(obj1, obj2);

            // assert
            actual.Should().BeTrue("Objects contain the same data");
        }

        [Test]
        public void MemberwiseCompare_returns_true_when_comparing_two_identical_objects_with_nulls()
        {
            // arrange
            TestClass1 obj1 = NBuilderUtility.Create<TestClass1>();
            TestClass1 obj2 = NBuilderUtility.Create<TestClass1>();
            obj1.ChildClass5.ChildClass1 = null;
            obj1.ChildClass5.ChildClass2 = null;
            obj1.ChildClass5.ChildClass3 = null;
            obj2.ChildClass5.ChildClass1 = null;
            obj2.ChildClass5.ChildClass2 = null;
            obj2.ChildClass5.ChildClass3 = null;

            // act
            bool actual = MemberwiseCompare.AreEqual(obj1, obj2);

            // assert
            actual.Should().BeTrue("Objects contain the same data");
        }

        [Test]
        public void MemberwiseCompare_returns_false_when_comparing_two_different_objects()
        {
            // arrange
            TestClass1 obj1 = NBuilderUtility.Create<TestClass1>();
            TestClass1 obj2 = NBuilderUtility.Create<TestClass1>();
            obj2.ChildClass5.Prop1 = DateTime.MaxValue;

            // act
            bool actual = MemberwiseCompare.AreEqual(obj1, obj2);

            // assert
            actual.Should().BeFalse("Objects contain different data");
        }

        [Test]
        public void MemberwiseCompare_returns_false_when_comparing_two_different_objects_with_nulls()
        {
            // arrange
            TestClass1 obj1 = NBuilderUtility.Create<TestClass1>();
            TestClass1 obj2 = NBuilderUtility.Create<TestClass1>();
            obj2.ChildClass5.ChildClass1 = null;

            // act
            bool actual = MemberwiseCompare.AreEqual(obj1, obj2);

            // assert
            actual.Should().BeFalse("Objects contain different data");
        }

        [Test]
        public void MemberwiseCompare_returns_false_when_comparing_similar_objects_with_different_datetimes()
        {
            // arrange
            TestClass1 obj1 = NBuilderUtility.Create<TestClass1>();
            TestClass1 obj2 = NBuilderUtility.Create<TestClass1>();
            obj1.ChildClass1.Prop1 = DateTime.Now;
            obj2.ChildClass1.Prop1 = DateTime.Now.AddMilliseconds(1);

            // act
            bool actual = MemberwiseCompare.AreEqual(obj1, obj2);

            // assert
            actual.Should().BeFalse("Objects contain different data");
        }

        [Test]
        public void MemberwiseCompare_returns_false_when_comparing_similar_objects_with_different_enum_values()
        {
            // arrange
            TestClass1 obj1 = NBuilderUtility.Create<TestClass1>();
            TestClass1 obj2 = NBuilderUtility.Create<TestClass1>();
            obj1.ChildClass5.ChildClass1.Prop6 = TestEnum.Val4;
            obj2.ChildClass5.ChildClass1.Prop6 = TestEnum.Val3;

            // act
            bool actual = MemberwiseCompare.AreEqual(obj1, obj2);

            // assert
            actual.Should().BeFalse("Objects contain different data");
        }
    }
}
