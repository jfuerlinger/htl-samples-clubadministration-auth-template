using ClubAdministration.Core.Contracts;
using ClubAdministration.Core.Entities;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ClubAdministration.Persistence
{
    public class MemberSectionRepository : IMemberSectionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public MemberSectionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<MemberSection> GetById(int id)
            => await _dbContext.MemberSections
                .FindAsync(id);

        public async Task<MemberSection> GetByIds(int sectionId, int memberId)
            => await _dbContext.MemberSections
                .Where(ms => ms.SectionId == sectionId && ms.MemberId == memberId)
                .FirstOrDefaultAsync();

        public async Task AddAsync(MemberSection entry)
            => await _dbContext.MemberSections.AddAsync(entry);

        public async Task AddRangeAsync(MemberSection[] memberSections)
            => await _dbContext.MemberSections.AddRangeAsync(memberSections);

        public void Remove(MemberSection entry)
            => _dbContext.MemberSections.Remove(entry);
    }
}