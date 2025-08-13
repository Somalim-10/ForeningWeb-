using ForeningWeb.Models;
using ForeningWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ForeningWeb.Pages.Om
{
    public class DeleteModel : PageModel
    {
        private readonly OmService _svc;
        public DeleteModel(OmService svc)
        {
            _svc = svc;
        }

        [BindProperty]
        public Om? Item { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Item = await _svc.FindAsync(id);
            if (Item == null) return NotFound();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _svc.DeleteAsync(id);
            return RedirectToPage("Index");
        }
    }
}
