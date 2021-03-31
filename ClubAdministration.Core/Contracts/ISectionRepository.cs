using ClubAdministration.Core.DataTransferObjects;
using ClubAdministration.Core.Entities;
using System.Threading.Tasks;

namespace ClubAdministration.Core.Contracts
{
    public interface ISectionRepository
    {
        Task<Section[]> GetAllAsync();
        
        Task<string[]> GetSectionNamesForMemberAsync(int memberId);
        Task<SectionDto> GetWithDetailsByIdAsync(int id);
        Task<SectionDto[]> GetSectionsForMemberAsync(int memberId);
        Task<SectionDto[]> GetSectionsNotMemberOfAsync(int memberId);
    }
}
