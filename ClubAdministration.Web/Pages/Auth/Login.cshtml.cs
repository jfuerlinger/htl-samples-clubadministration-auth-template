using System.Threading.Tasks;
using ClubAdministration.Core.Entities;
using ClubAdministration.Web.DataTransferObjects;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClubAdministration.Web.Pages.Auth
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public CredentialDto Credentials { get; set; }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                //TODO: Perform Login
                
                string redirectTo =
                    !string.IsNullOrEmpty(Request.Query["ReturnUrl"])
                        ? Request.Query["ReturnUrl"]
                        : "/Index";

                return LocalRedirect(redirectTo);
            }

            return Page();
        }
    }
}
