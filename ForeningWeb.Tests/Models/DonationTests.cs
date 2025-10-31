using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForeningWeb.Models;
using System;

namespace ForeningWeb.Tests.Models
{
    [TestClass]
    public class DonationTests
    {
        private Donation _donation;

        [TestInitialize]
        public void TestInitialize()
        {
            _donation = new Donation { Id = 1, MobilePayNummer = "123456" };
        }

        [TestMethod]
        public void Donation_DefaultValues_AreSet()
        {
            var donation = new Donation();
            Assert.AreEqual(0, donation.Id);
            Assert.AreEqual(string.Empty, donation.MobilePayNummer);
            Assert.IsNull(donation.Besked);
            Assert.IsNull(donation.QrKodePath);
        }

        [TestMethod]
        public void MobilePayNummer_ValidValue_SetsValue()
        {
            _donation.MobilePayNummer = "654321";
            Assert.AreEqual("654321", _donation.MobilePayNummer);
        }

        [TestMethod]
        public void MobilePayNummer_Empty_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => _donation.MobilePayNummer = "");
        }

        [TestMethod]
        public void MobilePayNummer_TooLong_ThrowsArgumentException()
        {
            string longNum = new string('1', 51);
            Assert.ThrowsException<ArgumentException>(() => _donation.MobilePayNummer = longNum);
        }

        [TestMethod]
        public void Id_Negative_ThrowsArgumentOutOfRangeException()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _donation.Id = -1);
        }
    }
}