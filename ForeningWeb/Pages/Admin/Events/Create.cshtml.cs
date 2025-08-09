using ForeningWeb.Models;
using ForeningWeb.Security;
using ForeningWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace ForeningWeb.Pages.Admin.Events
{
    public class CreateModel : PageModel
    {
        private readonly EventService _svc;
        private readonly IOptions<AdminOptions> _admin;

        public CreateModel(EventService svc, IOptions<AdminOptions> admin)
        {
            _svc = svc;
            _admin = admin;
        }

        [BindProperty]
        public Event Item { get; set; } = new();

        [FromQuery(Name = "key")]
        public string? Key { get; set; }

        private bool HasAccess() =>
            !string.IsNullOrWhiteSpace(Key) && Key == _admin.Value.Key;

        public IActionResult OnGet()
        {
            if (!HasAccess()) return Unauthorized(); // 401 hvis forkert/nul key
            Item.Dato = DateTime.Today;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!HasAccess()) return Unauthorized();
            if (!ModelState.IsValid) return Page();

            await _svc.CreateAsync(Item);
            TempData["Msg"] = "Begivenhed oprettet.";
            return Redirect($"/Events?msg=created");
        }
    }
}
