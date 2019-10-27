using Microsoft.AspNet.Identity.EntityFramework;
using SocialNetwork_DAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_DAL.EF
{
    public class SocialNetworkContext:IdentityDbContext<ApplicationUser>
    {
        public SocialNetworkContext(string connectionString):base(connectionString)
        {
        }

        static SocialNetworkContext()
        {
            Database.SetInitializer<SocialNetworkContext>(new SocialNetworkInitializer());
        }

        public DbSet<ClientProfile> ClientProfiles { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageHeader> MessageHeaders { get; set; }
        public DbSet<Publication> Publications { get; set; }
        public DbSet<Relationship> Relationships { get; set; }
        public DbSet<MessageHeaderType> MessageHeaderTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientProfile>().HasMany(x => x.LikedPublications).WithMany(x => x.UsersWhoLike);
            modelBuilder.Entity<ClientProfile>().HasMany(x => x.Publications).WithRequired(x => x.Author).HasForeignKey(k => k.AuthorId).WillCascadeOnDelete(false);
            modelBuilder.Entity<ClientProfile>().HasMany(x => x.MessageHeaders).WithMany(x => x.Users);
            modelBuilder.Entity<Message>().HasMany(x => x.DeleteFor).WithMany().Map(t => t.ToTable("MessageClientProfileDeleteFor"));
            modelBuilder.Entity<Message>().HasRequired(x => x.Sender).WithMany().WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }
    }
}
