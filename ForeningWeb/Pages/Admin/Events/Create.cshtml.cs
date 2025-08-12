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

            await _svc.CreateAsync(Item);
            TempData["Msg"] = "Begivenhed oprettet.";
            return RedirectToPage("/Events/Index", new { msg = "created" });
        }
    }
}
