using ForeningWeb.Data;
using ForeningWeb.Models;
using ForeningWeb.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace ForeningWeb.MSTests.Services
{
    [TestClass]
    public class EventServiceTests
    {
        private static EventService CreateService(out ApplicationDbContext context)
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            context = new ApplicationDbContext(options);
            // Use null for IHttpClientFactory and ILogger<EventService> for basic tests
            return new EventService(context, new DummyHttpClientFactory(), new DummyLogger<EventService>());
        }

        [TestMethod]
        public async Task CreateAsync_Adds_Event()
        {
            var service = CreateService(out var db);
            var evt = new Event { Titel = "test", Dato = DateTime.Today };

            var id = await service.CreateAsync(evt);

            Assert.IsTrue(id > 0);
            var fromDb = await db.Events.FindAsync(id);
            Assert.IsNotNull(fromDb);
        }

        [TestMethod]
        public async Task GetAllAsync_Returns_Events_In_Descending_Order()
        {
            var service = CreateService(out var db);
            await service.CreateAsync(new Event { Titel = "first", Dato = new DateTime(2023, 1, 1) });
            await service.CreateAsync(new Event { Titel = "second", Dato = new DateTime(2024, 1, 1) });

            var events = await service.GetAllAsync();

            Assert.AreEqual(2, events.Count);
            Assert.AreEqual("second", events[0].Titel);
            Assert.AreEqual("first", events[1].Titel);
        }

        [TestMethod]
        public async Task FindAsync_Returns_Event_By_Id()
        {
            var service = CreateService(out var db);
            await service.CreateAsync(new Event { Titel = "find", Dato = DateTime.Today });
            var id = await service.CreateAsync(new Event { Titel = "target", Dato = DateTime.Today });

            var found = await service.FindAsync(id);

            Assert.IsNotNull(found);
            Assert.AreEqual("target", found!.Titel);
        }

        [TestMethod]
        public async Task UpdateAsync_Updates_Event()
        {
            var service = CreateService(out var db);
            var evt = new Event { Titel = "old", Dato = DateTime.Today };
            await service.CreateAsync(evt);

            evt.Titel = "new";
            await service.UpdateAsync(evt);

            var updated = await service.FindAsync(evt.Id);
            Assert.AreEqual("new", updated!.Titel);
        }

        [TestMethod]
        public async Task DeleteAsync_Removes_Event()
        {
            var service = CreateService(out var db);
            var evt = new Event { Titel = "delete", Dato = DateTime.Today };
            await service.CreateAsync(evt);

            await service.DeleteAsync(evt.Id);

            var deleted = await service.FindAsync(evt.Id);
            Assert.IsNull(deleted);
        }
    }

    // Dummy implementations for dependencies
    public class DummyHttpClientFactory : IHttpClientFactory
    {
        public System.Net.Http.HttpClient CreateClient(string name = null) => new System.Net.Http.HttpClient();
    }
    public class DummyLogger<T> : Microsoft.Extensions.Logging.ILogger<T>
    {
        public IDisposable BeginScope<TState>(TState state) => null;
        public bool IsEnabled(Microsoft.Extensions.Logging.LogLevel logLevel) => false;
        public void Log<TState>(Microsoft.Extensions.Logging.LogLevel logLevel, Microsoft.Extensions.Logging.EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter) { }
    }
}
