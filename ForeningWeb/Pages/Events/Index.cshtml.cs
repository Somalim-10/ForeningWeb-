using ForeningWeb.Models;
using ForeningWeb.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ForeningWeb.Pages.Events
{
    public class IndexModel : PageModel
    {
        private readonly EventService _svc;
        public List<Event> Items { get; private set; } = new();
        public IndexModel(EventService svc)
        {
            _svc = svc;
        }
        public async Task OnGetAsync()
        {
            Items = await _svc.LatestAsync(50);
        }
    }
}
