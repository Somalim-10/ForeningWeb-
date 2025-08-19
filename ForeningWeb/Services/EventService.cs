using ForeningWeb.Data;
using ForeningWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace ForeningWeb.Services
{
    public class EventService
    {
        private readonly ApplicationDbContext _db;
        private readonly IHttpClientFactory _http;
        private readonly ILogger<EventService> _logger;

        public EventService(ApplicationDbContext db, IHttpClientFactory http, ILogger<EventService> logger)
        {
            _db = db;
            _http = http;
            _logger = logger;
        }

        public Task<List<Event>> LatestAsync(int count = 5) =>
            _db.Events.OrderByDescending(e => e.Dato).Take(count).ToListAsync();

        public Task<Event?> FindAsync(int id) =>
            _db.Events.FindAsync(id).AsTask();

        public async Task<int> CreateAsync(Event e)
        {
            _db.Events.Add(e);
            await _db.SaveChangesAsync();
            _logger.LogInformation("Ny event oprettet: Id {EventId}, Titel {Titel}", e.Id, e.Titel);
            return e.Id;
        }

        public async Task UpdateAsync(Event e)
        {
            _db.Events.Update(e);
            await _db.SaveChangesAsync();
            _logger.LogInformation("Event opdateret: Id {EventId}", e.Id);
        }

        public Task<List<Event>> GetAllAsync() =>
            _db.Events.OrderByDescending(e => e.Dato).ToListAsync();

        public async Task DeleteAsync(int id)
        {
            var e = await _db.Events.FindAsync(id);
            if (e != null)
            {
                _db.Events.Remove(e);
                await _db.SaveChangesAsync();
                _logger.LogWarning("Event slettet: Id {EventId}, Titel {Titel}", e.Id, e.Titel);
            }
        }

        // Valider at billed-URL peger på et rigtigt billede
        public async Task<bool> ValidateImageUrlAsync(string? url, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(url)) return true;

            if (!Uri.TryCreate(url, UriKind.Absolute, out var uri) ||
                (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps))
                return false;

            var client = _http.CreateClient();
            client.Timeout = TimeSpan.FromSeconds(10);

            try
            {
                // Lav HEAD request
                using var headReq = new HttpRequestMessage(HttpMethod.Head, uri);
                headReq.Headers.TryAddWithoutValidation("User-Agent",
                    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126 Safari/537.36");

                using var headResp = await client.SendAsync(headReq, HttpCompletionOption.ResponseHeadersRead, ct);

                if (headResp.IsSuccessStatusCode &&
                    headResp.Content.Headers.ContentType?.MediaType?.StartsWith("image") == true)
                    return true;

                // Hvis HEAD fejler → prøv GET (kun headers/et par bytes)
                using var getReq = new HttpRequestMessage(HttpMethod.Get, uri);
                getReq.Headers.TryAddWithoutValidation("Range", "bytes=0-0");

                using var getResp = await client.SendAsync(getReq, HttpCompletionOption.ResponseHeadersRead, ct);

                if (getResp.IsSuccessStatusCode &&
                    (getResp.Content.Headers.ContentType?.MediaType?.StartsWith("image") == true ||
                     uri.AbsolutePath.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) ||
                     uri.AbsolutePath.EndsWith(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                     uri.AbsolutePath.EndsWith(".png", StringComparison.OrdinalIgnoreCase) ||
                     uri.AbsolutePath.EndsWith(".gif", StringComparison.OrdinalIgnoreCase) ||
                     uri.AbsolutePath.EndsWith(".webp", StringComparison.OrdinalIgnoreCase)))
                {
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

    }
}
