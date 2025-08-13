using ForeningWeb.Models;
using ForeningWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ForeningWeb.Pages.Contacts
{
    public class DetailsModel : PageModel
    {
        private readonly KontaktService _svc;
        public Kontakt? Item { get; private set; }

        public DetailsModel(KontaktService svc)
        {
            _svc = svc;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Item = await _svc.FindAsync(id);
            if (Item == null) return NotFound();
            return Page();
        }
    }
}
