using ForeningWeb.Models;
using ForeningWeb.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ForeningWeb.Pages.About
{
    public class IndexModel : PageModel
    {
        private readonly OmService _svc;
        public List<Om> Items { get; set; } = new();

        public IndexModel(OmService svc)
        {
            _svc = svc;
        }

        public async Task OnGetAsync()
        {
            Items = await _svc.GetAllAsync();
        }
    }
}
