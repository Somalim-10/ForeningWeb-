using ForeningWeb.Data;
using ForeningWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace ForeningWeb.Services
{
    public class OmService
    {
        private readonly ApplicationDbContext _db;

        public OmService(ApplicationDbContext db)
        {
            _db = db;
        }

        public Task<Om?> FindAsync(int id) =>
            _db.Om.FindAsync(id).AsTask();

        public async Task<int> CreateAsync(Om o)
        {
            _db.Om.Add(o);
            await _db.SaveChangesAsync();
            return o.Id;
        }

        public async Task UpdateAsync(Om o)
        {
            _db.Om.Update(o);
            await _db.SaveChangesAsync();
        }

        public Task<List<Om>> GetAllAsync() =>
            _db.Om.ToListAsync();

        public async Task DeleteAsync(int id)
        {
            var o = await _db.Om.FindAsync(id);
            if (o != null)
            {
                _db.Om.Remove(o);
                await _db.SaveChangesAsync();
            }
        }
    }
}
