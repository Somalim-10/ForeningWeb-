using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForeningWeb.Models;
using System;

namespace ForeningWeb.Tests.Models
{
    [TestClass]
    public class KontaktTests
    {
        private Kontakt _kontakt;

        [TestInitialize]
        public void TestInitialize()
        {
            _kontakt = new Kontakt { Id = 1, Navn = "Ali", Email = "ali@email.com", Telefon = "+4512345678", Adresse = "Testvej 1" };
        }

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

        [TestMethod]
        public void Navn_ValidValue_SetsValue()
        {
            _kontakt.Navn = "Mohamed";
            Assert.AreEqual("Mohamed", _kontakt.Navn);
        }

        [TestMethod]
        public void Navn_Empty_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => _kontakt.Navn = "");
        }

        [TestMethod]
        public void Navn_TooLong_ThrowsArgumentException()
        {
            string longName = new string('a', 101);
            Assert.ThrowsException<ArgumentException>(() => _kontakt.Navn = longName);
        }

        [TestMethod]
        public void Email_Invalid_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => _kontakt.Email = "not-an-email");
        }

        [TestMethod]
        public void Telefon_Invalid_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() => _kontakt.Telefon = "123abc");
        }

        [TestMethod]
        public void Id_Negative_ThrowsArgumentOutOfRangeException()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _kontakt.Id = -1);
        }
    }
}