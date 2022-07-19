using ChatAppV._0._0._2.Core.Interfaces.ChatIRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppV._0._0._2.Core.Interfaces.UnitOfWorks
{
     public interface IUnitOfWork
    {

        IFriendRequestRepository FrindRequist { get; }
        IConnectionRepository Connections { get; }
        IUserRepository users { get; }
        IMessageRepository messages { get; }
    }
}
