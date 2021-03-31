using System.ComponentModel.DataAnnotations;

namespace ClubAdministration.Web.DataTransferObjects
{
    public class CredentialDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
