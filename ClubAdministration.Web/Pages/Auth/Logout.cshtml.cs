using System.Threading.Tasks;
using ClubAdministration.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClubAdministration.Web.Pages.Auth
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<Member> _signInManager;

        public LogoutModel(SignInManager<Member> signInManager)
        {
            _signInManager = signInManager;
        }

        public async Task<ActionResult> OnPostAsync()
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage("/Index");
        }
    }
}
