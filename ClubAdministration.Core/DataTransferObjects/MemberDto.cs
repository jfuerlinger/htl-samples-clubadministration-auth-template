
using System;
using System.ComponentModel;

namespace ClubAdministration.Core.DataTransferObjects
{
    public class MemberDto
    {
        public int Id { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }

        public DateTime RegisteredSince { get; set; }

        [DisplayName("Sections")]
        public int CountSections { get; set; }

        public override string ToString() => $"Id: {Id}; Lastname: {Lastname}; Firstname: {Firstname}; CountSections: {CountSections}";

    }
}
