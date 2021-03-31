using System.Threading.Tasks;
using ClubAdministration.Core.Contracts;
using ClubAdministration.Core.DataTransferObjects;
using ClubAdministration.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClubAdministration.Web.Pages.Sections
{
    public class JoinModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Member> _userManager;

        [BindProperty]
        public SectionDto Section { get; set; }

        public JoinModel(
            IUnitOfWork unitOfWork,
            UserManager<Member> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(int sectionId)
        {
            if (ModelState.IsValid)
            {
                Section = await _unitOfWork.SectionRepository.GetWithDetailsByIdAsync(sectionId);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Member member = await _userManager.GetUserAsync(HttpContext.User);
            if (member == null)
            {
                return NotFound();
            }

            await _unitOfWork.MemberSectionRepository.AddAsync(new MemberSection
            {
                SectionId = Section.Id,
                MemberId = member.Id
            });

            await _unitOfWork.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
