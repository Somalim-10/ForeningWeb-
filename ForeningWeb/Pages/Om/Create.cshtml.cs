using ForeningWeb.Models;
using ForeningWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ForeningWeb.Pages.Om
{
    public class CreateModel : PageModel
    {
        private readonly OmService _svc;
        public CreateModel(OmService svc)
        {
            _svc = svc;
        }

        [BindProperty]
        public Om Item { get; set; } = new();

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
