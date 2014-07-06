using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WrapperCielo24;

namespace UnitTest
{
    [TestClass]
    public class ActionsTest
    {
        static string username = "testscript";
        static string password = "testscript";

        [TestMethod]
        public void Login_UsernamePassword()
        {
            
        }

        [TestMethod]
        public void Login_UsernamePassword_Headers()
        {
            Actions actions = new Actions();
            Console.WriteLine(actions.Login(username, password, false));
            Console.WriteLine("Press any key to close console...");
            Console.ReadKey();
        }

        [TestMethod]
        public void Login_UsernameSecureKey()
        {
            Actions actions = new Actions();
            Console.WriteLine(actions.Login(username, password, false));
            Console.WriteLine("Press any key to close console...");
            Console.ReadKey();
        }

        [TestMethod]
        public void Login_UsernameSecureKey_Headers()
        {
            Actions actions = new Actions();
            Console.WriteLine(actions.Login(username, password, false));
            Console.WriteLine("Press any key to close console...");
            Console.ReadKey();
        }
    }
}
