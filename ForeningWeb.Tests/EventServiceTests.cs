using System;
using System.Threading.Tasks;
using ForeningWeb.Data;
using ForeningWeb.Models;
using ForeningWeb.Services;
using Microsoft.EntityFrameworkCore;

using Xunit;

namespace ForeningWeb.Tests
{
    public class EventServiceTests
    {
        private static EventService CreateService(out ApplicationDbContext context)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            context = new ApplicationDbContext(options);
            return new EventService(context);
        }

        [Fact]
        public async Task CreateAsync_Adds_Event()
        {
            var service = CreateService(out var db);
            var evt = new Event { Titel = "test", Dato = DateTime.Today };

            var id = await service.CreateAsync(evt);

            Assert.True(id > 0);
            var fromDb = await db.Events.FindAsync(id);
            Assert.NotNull(fromDb);
        }

        [Fact]
        public async Task GetAllAsync_Returns_Events_In_Descending_Order()
        {
            var service = CreateService(out var db);
            await service.CreateAsync(new Event { Titel = "first", Dato = new DateTime(2023, 1, 1) });
            await service.CreateAsync(new Event { Titel = "second", Dato = new DateTime(2024, 1, 1) });

            var events = await service.GetAllAsync();

            Assert.Equal(2, events.Count);
            Assert.Equal("second", events[0].Titel);
            Assert.Equal("first", events[1].Titel);
        }

        [Fact]
        public async Task FindAsync_Returns_Event_By_Id()
        {
            var service = CreateService(out var db);
            await service.CreateAsync(new Event { Titel = "find", Dato = DateTime.Today });
            var id = await service.CreateAsync(new Event { Titel = "target", Dato = DateTime.Today });

            var found = await service.FindAsync(id);

            Assert.NotNull(found);
            Assert.Equal("target", found!.Titel);
        }

        [Fact]
        public async Task UpdateAsync_Updates_Event()
        {
            var service = CreateService(out var db);
            var evt = new Event { Titel = "old", Dato = DateTime.Today };
            await service.CreateAsync(evt);

            evt.Titel = "new";
            await service.UpdateAsync(evt);

            var updated = await service.FindAsync(evt.Id);
            Assert.Equal("new", updated!.Titel);
        }

        [Fact]
        public async Task DeleteAsync_Removes_Event()
        {
            var service = CreateService(out var db);
            var evt = new Event { Titel = "delete", Dato = DateTime.Today };
            await service.CreateAsync(evt);

            await service.DeleteAsync(evt.Id);

            var deleted = await service.FindAsync(evt.Id);
            Assert.Null(deleted);
        }
    }
}