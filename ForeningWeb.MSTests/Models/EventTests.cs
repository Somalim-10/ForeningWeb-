using Microsoft.VisualStudio.TestTools.UnitTesting;
using ForeningWeb.Models;
using System;

namespace ForeningWeb.MSTests.Models
{
    [TestClass]
    public class EventTests
    {
        [TestMethod]
        public void Event_DefaultValues_AreSet()
        {
            var evt = new Event();
            Assert.AreEqual(0, evt.Id);
            Assert.AreEqual(string.Empty, evt.Titel);
            Assert.AreEqual(default(DateTime), evt.Dato);
            Assert.IsNull(evt.Tidspunkt);
            Assert.IsNull(evt.Beskrivelse);
            Assert.IsNull(evt.ImageUrl);
        }
    }
}