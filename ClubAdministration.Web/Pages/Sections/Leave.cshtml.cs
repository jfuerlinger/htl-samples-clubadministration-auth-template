using System.Threading.Tasks;
using ClubAdministration.Core.Contracts;
using ClubAdministration.Core.DataTransferObjects;
using ClubAdministration.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClubAdministration.Web.Pages.Sections
{
    public class LeaveModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<Member> _userManager;

        [BindProperty]
        public int MemberSectionId { get; set; }

        public SectionDto Section { get; set; }

        public LeaveModel(
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
                Member member = await _userManager.GetUserAsync(HttpContext.User);
                if (member == null)
                {
                    return NotFound();
                }

                MemberSection ms = await _unitOfWork.MemberSectionRepository.GetByIds(sectionId, member.Id);
                if (ms == null)
                {
                    return NotFound();
                }

                MemberSectionId = ms.Id;
                Section = await _unitOfWork.SectionRepository.GetWithDetailsByIdAsync(sectionId);
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            MemberSection ms = await _unitOfWork.MemberSectionRepository.GetById(MemberSectionId);
            _unitOfWork.MemberSectionRepository.Remove(ms);
            await _unitOfWork.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
