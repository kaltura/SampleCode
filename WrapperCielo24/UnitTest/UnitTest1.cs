using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WrapperCielo24;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        static string username = "testscript";
        static string password = "testscript";


        [TestMethod]
        public void LoginTest1()
        {
            Actions actions = new Actions();
            Console.WriteLine(actions.Login(username, password, false));
            Console.WriteLine("Press any key to close console...");
            Console.ReadKey();
        }
    }
}
