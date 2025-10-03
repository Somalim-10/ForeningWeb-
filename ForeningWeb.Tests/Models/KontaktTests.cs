using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForeningWeb.Models;

namespace ForeningWeb.Tests.Models
{
    [TestClass]
    public class KontaktTests
    {
        [TestMethod]
        public void Kontakt_DefaultValues_AreSet()
        {
            var kontakt = new Kontakt();
            Assert.AreEqual(0, kontakt.Id);
            Assert.AreEqual(string.Empty, kontakt.Navn);
            Assert.IsNull(kontakt.Email);
            Assert.IsNull(kontakt.Telefon);
            Assert.IsNull(kontakt.Adresse);
        }
    }
}