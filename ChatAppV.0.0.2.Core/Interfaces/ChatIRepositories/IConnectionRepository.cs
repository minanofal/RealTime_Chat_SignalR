using ChatAppV._0._0._2.Core.DTOS.Inputs.Connection;
using ChatAppV._0._0._2.Core.Interfaces.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppV._0._0._2.Core.Interfaces.ChatIRepositories
{
    public interface IConnectionRepository
    {
        IUnitOFWorkRepository _unitOfWork { get; }
        Task<IEnumerable<Friends>> GetAllFriends(string userName);
        Task<IEnumerable<Friends>> GetFriendSearch(string userName , string search);
    }
}
