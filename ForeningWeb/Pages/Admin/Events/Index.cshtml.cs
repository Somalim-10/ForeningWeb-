using ForeningWeb.Models;
using ForeningWeb.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ForeningWeb.Pages.Admin.Events
{
    public class IndexModel : PageModel
    {
        private readonly EventService _svc;

        public IndexModel(EventService svc)
        {
            _svc = svc;
        }

        public List<Event> Items { get; set; } = new();

        public async Task OnGetAsync()
        {
            Items = await _svc.GetAllAsync();
        }
    }
}
