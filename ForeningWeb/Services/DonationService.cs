using ForeningWeb.Data;
using ForeningWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace ForeningWeb.Services
{
    public class DonationService
    {
        private readonly ApplicationDbContext _db;

        public DonationService(ApplicationDbContext db)
        {
            _db = db;
        }

        public Task<Donation?> FindAsync(int id) =>
            _db.Donationer.FindAsync(id).AsTask();

        public async Task<int> CreateAsync(Donation d)
        {
            _db.Donationer.Add(d);
            await _db.SaveChangesAsync();
            return d.Id;
        }

        public async Task UpdateAsync(Donation d)
        {
            _db.Donationer.Update(d);
            await _db.SaveChangesAsync();
        }

        public Task<List<Donation>> GetAllAsync() =>
            _db.Donationer.ToListAsync();

        public async Task DeleteAsync(int id)
        {
            var d = await _db.Donationer.FindAsync(id);
            if (d != null)
            {
                _db.Donationer.Remove(d);
                await _db.SaveChangesAsync();
            }
        }
    }
}
