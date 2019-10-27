using System;
using System.Collections.Generic;

namespace SocialNetwork_PL.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PersonalInfo { get; set; }
        public RelationshipModel Relationship { get; set; }
        public CountryModel Country { get; set; }
        public CityModel City { get; set; }
        public ICollection<PublicationModel> Publications { get; set; }
        public ICollection<PublicationModel> LikedPublications { get; set; }
        public string PictureProfilePath { get; set; }
        public ICollection<UserModel> Followers { get; set; }
        public ICollection<UserModel> Following { get; set; }
        public bool IsBlocked { get; set; }
        public ICollection<MessageHeaderModel> MessageHeaders { get; set; }

        public string Email { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public UserModel()
        {
            Publications = new List<PublicationModel>();
            Followers = new List<UserModel>();
            Following = new List<UserModel>();
            MessageHeaders = new List<MessageHeaderModel>();
            LikedPublications = new List<PublicationModel>();
        }
    }
}