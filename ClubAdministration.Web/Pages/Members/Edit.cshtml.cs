using ClubAdministration.Core.Entities;
using ClubAdministration.Web.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace ClubAdministration.Web.Pages.Members
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly UserManager<Member> _userManager;

        public EditModel(UserManager<Member> userManager)
        {
            _userManager = userManager;
        }

        [BindProperty]
        public MemberWithDetailsDto Member { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            //TODO: Load Data
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //TODO: Save Changes

            return RedirectToPage("./Index");
        }
    }
}
