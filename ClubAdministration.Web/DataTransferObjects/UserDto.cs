using System.ComponentModel.DataAnnotations;

namespace ClubAdministration.Web.DataTransferObjects
{
    public class UserDto : CredentialDto
    {
        [Required(ErrorMessage = "{0} is required!")]
        [MaxLength(20, ErrorMessage = "{0} maximum length is {1}!")]
        [MinLength(2, ErrorMessage = "{0} minimum length is {1}!")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [MaxLength(20, ErrorMessage = "{0} maximum length is {1}!")]
        [MinLength(2, ErrorMessage = "{0} minimum length is {1}!")]
        public string Lastname { get; set; }
    }
}
