using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForeningWeb.Models;

namespace ForeningWeb.MSTests.Models
{
    [TestClass]
    public class DonationTests
    {
        [TestMethod]
        public void Donation_DefaultValues_AreSet()
        {
            var donation = new Donation();
            Assert.AreEqual(0, donation.Id);
            Assert.AreEqual(string.Empty, donation.MobilePayNummer);
            Assert.IsNull(donation.Besked);
            Assert.IsNull(donation.QrKodePath);
        }
    }
}