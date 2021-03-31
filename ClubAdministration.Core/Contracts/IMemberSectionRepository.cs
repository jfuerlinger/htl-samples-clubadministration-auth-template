using ClubAdministration.Core.Entities;
using System.Threading.Tasks;

namespace ClubAdministration.Core.Contracts
{
    public interface IMemberSectionRepository
    {
        Task<MemberSection> GetById(int id);
        Task<MemberSection> GetByIds(int sectionId, int memberId);

        Task AddRangeAsync(MemberSection[] memberSections);

        void Remove(MemberSection entry);
        Task AddAsync(MemberSection entry);
    }
}
