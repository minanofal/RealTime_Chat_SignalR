using AutoMapper;
using ChatAppV._0._0._2.Core.Interfaces.ChatIRepositories;
using ChatAppV._0._0._2.Core.Interfaces.UnitOfWorks;
using ChatAppV._0._0._2.Core.Models.Auithentication;
using ChatAppV._0._0._2.EF.Repositories.ChatRepository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppV._0._0._2.EF.Repositories.UnitOFWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        public IFriendRequestRepository FrindRequist { get; private set; }

        public IConnectionRepository Connections { get; private set; }
        public IUserRepository users { get; private set; }

        public IMessageRepository messages { get; private set; }

        public UnitOfWork(ApplicationDbContext context , IMapper _mapper)
        {
            FrindRequist = new FriendRequestRepository(context , _mapper );
            Connections = new ConnectionRepository(context , _mapper );
            users = new UserRepository(context , _mapper );
            messages = new MessageRepository(context);
        }
    }
}
