using ForeningWeb.Services;
using ForeningWeb.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace ForeningWeb.Pages.Admin.Events
{
    public class DeleteModel : PageModel
    {
        private readonly EventService _svc;
        private readonly IOptions<AdminOptions> _admin;

        public DeleteModel(EventService svc, IOptions<AdminOptions> admin)
        {
            _svc = svc;
            _admin = admin;
        }

        public int Id { get; set; }

        [FromQuery(Name = "key")]
        public string? Key { get; set; }

        private bool HasAccess() => !string.IsNullOrWhiteSpace(Key) && Key == _admin.Value.Key;

        public IActionResult OnGet(int id)
        {
            if (!HasAccess()) return Unauthorized();
            Id = id;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!HasAccess()) return Unauthorized();
            await _svc.DeleteAsync(id);
            TempData["Msg"] = "Begivenhed slettet.";
            return Redirect("/Events?msg=deleted");
        }
    }
}
