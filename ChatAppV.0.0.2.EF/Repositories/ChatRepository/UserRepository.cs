using AutoMapper;
using ChatAppV._0._0._2.Core.DTOS.Outputs.User;
using ChatAppV._0._0._2.Core.Interfaces.ChatIRepositories;
using ChatAppV._0._0._2.Core.Interfaces.UnitOfWorks;
using ChatAppV._0._0._2.EF.Repositories.UnitOFWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppV._0._0._2.EF.Repositories.ChatRepository
{
    public class UserRepository : IUserRepository
    {
        public IUnitOFWorkRepository _unitOfWork { get; private set; }
        private readonly IMapper _mapper;
        public IConnectionRepository  connectionRepository { get; private set; }
        public IFriendRequestRepository friendRequestRepository { get; private set; }

        public UserRepository(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = new UnitOFWorkRepository(context);
            connectionRepository = new ConnectionRepository(context , mapper);
            friendRequestRepository = new FriendRequestRepository(context , mapper);
        }

        public async Task<IEnumerable<UserDto>> GetUsers(string username)
        {
            var user = await _unitOfWork.Users.GetAsyncTraking(U => U.UserName == username);
            if (user == null)
                return null;
            var friends = await connectionRepository.GetAllFriends(username);
            var requsets = await friendRequestRepository.GetMyFrindRequists(username);
            var requestsForme = await friendRequestRepository.GetFrindRequists(username);

            var users = await _unitOfWork.Users.GetAllAsyncNoTraking(U => U.UserName != username && !friends.Select(f => f.UserName).Contains(U.UserName) && !requsets.Select(f => f.UserName).Contains(U.UserName) && !requestsForme.Select(f=>f.UserName).Contains(U.UserName));

            var result = _mapper.Map<IEnumerable<UserDto>>(users);
            return result;



        }

        public async Task<IEnumerable<UserDto>> GetUsersSearch(string username, string search)
        {
            var user = await _unitOfWork.Users.GetAsyncTraking(U => U.UserName == username);
            if (user == null)
                return null;

            var friends = await connectionRepository.GetAllFriends(username);
            var requsets = await friendRequestRepository.GetMyFrindRequists(username);
            var requestsForme = await friendRequestRepository.GetFrindRequists(username);

            var users = await _unitOfWork.Users.GetAllAsyncNoTraking(U=>U.UserName != username && U.UserName.Contains(search) && !requestsForme.Select(f=>f.UserName).Contains(U.UserName));

            var results = _mapper.Map<IEnumerable<UserDto>>(users);

            foreach (var result in results)
            {
                if (friends.Select(f => f.UserName).Contains(result.UserName))
                    result.status = "friend";
                else if (requsets.Select(f => f.UserName).Contains(result.UserName))
                    result.status = "send requist";
              

            }
            return results;
        }
    }
}
