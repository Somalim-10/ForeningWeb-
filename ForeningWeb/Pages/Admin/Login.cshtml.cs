using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using ForeningWeb.Security;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace ForeningWeb.Pages.Admin
{
    public class LoginModel : PageModel
    {
        private readonly IOptions<AdminOptions> _admin;

        public LoginModel(IOptions<AdminOptions> admin)
        {
            _admin = admin;
        }

        [BindProperty, Required(ErrorMessage = "Indtast adgangsnøglen.")]
        public string Key { get; set; } = "";

        public string? Error { get; set; }

        public void OnGet(string? returnUrl = null)
        {
            // returnUrl bruges hvis man blev sendt hertil pga. adgangskrav
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            if (!ModelState.IsValid)
            {
                Error = "Udfyld feltet.";
                return Page();
            }

            if (Key != _admin.Value.Key)
            {
                Error = "Forkert nøgle.";
                return Page();
            }

            // Opret claims til denne bruger
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Admin"),
                new Claim("role", "admin")
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            // Log brugeren ind med cookie
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            // Send tilbage til den side man kom fra, eller admin-startside
            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return Redirect("/Events"); // evt. /Admin/Events
        }
    }
}
