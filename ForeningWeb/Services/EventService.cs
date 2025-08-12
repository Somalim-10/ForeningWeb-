using ForeningWeb.Data;
using ForeningWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace ForeningWeb.Services
{
    public class EventService
    {
        private readonly ApplicationDbContext _db;

        public EventService(ApplicationDbContext db)
        {
            _db = db;
        }

        public Task<List<Event>> LatestAsync(int count = 5) =>
            _db.Events.OrderByDescending(e => e.Dato).Take(count).ToListAsync();

        public Task<Event?> FindAsync(int id) =>
            _db.Events.FindAsync(id).AsTask();

        public async Task<int> CreateAsync(Event e)
        {
            _db.Events.Add(e);
            await _db.SaveChangesAsync();
            return e.Id;
        }

        public async Task UpdateAsync(Event e)
        {
            _db.Events.Update(e);
            await _db.SaveChangesAsync();
        }

        public Task<List<Event>> GetAllAsync() =>
    _db.Events
        .OrderByDescending(e => e.Dato)
        .ToListAsync();


        public async Task DeleteAsync(int id)
        {
            var e = await _db.Events.FindAsync(id);
            if (e != null)
            {
                _db.Events.Remove(e);
                await _db.SaveChangesAsync();
            }
        }
    }
}
