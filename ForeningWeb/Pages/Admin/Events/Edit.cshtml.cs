using ForeningWeb.Models;
using ForeningWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ForeningWeb.Pages.Admin.Events
{
    public class EditModel : PageModel
    {
        private readonly EventService _svc;

        public EditModel(EventService svc)
        {
            _svc = svc;
        }

        [BindProperty]
        public Event Item { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var found = await _svc.FindAsync(id);
            if (found == null) return NotFound();

            Item = found;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var ok = await _svc.ValidateImageUrlAsync(Item.ImageUrl);
            if (!ok)
            {
                ModelState.AddModelError("Item.ImageUrl", "Billedet kunne ikke findes online.");
                return Page();
            }

            await _svc.UpdateAsync(Item);
            TempData["Msg"] = "Begivenhed opdateret.";
            return RedirectToPage("/Events/Details", new { id = Item.Id });
        }
    }
}
