using ChatAppV._0._0._2.Core.DTOS.Outputs.User;
using ChatAppV._0._0._2.Core.Interfaces.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppV._0._0._2.Core.Interfaces.ChatIRepositories
{
    public interface IUserRepository
    {
        IUnitOFWorkRepository _unitOfWork { get; }
        Task<IEnumerable<UserDto>> GetUsers(string username);

        Task<IEnumerable<UserDto>> GetUsersSearch(string username , string search);
    }
}
