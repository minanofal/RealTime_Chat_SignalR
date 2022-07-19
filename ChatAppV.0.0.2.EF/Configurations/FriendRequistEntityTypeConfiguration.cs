using ChatAppV._0._0._2.Core.Models.ChatModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppV._0._0._2.EF.Configurations
{
    internal class FriendRequistEntityTypeConfiguration : IEntityTypeConfiguration<FriendRequist>
    {
        public void Configure(EntityTypeBuilder<FriendRequist> builder)
        {
            builder.HasKey(R => R.Id);

            builder.HasOne(R=>R.Sender)
                .WithMany(U=>U.SenderFriendRequists)
                .HasPrincipalKey(U=>U.Id)
                .HasForeignKey(R=>R.SenderId);

            builder.HasOne(R => R.Resiver)
               .WithMany(U => U.ResiverFriendRequists)
               .HasPrincipalKey(U => U.Id)
               .HasForeignKey(R => R.ResiverId);
        }
    }
}
