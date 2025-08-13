using ForeningWeb.Models;
using ForeningWeb.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ForeningWeb.Pages.Donations
{
    public class IndexModel : PageModel
    {
        private readonly DonationService _svc;
        public IndexModel(DonationService svc) => _svc = svc;

        public IList<Donation> Items { get; set; } = new List<Donation>();

        public async Task OnGetAsync() => Items = await _svc.GetAllAsync();
    }
}
