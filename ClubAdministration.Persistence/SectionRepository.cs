using ClubAdministration.Core.Contracts;
using ClubAdministration.Core.DataTransferObjects;
using ClubAdministration.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ClubAdministration.Persistence
{
    public class SectionRepository : ISectionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SectionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Section[]> GetAllAsync()
          => await _dbContext.Sections
                .OrderBy(s => s.Name)
                .ToArrayAsync();

        public async Task<string[]> GetSectionNamesForMemberAsync(int memberId)
          => await _dbContext.MemberSections
                .Where(ms => ms.MemberId == memberId)
                .OrderBy(ms => ms.Section.Name)
                .Select(ms => ms.Section.Name)
                .ToArrayAsync();

        public async Task<SectionDto[]> GetSectionsForMemberAsync(int memberId)
          => await _dbContext.Sections
                .Where(s => s.MemberSections.Any(ms => ms.MemberId == memberId))
                .Select(s => new SectionDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    CountOfMembers = s.MemberSections.Count()
                })
                .ToArrayAsync();

        public async Task<SectionDto[]> GetSectionsNotMemberOfAsync(int memberId)
            => await _dbContext.Sections
                .Where(s => !s.MemberSections.Any(ms => ms.MemberId == memberId))
                .Select(s => new SectionDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    CountOfMembers = s.MemberSections.Count()
                })
                .ToArrayAsync();

        public async Task<SectionDto> GetWithDetailsByIdAsync(int id)
            => await _dbContext.Sections
                .Where(s => s.Id == id)
                .Select(s => new SectionDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    CountOfMembers = s.MemberSections.Count()
                })
                .FirstOrDefaultAsync();
    }
}