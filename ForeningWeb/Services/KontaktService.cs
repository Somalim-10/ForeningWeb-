using ForeningWeb.Data;
using ForeningWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace ForeningWeb.Services
{
    public class KontaktService
    {
        private readonly ApplicationDbContext _db;

        public KontaktService(ApplicationDbContext db)
        {
            _db = db;
        }

        public Task<Kontakt?> FindAsync(int id) =>
            _db.Kontakter.FindAsync(id).AsTask();

        public async Task<int> CreateAsync(Kontakt k)
        {
            _db.Kontakter.Add(k);
            await _db.SaveChangesAsync();
            return k.Id;
        }

        public async Task UpdateAsync(Kontakt k)
        {
            _db.Kontakter.Update(k);
            await _db.SaveChangesAsync();
        }

        public Task<List<Kontakt>> GetAllAsync() =>
            _db.Kontakter.ToListAsync();

        public async Task DeleteAsync(int id)
        {
            var k = await _db.Kontakter.FindAsync(id);
            if (k != null)
            {
                _db.Kontakter.Remove(k);
                await _db.SaveChangesAsync();
            }
        }
    }
}
