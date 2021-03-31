using ClubAdministration.Core.Contracts;
using ClubAdministration.Core.DataTransferObjects;
using ClubAdministration.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ClubAdministration.Persistence
{
  public class MemberRepository : IMemberRepository
  {
    private readonly ApplicationDbContext _dbContext;

    public MemberRepository(ApplicationDbContext dbContext)
    {
      _dbContext = dbContext;
    }

    public async Task<Member> GetMemberByNameAsync(string lastName, string firstName)
     => await _dbContext.Members
          .Include(m => m.MemberSections)
            .ThenInclude(ms => ms.Section)
          .FirstOrDefaultAsync(m => m.Lastname == lastName && m.Firstname == firstName);

    public async Task<Member[]> GetMembersForSectionAsync(int sectionId)
    {
      var members = (await _dbContext.Sections
          .Include(s => s.MemberSections)
          .ThenInclude(ms => ms.Member)
          .FirstOrDefaultAsync(s => s.Id == sectionId))
          ?.MemberSections
            .Select(ms => ms.Member)
            .OrderBy(m => m.Lastname)
            .ThenBy(m => m.Firstname)
            .ToArray();

      return members;
    }

    public async Task<MemberDto[]> GetMemberDtosAsync(int sectionId)
    {
      var memberDtos = (await _dbContext.MemberSections
                .Where(ms => ms.SectionId == sectionId)
                .Include(ms => ms.Member)
                  .ThenInclude(m => m.MemberSections)
                .ToArrayAsync())
                .GroupBy(ms => ms.Member)
                .Select(ms => new MemberDto
                {
                  Id = ms.Key.Id,
                  Firstname = ms.Key.Firstname,
                  Lastname = ms.Key.Lastname,
                  RegisteredSince = ms.Key.RegisteredSince,
                  CountSections = _dbContext.Members.Single(m => m.Id == ms.Key.Id).MemberSections.Count
                })
                .OrderBy(m => m.Lastname)
                .ThenBy(m => m.Firstname)
                .ToArray();

      return memberDtos;
    }

    public async Task<Member> GetByIdAsync(int id)
      => await _dbContext.Members.FindAsync(id);

    public async Task<string[]> GetMemberNamesAsync()
      => await _dbContext.Members
          .OrderBy(m => m.Lastname)
          .ThenBy(m => m.Firstname)
          .Select(m => $"{m.Lastname} {m.Firstname}")
          .ToArrayAsync();

    public bool HasDuplicate(Member member)
      => _dbContext.Members
        .Any(m => m.Id != member.Id && m.Firstname == member.Firstname && m.Lastname == member.Lastname);
  }
}