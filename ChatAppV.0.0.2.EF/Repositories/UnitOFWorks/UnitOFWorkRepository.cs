using ChatAppV._0._0._2.Core.Interfaces;
using ChatAppV._0._0._2.Core.Interfaces.UnitOfWorks;
using ChatAppV._0._0._2.Core.Models.Auithentication;
using ChatAppV._0._0._2.Core.Models.ChatModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppV._0._0._2.EF.Repositories.UnitOFWorks
{
    public class UnitOFWorkRepository : IUnitOFWorkRepository
    {
        public IBaseRepository<FriendRequist> FriendRequists { get; private set; }

        public IBaseRepository<Message> Messages { get; private set; }
        public IBaseRepository<Connection> Connections { get; private set; }

        public IBaseRepository<ApplicationUser> Users { get; private set; }
        private readonly ApplicationDbContext _context;

        public UnitOFWorkRepository(ApplicationDbContext context)
        {
            FriendRequists = new BaseRepository<FriendRequist>(context);
            Messages = new BaseRepository<Message>(context);
            Connections = new BaseRepository<Connection>(context);
            Users = new BaseRepository<ApplicationUser>(context);
            _context = context;
        }
        public  async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

    }
}
