using jixer.Framework.Core;
using jixer.Framework.Core.Tests.Unit.Helper;
using jixer.Framework.Core.Tests.Unit.TestClass;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jixxer.Framework.Core.Performance.App.Console
{
    public class MemberwiseCompareTestRunner
    {
        private static TestClass1 _t1;
        private static TestClass1 _t2;
        private const int Iterations = 10000;

        public static void Run()
        {
            InitClasses();
            RunTest1(1);
            System.Console.WriteLine("\r\n");

            InitClasses();
            RunTest1(2);
            System.Console.WriteLine("\r\n");

            InitClasses();
            _t1.ChildClass5.Prop1 = DateTime.Now.AddMilliseconds(10); // simulate different date times
            RunTest1(3); // this should result in the fastest times since not all properties need to be analyzed
            System.Console.WriteLine("\r\n");
        }

        private static void RunTest1(int v)
        {
            List<TimeSpan> timing = new List<TimeSpan>();

            Stopwatch sw = new Stopwatch();
            bool result = false;
            for (int i = 0; i < Iterations; i++)
            {
                sw.Start();
                result = MemberwiseCompare.AreEqual<TestClass1>(_t1, _t2);
                timing.Add(sw.Elapsed);
                sw.Reset();
            }

            System.Console.WriteLine($"Pass #{v} test iterations: {Iterations}");
            System.Console.WriteLine($"Values are equal: {result}");
            System.Console.WriteLine($"First time: {timing[0].Ticks} ticks, {timing[0].TotalMilliseconds} ms");
            System.Console.WriteLine($"Second time: {timing[1].Ticks} ticks, {timing[1].TotalMilliseconds} ms");
            System.Console.WriteLine($"Last time: {timing[Iterations - 1].Ticks} ticks, {timing[Iterations - 1].TotalMilliseconds} ms");
            System.Console.WriteLine($"Average time: {timing.Average(x => x.Ticks)} ticks, {timing.Average(x => x.TotalMilliseconds)} ms");
            System.Console.WriteLine($"Min time: {timing.Min().Ticks} ticks, {timing.Min().TotalMilliseconds} ms");
            System.Console.WriteLine($"Max time: {timing.Max().Ticks} ticks, {timing.Max().TotalMilliseconds} ms");
            System.Console.WriteLine($"Total time: {timing.Sum(x => x.Ticks)} ticks, {timing.Sum(x => x.TotalMilliseconds)} ms");
        }

        private static void InitClasses()
        {
            _t1 = NBuilderUtility.Create<TestClass1>();
            _t2 = NBuilderUtility.Create<TestClass1>();
        }
    }
}