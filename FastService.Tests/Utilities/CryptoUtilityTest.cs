using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FastService.Tests.Utilities
{
    [TestClass]
    public class CryptoUtilityTest
    {
        [TestMethod]
        public void Test()
        {
            string expected = "this is my test string";
            string a = expected.Crypt();
            string actual = a.Decrypt();
            Assert.AreEqual(expected, actual);
        }
    }
}
