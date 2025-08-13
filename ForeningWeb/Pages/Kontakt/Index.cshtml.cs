using ForeningWeb.Models;
using ForeningWeb.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ForeningWeb.Pages.Kontakt
{
    public class IndexModel : PageModel
    {
        private readonly KontaktService _svc;
        public List<Kontakt> Items { get; set; } = new();

        public IndexModel(KontaktService svc)
        {
            _svc = svc;
        }

        public async Task OnGetAsync()
        {
            Items = await _svc.GetAllAsync();
        }
    }
}
