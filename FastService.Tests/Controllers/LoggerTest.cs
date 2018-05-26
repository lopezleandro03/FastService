using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NLog;
using System.IO;

namespace FastService.Tests.Controllers
{
    [TestClass]
    public class LoggerTest
    {
        [TestMethod]
        public void LogToDb()
        {
            var logger = LogManager.GetCurrentClassLogger();
            Exception e = new FileNotFoundException();
            logger.Error(e);
        }

    }
}
