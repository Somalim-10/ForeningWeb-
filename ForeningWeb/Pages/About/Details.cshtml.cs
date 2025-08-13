using ForeningWeb.Models;
using ForeningWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ForeningWeb.Pages.About
{
    public class DetailsModel : PageModel
    {
        private readonly OmService _svc;
        public Om? Item { get; private set; }

        public DetailsModel(OmService svc)
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
