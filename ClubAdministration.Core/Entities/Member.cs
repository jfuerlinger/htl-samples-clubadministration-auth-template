using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClubAdministration.Core.Entities
{
    public class Member : EntityObject
    {
        [Required(ErrorMessage = "{0} is required!")]
        [MaxLength(20, ErrorMessage = "{0} maximum length is {1}!")]
        [MinLength(2, ErrorMessage = "{0} minimum length is {1}!")]
        public string Firstname { get; set; }

        [Required(ErrorMessage = "{0} is required!")]
        [MaxLength(20, ErrorMessage = "{0} maximum length is {1}!")]
        [MinLength(2, ErrorMessage = "{0} minimum length is {1}!")]
        public string Lastname { get; set; }

        public string FullName => $"{Firstname} {Lastname}";

        public DateTime RegisteredSince { get; set; }

        public ICollection<MemberSection> MemberSections { get; set; }

        public override string ToString() => $"Id: {Id}; Lastname: {Lastname}; Firstname: {Firstname}; MemberSections: {MemberSections?.Count}";

        public Member()
        {
            RegisteredSince = DateTime.Now;

            //TODO: Initialize Identity Properties
        }
    }
}
