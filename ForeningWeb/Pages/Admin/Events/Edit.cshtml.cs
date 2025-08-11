using ForeningWeb.Models;
using ForeningWeb.Security;
using ForeningWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace ForeningWeb.Pages.Admin.Events
{
    public class EditModel : PageModel
    {
        private readonly EventService _svc;
        private readonly IOptions<AdminOptions> _admin;

        public EditModel(EventService svc, IOptions<AdminOptions> admin)
        {
            _svc = svc;
            _admin = admin;
        }

        [BindProperty]

        public Event Item { get; set; } = new();

        [FromQuery(Name = "key")]
        public string? Key { get; set; }

        private bool HasAccess() => !string.IsNullOrWhiteSpace(Key) && Key == _admin.Value.Key;
        public async Task<IActionResult> OnGetAsync(int id)
        {
            if (!HasAccess()) return Unauthorized();
            var found = await _svc.FindAsync(id);
            if (found == null) return NotFound();
            Item = found;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!HasAccess()) return Unauthorized();
            if (!ModelState.IsValid) return Page();

            await _svc.UpdateAsync(Item);
            TempData["Msg"] = "Begivenhed opdateret.";
            return Redirect($"/Events/Details/{Item.Id}");
        }
    }
}

