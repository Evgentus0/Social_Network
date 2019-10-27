using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SocialNetwork_DAL.Entities;
using SocialNetwork_DAL.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace SocialNetwork_DAL.EF
{
    public  class SocialNetworkInitializer : DropCreateDatabaseIfModelChanges<SocialNetworkContext>
    {
        protected override void Seed(SocialNetworkContext context)
        {
            var relationships = new Relationship[]
            {
                new Relationship{Name="Single"},
                new Relationship{Name="Dating"},
                new Relationship{Name="Married"},
                new Relationship{Name="it's complicated"}
            };
            context.Relationships.AddRange(relationships);
            context.SaveChanges();

            var countries = new Country[]
            {
                new Country{Name="Ukraine"},
                new Country{Name="Russia"},
                new Country{Name="USA"},
                new Country{Name="United Kingdom of Britain"},
                new Country{Name="Italy"}
            };
            context.Countries.AddRange(countries);
            context.SaveChanges();
            var cities = new City[]
            {
                new City{Name="Kyiv",  CountryId=1},
                new City{Name="Kharkov",  CountryId=1},
                new City{Name="Donetsk",  CountryId=1},
                new City{Name="Odessa",  CountryId=1},
                new City{Name="Moscow",  CountryId=2},
                new City{Name="Saint Petersburg",  CountryId=2},
                new City{Name="Washington",  CountryId=3},
                new City{Name="New York",  CountryId=3},
                new City{Name="London",  CountryId=4},
                new City{Name="Liverpool",  CountryId=4},
                new City{Name="Rome",  CountryId=5}
            };
            context.Cities.AddRange(cities);
            context.SaveChanges();

            var messageHeaderTypes = new MessageHeaderType[]
            {
                new MessageHeaderType{Name="Dialog"},
                new MessageHeaderType{Name="Group"},
                new MessageHeaderType{Name="SuperGroup"},
                new MessageHeaderType{Name="Channel"}
            };
            context.MessageHeaderTypes.AddRange(messageHeaderTypes);
            context.SaveChanges();

            ApplicationRoleManager roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));
            ApplicationRole[] roles = new ApplicationRole[]
            {
                new ApplicationRole{Name="User"},
                new ApplicationRole{Name="Admin"},
                new ApplicationRole{Name="Moderator"}
            };
            foreach(var r in roles)
            {
                roleManager.Create(r);
            }

            ApplicationUserManager userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            ApplicationUser user1 = new ApplicationUser
            {
                Email = "zerom2016romanenko@gmail.com",
                UserName = "zerom2016romanenko@gmail.com",
                PhoneNumber = "380990131696"
            };
            userManager.Create(user1, "lenoc25S");
            userManager.AddToRole(user1.Id, "Moderator");
            ClientProfile profile1 = new ClientProfile
            {
                Id = user1.Id,
                Name = "Eugene",
                Surname = "Romanenko",
                Gender = true,
                DateOfBirth = new DateTime(2000, 3, 16),
                PersonalInfo = "Creator of this social network",
                RelationshipId =2,
                CountryId = 1,
                CityId = 1,
                IsBlocked = false
            };
            context.ClientProfiles.Add(profile1);
            ApplicationUser user2 = new ApplicationUser
            {
                Email = "nastyusha.solod369@gmail.com",
                UserName = "nastyusha.solod369@gmail.com",
                PhoneNumber = "380956713689"
            };
            userManager.Create(user2, "lenoc25SGirl");
            userManager.AddToRole(user2.Id, "Moderator");
            ClientProfile profile2 = new ClientProfile
            {
                Id = user2.Id,
                Name = "Nastya",
                Surname = "Solod",
                Gender = false,
                DateOfBirth = new DateTime(2000, 04, 27),
                PersonalInfo = "Girlfriend of Creator",
                RelationshipId = 2,
                CountryId = 1,
                CityId = 1,
                IsBlocked = false,
                Followers=new List<ClientProfile>() { profile1}
                
            };
            context.ClientProfiles.Add(profile2);
            context.SaveChanges();

            base.Seed(context);
        }
    }
}