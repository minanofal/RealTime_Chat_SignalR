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
    internal class MessageEntityTypeConfigurations :  IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(M=>M.Id);

            builder.HasOne(M => M.connection)
                .WithMany(C => C.Messages)
                .HasPrincipalKey(C => C.ConnectionId)
                .HasForeignKey(M => M.ConnectionId);

            builder.HasOne(M => M.Sender)
               .WithMany(U => U.Messages)
               .HasPrincipalKey(U =>U.Id )
               .HasForeignKey(M => M.SenderId);

        }
    }
}
