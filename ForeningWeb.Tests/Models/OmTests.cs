using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForeningWeb.Models;

namespace ForeningWeb.Tests.Models
{
    [TestClass]
    public class OmTests
    {
        [TestMethod]
        public void Om_DefaultValues_AreSet()
        {
            var om = new Om();
            Assert.AreEqual(0, om.Id);
            Assert.AreEqual(string.Empty, om.Indhold);
            Assert.IsNull(om.BilledePath);
        }
    }
}