using ForeningWeb.Models;
using ForeningWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ForeningWeb.Pages.Admin.Events
{
    public class CreateModel : PageModel
    {
        private readonly EventService _svc;

        public CreateModel(EventService svc)
        {
            _svc = svc;
        }

        [BindProperty]
        public Event Item { get; set; } = new();

        public IActionResult OnGet()
        {
            // Sæt standarddato til i dag
            Item.Dato = DateTime.Today;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            // 1. Valider billedet
            var ok = await _svc.ValidateImageUrlAsync(Item.ImageUrl);
            if (!ok)
            {
                ModelState.AddModelError("Item.ImageUrl", "Billedet kunne ikke findes online.");
                return Page();
            }

            // 2. Gem event
            await _svc.CreateAsync(Item);

            // 3. Redirect med besked
            TempData["Msg"] = "Begivenhed oprettet.";
            return RedirectToPage("/Events/Index", new { msg = "created" });
        }


    }
}

