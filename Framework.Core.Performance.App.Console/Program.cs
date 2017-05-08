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
    class Program
    {
        static void Main(string[] args)
        {
            MemberwiseCompareTestRunner.Run();

            System.Console.WriteLine($"Press any key to exit...");
            System.Console.ReadKey();
        }
    }
}