using ForeningWeb.Models;
using ForeningWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ForeningWeb.Pages.Om
{
    public class EditModel : PageModel
    {
        private readonly OmService _svc;
        public EditModel(OmService svc)
        {
            _svc = svc;
        }

        [BindProperty]
        public Om Item { get; set; } = new();

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
            await _svc.UpdateAsync(Item);
            return RedirectToPage("Details", new { id = Item.Id });
        }
    }
}
