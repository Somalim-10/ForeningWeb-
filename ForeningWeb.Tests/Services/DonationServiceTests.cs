using ForeningWeb.Data;
using ForeningWeb.Models;
using ForeningWeb.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace ForeningWeb.Tests.Services
{
    [TestClass]
    public class DonationServiceTests
    {
        private static DonationService CreateService(out ApplicationDbContext context)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(System.Guid.NewGuid().ToString())
                .Options;
            context = new ApplicationDbContext(options);
            return new DonationService(context);
        }

        [TestMethod]
        public async Task CreateAsync_AddsDonation()
        {
            var service = CreateService(out var db);
            var donation = new Donation { MobilePayNummer = "123" };

            var id = await service.CreateAsync(donation);

            var stored = await db.Donationer.FindAsync(id);
            Assert.IsNotNull(stored);
            Assert.AreEqual("123", stored!.MobilePayNummer);
        }

        [TestMethod]
        public async Task GetAllAsync_ReturnsAllDonations()
        {
            var service = CreateService(out var db);
            db.Donationer.Add(new Donation { MobilePayNummer = "1" });
            db.Donationer.Add(new Donation { MobilePayNummer = "2" });
            await db.SaveChangesAsync();

            var all = await service.GetAllAsync();

            Assert.AreEqual(2, all.Count);
        }

        [TestMethod]
        public async Task FindAsync_ReturnsDonationById()
        {
            var service = CreateService(out var db);
            var donation = new Donation { MobilePayNummer = "321" };
            db.Donationer.Add(donation);
            await db.SaveChangesAsync();

            var found = await service.FindAsync(donation.Id);

            Assert.IsNotNull(found);
            Assert.AreEqual("321", found!.MobilePayNummer);
        }

        [TestMethod]
        public async Task UpdateAsync_UpdatesDonation()
        {
            var service = CreateService(out var db);
            var donation = new Donation { MobilePayNummer = "111" };
            db.Donationer.Add(donation);
            await db.SaveChangesAsync();

            donation.MobilePayNummer = "222";
            await service.UpdateAsync(donation);

            var updated = await db.Donationer.FindAsync(donation.Id);
            Assert.AreEqual("222", updated!.MobilePayNummer);
        }

        [TestMethod]
        public async Task DeleteAsync_RemovesDonation()
        {
            var service = CreateService(out var db);
            var donation = new Donation { MobilePayNummer = "555" };
            db.Donationer.Add(donation);
            await db.SaveChangesAsync();

            await service.DeleteAsync(donation.Id);

            var deleted = await db.Donationer.FindAsync(donation.Id);
            Assert.IsNull(deleted);
        }
    }
}