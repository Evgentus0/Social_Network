using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork_DAL.Entities
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [ForeignKey("CountryId")]
        public Country Country { get; set; }
        public int? CountryId { get; set; }
    }
}