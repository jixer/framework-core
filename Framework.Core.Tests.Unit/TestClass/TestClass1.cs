﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jixer.Framework.Core.Tests.Unit.TestClass
{
    public class TestClass1
    {
        public DateTime Prop1 { get; set; }
        public string Prop2 { get; set; }
        public int Prop3 { get; set; }
        public double Prop4 { get; set; }
        public long Prop5 { get; set; }
        public TestEnum Prop6 { get; set; }

        public TestClass2 ChildClass1 { get; set; }
        public TestClass2 ChildClass2 { get; set; }

        public TestClass3 ChildClass3 { get; set; }
        public TestClass4 ChildClass4 { get; set; }
        public TestClass5 ChildClass5 { get; set; }
    }
}
