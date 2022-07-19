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
    internal class ConnectionsEntityTypeConfigurations : IEntityTypeConfiguration<Connection>
    {
        public void Configure(EntityTypeBuilder<Connection> builder)
        {
            builder.HasKey(C => C.ConnectionId);

            builder.HasOne(C => C.User1)
                .WithMany(U => U.User1Connection)
                .HasPrincipalKey(U => U.Id)
                .HasForeignKey(C => C.User1ID);

            builder.HasOne(C => C.User2)
             .WithMany(U => U.User2Connection)
             .HasPrincipalKey(U => U.Id)
             .HasForeignKey(C => C.User2ID);

            builder.HasMany(C => C.Messages)
                 .WithOne(M => M.connection)
                 .HasPrincipalKey(C => C.ConnectionId)
                 .HasForeignKey(M => M.ConnectionId);
        }
    }
}
