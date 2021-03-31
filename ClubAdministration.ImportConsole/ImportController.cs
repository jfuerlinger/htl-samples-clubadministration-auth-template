using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ClubAdministration.Core.Entities;
using Utils;

namespace ClubAdministration.ImportConsole
{
    public class ImportController
    {
        const string _fileName = "members.csv";

        public static async Task<MemberSection[]> ReadFromCsvAsync()
        {
            string[][] matrix = await MyFile.ReadStringMatrixFromCsvAsync(_fileName, false);
            List<Member> members = matrix
                .GroupBy(line => line[0] + "~" + line[1])
                .Select(grp => new Member { Lastname = grp.Key.Split('~')[0], Firstname = grp.Key.Split('~')[1] })
                .Select(m =>
                {
                    //TODO: Calculate Username 
                    return m;
                })
                .ToList();
            List<Section> sections = matrix
                .GroupBy(line => line[2])
                .Select(grp => new Section { Name = grp.Key })
                .ToList();
            var memberSections = matrix.Select(line => new MemberSection
            {
                Member = members.Single(m => m.Lastname == line[0] && m.Firstname == line[1]),
                Section = sections.Single(s => s.Name == line[2])
            }).ToArray();
            return memberSections;
        }

    }
}
