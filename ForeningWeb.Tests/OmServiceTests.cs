using ForeningWeb.Data;
using ForeningWeb.Models;
using ForeningWeb.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace ForeningWeb.Tests
{
    public class OmServiceTests
    {
        private static OmService CreateService(out ApplicationDbContext db)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            db = new ApplicationDbContext(options);
            return new OmService(db);
        }

        [Fact]
        public async Task CreateAsync_Persists_Om()
        {
            var service = CreateService(out var db);
            var om = new Om { Indhold = "Test" };

            var id = await service.CreateAsync(om);
            var saved = await db.Om.FindAsync(id);

            Assert.NotNull(saved);
            Assert.Equal("Test", saved!.Indhold);
        }

        [Fact]
        public async Task GetAllAsync_Returns_All_Entities()
        {
            var service = CreateService(out _);
            await service.CreateAsync(new Om { Indhold = "One" });
            await service.CreateAsync(new Om { Indhold = "Two" });

            var all = await service.GetAllAsync();

            Assert.Equal(2, all.Count);
        }

        [Fact]
        public async Task UpdateAsync_Changes_Data()
        {
            var service = CreateService(out _);
            var id = await service.CreateAsync(new Om { Indhold = "Before" });

            var entity = await service.FindAsync(id);
            entity!.Indhold = "After";
            await service.UpdateAsync(entity);

            var updated = await service.FindAsync(id);
            Assert.Equal("After", updated!.Indhold);
        }

        [Fact]
        public async Task DeleteAsync_Removes_Entity()
        {
            var service = CreateService(out _);
            var id = await service.CreateAsync(new Om { Indhold = "Delete" });

            await service.DeleteAsync(id);
            var entity = await service.FindAsync(id);

            Assert.Null(entity);
            Assert.Empty(await service.GetAllAsync());
        }
    }
}