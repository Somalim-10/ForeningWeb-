using ForeningWeb.Models;
using ForeningWeb.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ForeningWeb.Pages.Donation
{
    public class IndexModel : PageModel
    {
        private readonly DonationService _svc;
        public List<Donation> Items { get; set; } = new();

        public IndexModel(DonationService svc)
        {
            _svc = svc;
        }

        public async Task OnGetAsync()
        {
            Items = await _svc.GetAllAsync();
        }
    }
}
