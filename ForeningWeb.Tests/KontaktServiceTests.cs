using ForeningWeb.Data;
using ForeningWeb.Models;
using ForeningWeb.Services;
using Microsoft.EntityFrameworkCore;


namespace ForeningWeb.Tests
{
    public class KontaktServiceTests
    {
        private static ApplicationDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task CreateAsync_AddsKontakt()
        {
            using var context = CreateContext();
            var service = new KontaktService(context);

            var kontakt = new Kontakt { Navn = "Test" };
            var id = await service.CreateAsync(kontakt);

            Assert.NotEqual(0, id);
            var stored = await context.Kontakter.FindAsync(id);
            Assert.Equal("Test", stored?.Navn);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAll()
        {
            using var context = CreateContext();
            var service = new KontaktService(context);

            await service.CreateAsync(new Kontakt { Navn = "A" });
            await service.CreateAsync(new Kontakt { Navn = "B" });

            var all = await service.GetAllAsync();
            Assert.Equal(2, all.Count);
        }

        [Fact]
        public async Task FindAsync_ReturnsKontakt()
        {
            using var context = CreateContext();
            var service = new KontaktService(context);

            var id = await service.CreateAsync(new Kontakt { Navn = "Find" });

            var found = await service.FindAsync(id);
            Assert.NotNull(found);
            Assert.Equal("Find", found?.Navn);
        }

        [Fact]
        public async Task UpdateAsync_UpdatesKontakt()
        {
            using var context = CreateContext();
            var service = new KontaktService(context);

            var id = await service.CreateAsync(new Kontakt { Navn = "Old" });
            var kontakt = await service.FindAsync(id);
            kontakt!.Navn = "New";
            await service.UpdateAsync(kontakt);

            var updated = await service.FindAsync(id);
            Assert.Equal("New", updated?.Navn);
        }

        [Fact]
        public async Task DeleteAsync_RemovesKontakt()
        {
            using var context = CreateContext();
            var service = new KontaktService(context);

            var id = await service.CreateAsync(new Kontakt { Navn = "Delete" });
            await service.DeleteAsync(id);

            var found = await service.FindAsync(id);
            Assert.Null(found);
            var all = await service.GetAllAsync();
            Assert.Empty(all);
        }
    }
}