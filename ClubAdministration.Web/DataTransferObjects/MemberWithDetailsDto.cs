using System;

namespace ClubAdministration.Web.DataTransferObjects
{
    public class MemberWithDetailsDto : UserDto
    {
        public int Id { get; set; }
        
        public DateTime RegisteredSince { get; set; }
        public bool IsAdmin { get; set; }
    }
}
