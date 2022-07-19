using ChatAppV._0._0._2.Core.Models.Auithentication;
using ChatAppV._0._0._2.Core.Models.ChatModels;
using ChatAppV._0._0._2.EF.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppV._0._0._2.EF
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<ApplicationUser>().ToTable("Users", "Security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "Security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "Security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "Security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "Security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "Security");


            builder.Entity<IdentityUserLogin<string>>().HasKey(x => new { x.ProviderKey, x.LoginProvider });
            builder.Entity<IdentityUserRole<string>>().HasKey(x => new { x.RoleId, x.UserId });
            builder.Entity<IdentityUserToken<string>>().HasKey(x => new { x.LoginProvider, x.Name, x.UserId });

            new ApplicationUserEntityTypeConfigurations().Configure(builder.Entity<ApplicationUser>());
            new ConnectionsEntityTypeConfigurations().Configure(builder.Entity<Connection>());
            new MessageEntityTypeConfigurations().Configure(builder.Entity<Message>());
            new FriendRequistEntityTypeConfiguration().Configure(builder.Entity<FriendRequist>());

        }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<FriendRequist> FriendRequists { get; set; }

    }
}
