using ForeningWeb.Data;
using ForeningWeb.Models;
using ForeningWeb.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace ForeningWeb.Tests.Services
{
    [TestClass]
    public class OmServiceTests
    {
        private static OmService CreateService(out ApplicationDbContext db)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(System.Guid.NewGuid().ToString())
                .Options;
            db = new ApplicationDbContext(options);
            return new OmService(db);
        }

        [TestMethod]
        public async Task CreateAsync_Persists_Om()
        {
            var service = CreateService(out var db);
            var om = new Om { Indhold = "Test" };

            var id = await service.CreateAsync(om);
            var saved = await db.Om.FindAsync(id);

            Assert.IsNotNull(saved);
            Assert.AreEqual("Test", saved!.Indhold);
        }

        [TestMethod]
        public async Task GetAllAsync_Returns_All_Entities()
        {
            var service = CreateService(out _);
            await service.CreateAsync(new Om { Indhold = "One" });
            await service.CreateAsync(new Om { Indhold = "Two" });

            var all = await service.GetAllAsync();

            Assert.AreEqual(2, all.Count);
        }

        [TestMethod]
        public async Task UpdateAsync_Changes_Data()
        {
            var service = CreateService(out _);
            var id = await service.CreateAsync(new Om { Indhold = "Before" });

            var entity = await service.FindAsync(id);
            entity!.Indhold = "After";
            await service.UpdateAsync(entity);

            var updated = await service.FindAsync(id);
            Assert.AreEqual("After", updated!.Indhold);
        }

        [TestMethod]
        public async Task DeleteAsync_Removes_Entity()
        {
            var service = CreateService(out _);
            var id = await service.CreateAsync(new Om { Indhold = "Delete" });

            await service.DeleteAsync(id);
            var entity = await service.FindAsync(id);

            Assert.IsNull(entity);
            Assert.AreEqual(0, (await service.GetAllAsync()).Count);
        }
    }
}