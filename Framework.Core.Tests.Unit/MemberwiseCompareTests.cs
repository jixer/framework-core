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
        public void MemberwiseCompare_returns_true_when_comparing_two_identical_objects_with_enumerable_props()
        {
            // arrange
            TestClass6 obj1 = Builder<TestClass6>.CreateNew().Build();
            obj1.ChildClass1 = Builder<TestClass1>.CreateListOfSize(10).Build();
            TestClass6 obj2 = Builder<TestClass6>.CreateNew().Build();
            obj2.ChildClass1 = Builder<TestClass1>.CreateListOfSize(10).Build();

            // act
            bool actual = MemberwiseCompare.AreEqual(obj1, obj2);

            // assert
            actual.Should().BeTrue("Objects contain the same data");
        }

        [Test]
        public void MemberwiseCompare_returns_true_when_comparing_two_identical_objects_with_enumerable_valuetype_props()
        {
            // arrange
            TestClass7 obj1 = Builder<TestClass7>.CreateNew().Build();
            obj1.ChildArray1 = new[] { "1", "2", "3", "4" };
            obj1.ChildArray2 = new[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            obj1.ChildArray3 = new[] { true, false, false, true };
            TestClass7 obj2 = Builder<TestClass7>.CreateNew().Build();
            obj2.ChildArray1 = new[] { "1", "2", "3", "4" };
            obj2.ChildArray2 = new[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            obj2.ChildArray3 = new[] { true, false, false, true };


            // act
            bool actual = MemberwiseCompare.AreEqual(obj1, obj2);

            // assert
            actual.Should().BeTrue("Objects contain the same data");
        }

        [Test]
        public void MemberwiseCompare_returns_true_when_comparing_two_identical_objects_with_null_enumerable_valuetype_props()
        {
            // arrange
            TestClass7 obj1 = Builder<TestClass7>.CreateNew().Build();
            TestClass7 obj2 = Builder<TestClass7>.CreateNew().Build();


            // act
            bool actual = MemberwiseCompare.AreEqual(obj1, obj2);

            // assert
            actual.Should().BeTrue("Objects contain the same data");
        }

        [Test]
        public void MemberwiseCompare_returns_true_when_comparing_two_identical_objects_with_empty_enumerable_valuetype_props()
        {
            // arrange
            TestClass7 obj1 = Builder<TestClass7>.CreateNew().Build();
            obj1.ChildArray1 = new string[0];
            obj1.ChildArray2 = new int[0];
            obj1.ChildArray3 = new[] { true, false, false, true };
            TestClass7 obj2 = Builder<TestClass7>.CreateNew().Build();
            obj2.ChildArray1 = new string[0];
            obj2.ChildArray2 = new int[0];
            obj2.ChildArray3 = new[] { true, false, false, true };


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
        public void MemberwiseCompare_returns_false_when_comparing_two_objects_with_different_enumerable_prop_values()
        {
            // arrange
            TestClass6 obj1 = Builder<TestClass6>.CreateNew().Build();
            obj1.ChildClass1 = Builder<TestClass1>.CreateListOfSize(10).Build();
            TestClass6 obj2 = Builder<TestClass6>.CreateNew().Build();
            obj2.ChildClass1 = Builder<TestClass1>.CreateListOfSize(10).Build();
            obj2.ChildClass1[9].Prop3 = 5;

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

        [Test]
        public void MemberwiseCompare_returns_false_when_comparing_objects_with_different_enumerable_prop_sizes_1()
        {
            // arrange
            // arrange
            TestClass6 obj1 = Builder<TestClass6>.CreateNew().Build();
            obj1.ChildClass1 = Builder<TestClass1>.CreateListOfSize(10).Build();
            TestClass6 obj2 = Builder<TestClass6>.CreateNew().Build();
            obj2.ChildClass1 = Builder<TestClass1>.CreateListOfSize(9).Build();

            // act
            bool actual = MemberwiseCompare.AreEqual(obj1, obj2);

            // assert
            actual.Should().BeFalse("Objects contain different data");
        }

        [Test]
        public void MemberwiseCompare_returns_false_when_comparing_objects_with_different_enumerable_prop_sizes_2()
        {
            // arrange
            // arrange
            TestClass6 obj1 = Builder<TestClass6>.CreateNew().Build();
            obj1.ChildClass1 = Builder<TestClass1>.CreateListOfSize(9).Build();
            TestClass6 obj2 = Builder<TestClass6>.CreateNew().Build();
            obj2.ChildClass1 = Builder<TestClass1>.CreateListOfSize(10).Build();

            // act
            bool actual = MemberwiseCompare.AreEqual(obj1, obj2);

            // assert
            actual.Should().BeFalse("Objects contain different data");
        }

        [Test]
        public void MemberwiseCompare_returns_false_when_comparing_objects_with_1_enumerable_prop_set_and_the_other_one_nulled_1()
        {
            // arrange
            // arrange
            TestClass6 obj1 = Builder<TestClass6>.CreateNew().Build();
            obj1.ChildClass1 = Builder<TestClass1>.CreateListOfSize(10).Build();
            TestClass6 obj2 = Builder<TestClass6>.CreateNew().Build();

            // act
            bool actual = MemberwiseCompare.AreEqual(obj1, obj2);

            // assert
            actual.Should().BeFalse("Objects contain different data");
        }

        [Test]
        public void MemberwiseCompare_returns_false_when_comparing_objects_with_1_enumerable_prop_set_and_the_other_one_nulled_2()
        {
            // arrange
            // arrange
            TestClass6 obj1 = Builder<TestClass6>.CreateNew().Build();
            TestClass6 obj2 = Builder<TestClass6>.CreateNew().Build();
            obj2.ChildClass1 = Builder<TestClass1>.CreateListOfSize(10).Build();

            // act
            bool actual = MemberwiseCompare.AreEqual(obj1, obj2);

            // assert
            actual.Should().BeFalse("Objects contain different data");
        }

        [Test]
        public void MemberwiseCompare_returns_true_when_comparing_objects_with_IEnumerable_TestClass1()
        {
            // arrange
            TestClass8 obj1 = Builder<TestClass8>.CreateNew().Build();
            obj1.ChildArray1 = Builder<TestClass1>.CreateListOfSize(10).Build();
            TestClass8 obj2 = Builder<TestClass8>.CreateNew().Build();
            obj2.ChildArray1 = Builder<TestClass1>.CreateListOfSize(10).Build();

            // act
            bool actual = MemberwiseCompare.AreEqual(obj1, obj2);

            // assert
            actual.Should().BeTrue("Objects contain the same data");
        }

        [Test]
        public void MemberwiseCompare_returns_true_when_comparing_two_similar_arrays()
        {
            // arrange
            TestClass8[] obj1 = Builder<TestClass8>.CreateListOfSize(10).Build().ToArray();
            TestClass8[] obj2 = Builder<TestClass8>.CreateListOfSize(10).Build().ToArray();


            // act
            bool actual = MemberwiseCompare.AreArraysEqual(obj1, obj2);

            // assert
            actual.Should().BeTrue("Objects contain the same data");
        }

        [Test]
        public void MemberwiseCompare_returns_true_when_comparing_two_similar_lists()
        {
            // arrange
            IList<TestClass8> obj1 = Builder<TestClass8>.CreateListOfSize(10).Build();
            IList<TestClass8> obj2 = Builder<TestClass8>.CreateListOfSize(10).Build();


            // act
            bool actual = MemberwiseCompare.AreArraysEqual(obj1, obj2);

            // assert
            actual.Should().BeTrue("Objects contain the same data");
        }

        [Test]
        public void MemberwiseCompare_returns_false_when_comparing_two_different_arrays()
        {
            // arrange
            TestClass8[] obj1 = Builder<TestClass8>.CreateListOfSize(10).Build().ToArray();
            TestClass8[] obj2 = Builder<TestClass8>.CreateListOfSize(10).Build().ToArray();
            obj2[9].Prop1 = DateTime.Now.AddMilliseconds(500);

            // act
            bool actual = MemberwiseCompare.AreArraysEqual(obj1, obj2);

            // assert
            actual.Should().BeFalse("Objects contain different data");
        }

        [Test]
        public void MemberwiseCompare_returns_false_when_comparing_two_different_lists()
        {
            // arrange
            IList<TestClass8> obj1 = Builder<TestClass8>.CreateListOfSize(10).Build();
            IList<TestClass8> obj2 = Builder<TestClass8>.CreateListOfSize(10).Build();
            obj2.Last().Prop1 = DateTime.Now.AddMilliseconds(500);


            // act
            bool actual = MemberwiseCompare.AreArraysEqual(obj1, obj2);

            // assert
            actual.Should().BeFalse("Objects contain different data");
        }
    }
}
