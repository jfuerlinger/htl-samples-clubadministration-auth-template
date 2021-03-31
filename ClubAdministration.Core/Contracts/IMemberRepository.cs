using ClubAdministration.Core.DataTransferObjects;
using ClubAdministration.Core.Entities;
using System.Threading.Tasks;

namespace ClubAdministration.Core.Contracts
{
  public interface IMemberRepository
  {
    Task<Member> GetMemberByNameAsync(string lastName, string firstName);
    Task<Member[]> GetMembersForSectionAsync(int sectionId);
    Task<MemberDto[]> GetMemberDtosAsync(int id);
    Task<Member> GetByIdAsync(int id);
    Task<string[]> GetMemberNamesAsync();
    bool HasDuplicate(Member member);
  }
}
