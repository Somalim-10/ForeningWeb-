using ForeningWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ForeningWeb.Pages.Admin.Events
{
    public class DeleteModel : PageModel
    {
        private readonly EventService _svc;

        public DeleteModel(EventService svc)
        {
            _svc = svc;
        }

        public int Id { get; set; }

        public IActionResult OnGet(int id)
        {
            Id = id;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            await _svc.DeleteAsync(id);
            TempData["Msg"] = "Begivenhed slettet.";
            return RedirectToPage("/Events/Index", new { msg = "deleted" });
        }
    }
}
