using ChatAppV._0._0._2.Core.Models.Auithentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppV._0._0._2.EF.Configurations
{
    internal class ApplicationUserEntityTypeConfigurations : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.Property(U => U.UserName)
                .HasMaxLength(50);

            builder.Property(U=>U.FirstName)
                .HasMaxLength(50);

            builder.Property(U => U.LastName)
                .HasMaxLength(50);

            builder.HasMany(U => U.User1Connection)
                .WithOne(C => C.User1)
                .HasPrincipalKey(U => U.Id)
                .HasForeignKey(C => C.User1ID);

            builder.HasMany(U => U.User2Connection)
              .WithOne(C => C.User2)
              .HasPrincipalKey(U => U.Id)
              .HasForeignKey(C => C.User2ID);

            builder.HasMany(U => U.Messages)
                .WithOne(M => M.Sender)
                .HasPrincipalKey(U => U.Id)
                .HasForeignKey(M => M.SenderId);

            builder.HasMany(U=>U.SenderFriendRequists)
                .WithOne(R=>R.Sender)
                .HasPrincipalKey(U=>U.Id)
                .HasForeignKey(R=>R.SenderId);

            builder.HasMany(U => U.ResiverFriendRequists)
                .WithOne(R => R.Resiver)
                .HasPrincipalKey(U => U.Id)
                .HasForeignKey(R => R.ResiverId);
        }
    }
}
