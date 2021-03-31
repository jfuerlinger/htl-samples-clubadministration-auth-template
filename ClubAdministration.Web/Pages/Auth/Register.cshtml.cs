using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClubAdministration.Core.Entities;
using ClubAdministration.Web.DataTransferObjects;

namespace ClubAdministration.Web.Pages.Auth
{
    public class RegisterModel : PageModel
    {

        [BindProperty]
        public UserDto AuthUser { get; set; }

        public ActionResult OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                //TODO: Implement Register logic
            }

            return Page();
        }
    }
}
