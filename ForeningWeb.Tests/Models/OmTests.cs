using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForeningWeb.Models;
using System;

namespace ForeningWeb.Tests.Models
{
    [TestClass]
    public class OmTests
    {
        private Om _om;

        [TestInitialize]
        public void TestInitialize()
        {
            _om = new Om { Id = 1, Indhold = "Noget indhold", BilledePath = "/img/test.png" };
        }

        [TestMethod]
        public void Om_DefaultValues_AreSet()
        {
            var om = new Om();
            Assert.AreEqual(0, om.Id);
            Assert.AreEqual(string.Empty, om.Indhold);
            Assert.IsNull(om.BilledePath);
        }

        [TestMethod]
        public void Indhold_ValidValue_SetsValue()
        {
            _om.Indhold = "Nyt indhold";
            Assert.AreEqual("Nyt indhold", _om.Indhold);
        }

        [TestMethod]
        public void Indhold_Empty_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => _om.Indhold = "");
        }

        [TestMethod]
        public void Id_Negative_ThrowsArgumentOutOfRangeException()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _om.Id = -1);
        }
    }
}