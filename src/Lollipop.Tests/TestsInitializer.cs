using System;
using System.IO;
using NUnit.Framework;

namespace Lollipop.Tests
{
    [SetUpFixture]
    public class TestsInitializer
    {
        [SetUp]
        public void Setup()
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo("log.config"));
        }
    }
}