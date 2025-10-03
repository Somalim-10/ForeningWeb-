using ForeningWeb.Data;
using ForeningWeb.Models;
using ForeningWeb.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace ForeningWeb.Tests.Services
{
    [TestClass]
    public class KontaktServiceTests
    {
        private static ApplicationDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(System.Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [TestMethod]
        public async Task CreateAsync_AddsKontakt()
        {
            using var context = CreateContext();
            var service = new KontaktService(context);

            var kontakt = new Kontakt { Navn = "Test" };
            var id = await service.CreateAsync(kontakt);

            Assert.AreNotEqual(0, id);
            var stored = await context.Kontakter.FindAsync(id);
            Assert.AreEqual("Test", stored?.Navn);
        }

        [TestMethod]
        public async Task GetAllAsync_ReturnsAll()
        {
            using var context = CreateContext();
            var service = new KontaktService(context);

            await service.CreateAsync(new Kontakt { Navn = "A" });
            await service.CreateAsync(new Kontakt { Navn = "B" });

            var all = await service.GetAllAsync();
            Assert.AreEqual(2, all.Count);
        }

        [TestMethod]
        public async Task FindAsync_ReturnsKontakt()
        {
            using var context = CreateContext();
            var service = new KontaktService(context);

            var id = await service.CreateAsync(new Kontakt { Navn = "Find" });

            var found = await service.FindAsync(id);
            Assert.IsNotNull(found);
            Assert.AreEqual("Find", found?.Navn);
        }

        [TestMethod]
        public async Task UpdateAsync_UpdatesKontakt()
        {
            using var context = CreateContext();
            var service = new KontaktService(context);

            var id = await service.CreateAsync(new Kontakt { Navn = "Old" });
            var kontakt = await service.FindAsync(id);
            kontakt!.Navn = "New";
            await service.UpdateAsync(kontakt);

            var updated = await service.FindAsync(id);
            Assert.AreEqual("New", updated?.Navn);
        }

        [TestMethod]
        public async Task DeleteAsync_RemovesKontakt()
        {
            using var context = CreateContext();
            var service = new KontaktService(context);

            var id = await service.CreateAsync(new Kontakt { Navn = "Delete" });
            await service.DeleteAsync(id);

            var found = await service.FindAsync(id);
            Assert.IsNull(found);
            var all = await service.GetAllAsync();
            Assert.AreEqual(0, all.Count);
        }
    }
}