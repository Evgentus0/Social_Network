using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialNetwork_PL.Models
{
    public class CityModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CountryModel Country { get; set; }
    }
}