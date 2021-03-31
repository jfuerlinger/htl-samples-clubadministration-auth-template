using System.ComponentModel;

namespace ClubAdministration.Core.DataTransferObjects
{
    public class SectionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        [DisplayName("Members")]
        public int CountOfMembers { get; set; }

        public override string ToString() => $"Name: {Name}; CountOfMembers: {CountOfMembers};";
    }
}
