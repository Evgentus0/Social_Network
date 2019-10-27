using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SocialNetwork_PL.Models
{

    public class PublicationUserLikeModel
    {
        public string Email { get; set; }
        public int PublicationId { get; set; }
    }

    public class ChangeHeaderModel
    {
        public int Id { get; set; }
        public string Header { get; set; }
    }

    public class AddMemberModel
    {
        public string UserId { get; set; }
        public int MessageHeaderId { get; set; }
    }
    public class RegisterModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public bool Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PersonalInfo { get; set; }
        public int RelationShipId { get; set; }
        public int CountryId { get; set; }
        public int CityId { get; set; }
        public string PictureProfilePath { get; set; }

        [Required]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}