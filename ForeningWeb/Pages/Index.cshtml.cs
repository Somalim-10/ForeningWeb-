using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ForeningWeb.Models;
using ForeningWeb.Services;

namespace ForeningWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly EventService _svc;

        public List<Event> UpcomingEvents { get; private set; } = new();

        public IndexModel(ILogger<IndexModel> logger, EventService svc)
        {
            _logger = logger;
            _svc = svc;
        }

        public async Task OnGetAsync()
        {
            var items = await _svc.GetAllAsync();
            UpcomingEvents = items
                .Where(e => e.Dato >= DateTime.Today)
                .OrderBy(e => e.Dato)
                .Take(5)
                .ToList();
        }
    }
}
