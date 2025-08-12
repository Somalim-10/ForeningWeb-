using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace ForeningWeb.Pages.Admin
{
    [AllowAnonymous] // Skal kunne tilgås uden login
    public class LogoutModel : PageModel
    {
        public async Task<IActionResult> OnGet()
        {
            // Fjern auth-cookie
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Tilbage til forsiden
            return RedirectToPage("/Index");
        }
    }
}
