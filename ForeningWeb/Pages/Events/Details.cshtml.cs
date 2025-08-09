using ForeningWeb.Models;
using ForeningWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ForeningWeb.Pages.Events
{
    public class DetailsModel : PageModel
    {
        private readonly EventService _svc;
        public Event? Item { get; private set; }

        public DetailsModel(EventService svc)
        {
            _svc = svc;
        }

        public async Task<IActionResult> OnGet(int id)
        {
            Item = await _svc.FindAsync(id);
            if (Item == null) return NotFound();
            return Page();
        }
    }
}
