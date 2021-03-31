using ClubAdministration.Core.Contracts;
using ClubAdministration.Core.Entities;
using ClubAdministration.Web.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ClubAdministration.Web.Pages.Members
{
    [Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<SelectListItem> Sections { get; set; }

        /// <summary>
        /// In Combobox ausgewählte Sektion
        /// </summary>
        [BindProperty]
        [DisplayName("Section")]
        public int SelectedSectionId { get; set; }

        public MemberWithDetailsDto[] Members { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Sections = (await _unitOfWork
                .SectionRepository
                .GetAllAsync())
                .Select(section => new SelectListItem(
                    $"{section.Name}",
                    section.Id.ToString()))
                .ToList();

            if (Sections.Any())
            {
                Members = (await _unitOfWork.MemberRepository
                            .GetMembersForSectionAsync(int.Parse(Sections[0].Value)))
                            .Select(async m => new MemberWithDetailsDto
                            {
                                Id = m.Id,
                                Firstname = m.Firstname,
                                Lastname = m.Lastname,
                                RegisteredSince = m.RegisteredSince,
                                //TODO: Calculate IsAdmin-Property
                            })
                            .Select(m => m.Result)
                            .ToArray();
            }
            else
            {
                Members = Enumerable.Empty<MemberWithDetailsDto>().ToArray();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Sections = (await _unitOfWork
                .SectionRepository
                .GetAllAsync())
                .Select(section => new SelectListItem(
                    $"{section.Name}",
                    section.Id.ToString()))
                .ToList();

            Members = (await _unitOfWork.MemberRepository
                        .GetMembersForSectionAsync(SelectedSectionId))
                        .Select(async m => new MemberWithDetailsDto
                        {
                            Id = m.Id,
                            Firstname = m.Firstname,
                            Lastname = m.Lastname,
                            RegisteredSince = m.RegisteredSince,
                            //TODO: Calculate IsAdmin-Property
                        })
                        .Select(m => m.Result)
                        .ToArray();

            return Page();
        }
    }
}
