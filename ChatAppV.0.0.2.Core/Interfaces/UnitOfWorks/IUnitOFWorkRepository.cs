using ChatAppV._0._0._2.Core.Interfaces.Authentication;
using ChatAppV._0._0._2.Core.Models.Auithentication;
using ChatAppV._0._0._2.Core.Models.ChatModels;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppV._0._0._2.Core.Interfaces.UnitOfWorks
{
    public interface IUnitOFWorkRepository
    {

        IBaseRepository<FriendRequist> FriendRequists {get;}
        IBaseRepository<Message> Messages {get;}
        IBaseRepository<Connection> Connections {get;}
        IBaseRepository<ApplicationUser> Users { get; }
        Task SaveChanges();

    }
}
