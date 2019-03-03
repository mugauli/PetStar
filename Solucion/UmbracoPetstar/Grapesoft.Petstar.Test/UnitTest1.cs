using System;
using Grapesoft.Petstar.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Grapesoft.Petstar.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Your custom code goes here
            new Data().save();
        }
    }
}
