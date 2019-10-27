using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork_DAL.Entities
{
    public class ClientProfile
    {
        [Key]
        [ForeignKey("ApplicationUser")]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PersonalInfo { get; set; }
        [ForeignKey("RelationshipId")]
        public Relationship Relationship { get; set; }
        public int? RelationshipId { get; set; }
        [ForeignKey("CountryId")]
        public Country Country { get; set; }
        public int? CountryId { get; set; }
        [ForeignKey("CityId")]
        public City City { get; set; }
        public int? CityId { get; set; }
        public ICollection<Publication> Publications { get; set; }
        public ICollection<Publication> LikedPublications { get; set; }
        public string PictureProfilePath { get; set; }
        public ICollection<ClientProfile> Followers { get; set; }
        public ICollection<ClientProfile> Following { get; set; }
        public bool IsBlocked { get; set; }
        public ICollection<MessageHeader> MessageHeaders { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public ClientProfile()
        {
            Publications = new List<Publication>();
            Followers = new List<ClientProfile>();
            Following = new List<ClientProfile>();
            MessageHeaders = new List<MessageHeader>();
            LikedPublications = new List<Publication>();
        }


    }
}