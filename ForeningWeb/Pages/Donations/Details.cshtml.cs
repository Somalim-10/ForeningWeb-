using ForeningWeb.Models;
using ForeningWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ForeningWeb.Pages.Donations
{
    public class DetailsModel : PageModel
    {
        private readonly DonationService _svc;
        public Donation? Item { get; private set; }

        public DetailsModel(DonationService svc)
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
