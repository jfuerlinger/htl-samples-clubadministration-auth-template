using System.Threading.Tasks;
using ClubAdministration.Core.Contracts;
using ClubAdministration.Core.DataTransferObjects;
using ClubAdministration.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClubAdministration.Web.Pages.Sections
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Member> _userManager;

        public SectionDto[] Sections { get; set; }
        public SectionDto[] SectionsNotMemberOf { get; set; }

        public IndexModel(
            IUnitOfWork unitOfWork,
            UserManager<Member> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var member = await _userManager.GetUserAsync(HttpContext.User);
            
            Sections = await _unitOfWork.SectionRepository.GetSectionsForMemberAsync(member.Id);
            SectionsNotMemberOf = await _unitOfWork.SectionRepository.GetSectionsNotMemberOfAsync(member.Id);

            return Page();
        }
    }
}
