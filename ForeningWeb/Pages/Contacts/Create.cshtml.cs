using ForeningWeb.Models;
using ForeningWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ForeningWeb.Pages.Contacts
{
    public class CreateModel : PageModel
    {
        private readonly KontaktService _svc;
        public CreateModel(KontaktService svc)
        {
            _svc = svc;
        }

        [BindProperty]
        public Kontakt Item { get; set; } = new();

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
