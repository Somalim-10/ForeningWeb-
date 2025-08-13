using ForeningWeb.Models;
using ForeningWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ForeningWeb.Pages.Donations
{
    public class CreateModel : PageModel
    {
        private readonly DonationService _svc;
        public CreateModel(DonationService svc)
        {
            _svc = svc;
        }

        [BindProperty]
        public Donation Item { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            await _svc.CreateAsync(Item);
            return RedirectToPage("Index");
        }
    }
}
